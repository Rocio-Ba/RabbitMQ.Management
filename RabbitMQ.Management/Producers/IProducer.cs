

namespace RabbitMQ.Management.Producers;

public interface IProducer
{
    Task PublishMessage<T>(T message);
}
