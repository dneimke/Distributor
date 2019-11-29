# Distributor
A distributed system / messaging playground which may one day evolve into something useful

## Tech Used

- DotNet Core
- RabbitMQ (Docker)
- MassTransit
- Autofac
- Serilog

## Important Learnings

- [How MassTransit uses Rabbit for publishing, routing, and delivery](https://masstransit-project.com/MassTransit/understand/publishing.html#routing-on-rabbitmq)

This one lets you wrap your head around how MT creates Rabbit Exchanges, Queues, and Bindings to accomplish various message delivery semantics - single consumer (commands), competing consumer (commands), fan-out (events)
