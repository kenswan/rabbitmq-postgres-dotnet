# rabbitmq-postgres-dotnet

Chat/Messenger application leveraging RabbitMQ messaging and PostgresSQL storage

## Pre-Requisites

- Docker
- Postgres
- .NET Core 3.1

## Configuration

### RabbitMQ

- Install RabbitMQ Community Docker Image ([Downloading and Installing RabbitMQ](https://www.rabbitmq.com/download.html))

  `docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management`

- Navigate to [https://localhost:15672](https://localhost:15672) in your browser
- Log in as username: `guest` password: `guest` to ensure setup is ready
- Add virtual host
- Create new admin user/pass
- Create new message queue
- Copy `appsettings.json` into a new `appsettings.Development.json` file in the same location
- Copy your newly create properties in the corresponding fields in `RabbitMQ` section

### Postgres

- Install Postgres
  `docker run --rm --name postgres-docker -e POSTGRES_PASSWORD=docker -d -p 5432:5432 -v $HOME/docker/volumes/postgres:/var/lib/postgresql/data postgres`
- Add appropriate credentials to appsettings.Development.json in `DefaultConnection` under `ConnectionStrings`
  ex. `Host=localhost;Port=<port>;Database=MessageAppDb;Username=<username>;Password=<password>;`

## Known Issues

- RabbitMQ.Client currently unable to connect (MessageProviderClient.cs). Using RabbitMQ API directly for now.
- RabbitMQ currently returning 401 unauthorized for request

Working Curl request
`curl -XPOST -d'{"properties":{},"routing_key":"<Queue>","payload":"my body","payload_encoding":"string"}' http://<user>:<pass>@localhost:15672/api/exchanges/<virtualhost>/amq.default/publish`
