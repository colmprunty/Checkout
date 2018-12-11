resource "aws_launch_configuration" "checkout_launch_configuration" {
  name                 = "ecs-launch-configuration"
  image_id             = "ami-05b65c0f6a75c1c64"
  instance_type        = "t2.micro"
  iam_instance_profile = "${aws_iam_instance_profile.ecs-instance-profile.name}"

  user_data = <<EOF
#!/bin/bash 
echo ECS_CLUSTER=checkout-cluster >> /etc/ecs/ecs.config
EOF

  associate_public_ip_address = "false"
}

resource "aws_autoscaling_group" "checkout_asg" {
  name                 = "checkout_asg"
  max_size             = 1
  min_size             = 1
  desired_capacity     = 1
  availability_zones   = ["eu-west-1a", "eu-west-1b", "eu-west-1c"]
  launch_configuration = "${aws_launch_configuration.checkout_launch_configuration.name}"
}
