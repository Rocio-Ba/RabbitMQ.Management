
namespace RabbitMQ.Management.Consumers;

public interface IBaseConsumer
{
    Task SetConfig(IDictionary<string, ConsumerConfig> consumerConfig);
}
