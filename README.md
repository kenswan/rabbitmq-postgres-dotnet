# rabbitmq-postgres-dotnet

Chat/Messenger application leveraging RabbitMQ messaging and PostgresSQL storage

## Pre-Requisites

- Docker

## Installation

### RabbitMQ

- Install RabbitMQ Community Docker Image ([Downloading and Installing RabbitMQ](https://www.rabbitmq.com/download.html))

  `docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management`

- Navigate to [https://localhost:15672](https://localhost:15672) in your browser
- Log in as username: `guest` password: `guest` to ensure setup is ready
