resource "aws_ecs_cluster" "checkout-cluster" {
  name = "checkout-cluster"
}

resource "aws_ecs_service" "checkoutapi" {
  name            = "checkoutapi"
  cluster         = "${aws_ecs_cluster.checkout-cluster.id}"
  task_definition = "${aws_ecs_task_definition.checkoutapi.arn}"
  desired_count   = 1
}
