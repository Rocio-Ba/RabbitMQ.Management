

namespace RabbitMQ.Management.Producers;

public interface IBaseProducer
{
    Task FanoutPublish<T>(FanoutExchangeType<T> producerConfig);
    Task TopicOrDirectPublish<T>(TopicOrDirectExchangeType<T> producerConfig);
    Task HeaderPublish<T>(HeaderExchangeType<T> producerConfig);
}
