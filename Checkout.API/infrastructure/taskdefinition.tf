resource "aws_ecs_task_definition" "checkoutapi" {
  family                = "checkoutapi"
  container_definitions = "${file("./taskdefinition.json")}"
  network_mode          = "bridge"
}
