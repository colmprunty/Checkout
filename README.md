## Checkout.API

The API documentation is generated by Swagger, and can be found at `http://baseurl/swagger/index.html`. This provides each of the endpoints, their HTTP verbs, and the entities required. This document will provide a more general overview.

This API makes the assumption that an order will initially be created when the first item is added, and for MVP purposes there isn't a use case for a user to add an empty order as an action.

Every endpoint in the API also returns the `Order` object, so that the client will have access to their current order, as well as the items and products associated with it.

This MVP is using an in-memory orders collection, and so an order will not be persisted across multiple sessions.

The API does not do validity checking on the `orderId` or `itemId` inputs in the interests of clarity and readability for an MVP.

### **Endpoints**

`OrderAddItem`

This endpoint takes a `POST`, with a JSON serialized `OrderItem` and an optional `orderId`. If the order ID is null, a new order is created with the item. Otherwise the item is added to an existing order. In either case, the order is returned.

`OrderRemoveItem`

This endpoint takes a `DELETE` with a mandatory `orderId` and `itemId`. It removes the item from the order and returns the updated order.

`OrderClear`

This endpoint takes a `DELETE` and a mandatory `orderId`. It deletes all of the items from the order and returns the empty order.

`OrderUpdateItemQuantity`

This endpoint takes a `PUT` and mandatory parameters `orderId`, `itemId` and `quantity`. It finds the item and updates its quantity, and returns the updated order.

## Checkout.Client

The client was generated from the Swagger JSON file using [NSwag Studio](https://github.com/RSuter/NSwag/wiki/NSwagStudio). It has methods for calling all endpoints on the API.

The client project also includes a console application that demonstrates the client usage. It expects the API to be running on `https://localhost:44322`.

## Running the API

There are multiple ways to run the Checkout API. 

### **Visual Studio**

The easiest is to run directly from Visual Studio using IIS Express. It will launch on `https://localhost:44322` and as a default will redirect to the Swagger UI on `https://localhost:44322/swagger/index.html`

### **Docker**

The project root folder contains a Dockerfile that can be used to run the API in a container. It is neccessary to have [Docker](https://www.docker.com/) installed on your system. This API is built using .NET Core 2.2, and so this Dockerfile will run on either Windows or Linux.

It uses a multi-stage build, meaning that the build is done in one container and the runtime container is created from the outputs from this process. This means things like MSBuild (or Node, or Gulp) are only in the build container, leaving the runtime container to only have the files necessary to run the app. 

To build the container, use the following command in the root folder containing the Dockerfile (if on Linux you may need to use `sudo`):

`docker build -t checkoutapi .`

To run the container, use the following command:

`docker run -p 44322:80 checkoutapi`

You can now access the API. On Linux, it will run on `https://localhost:44322`. There is a networking limitation on Windows that means you have to find the container's IP address and use that instead of localhost. To do that:

1. Run `docker ps` and find the ID of the checkout API container.
2. run `docker container inspect --format '{{.NetworkSettings.Networks.nat.IPAddress }}' the_container_id` to find the IP of the container.
3. Then you can access the API on `http://containerIp/swagger`.

### Deploy to AWS

This project also includes an Infrastructure folder with terraform files to deploy a service into an ECS cluster. There are a couple of prerequisites to this:

1. Both [Terraform](http://terraform.io) and the [AWS CLI](https://aws.amazon.com/cli/) are installed.
2. You have an AWS user with Access Key and Secret Key.
3. You follow the [Quick Configuration](https://docs.aws.amazon.com/cli/latest/userguide/cli-chap-configure.html#cli-quick-configuration) setup.
4. You must create a repository in AWS ECR called `checkoutapi`.
5. You must create an S3 bucket for your terraform state, and edit `main.tf` to use this bucket. ("terraform-state" is already taken, as S3 bucket names must be globally unique).

Following this, having completed the Docker step above, you must [push your image to the ECR repository](https://docs.aws.amazon.com/AmazonECR/latest/userguide/docker-push-ecr-image.html).
 
 Finally, you need to edit the `taskdefinition.json` file in the infrastructure folder to include your AWS account ID and region where indicated.

Having set this up, navigate to the infrastructure folder and run:

`terraform init`

`terraform apply`

The latter command will list the infrastructure you're creating and ask if you're sure you want to create it. This will leave you with new ECS Cluster called `checkout-cluster`, a single EC2 instance within an autoscaling group, and an ECS Service with the API running as a task.
