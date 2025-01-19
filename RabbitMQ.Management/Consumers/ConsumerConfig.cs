
using RabbitMQ.Client.Events;

namespace RabbitMQ.Management.Consumers;

public class ConsumerConfig
{
    public string ExchangeType { get; set; }
    public List<QueueConfig> QueueConfigs { get; set; } 
}


public record QueueConfig(string QueueName, AsyncEventHandler<BasicDeliverEventArgs> Consumer); 

