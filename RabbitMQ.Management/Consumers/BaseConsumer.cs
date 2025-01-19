
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Management.Infrastructure;

namespace RabbitMQ.Management.Consumers;

public class BaseConsumer :IBaseConsumer
{
    private readonly IConnectionManager _connectionManager;
    public BaseConsumer(IConnectionManager connectionManager) => _connectionManager = connectionManager;

    public async Task SetConfig(IDictionary<string,ConsumerConfig> consumerConfig)
    {
        var connection = await _connectionManager.Connect();
        using var channel = await connection.CreateChannelAsync();

        foreach (var config in consumerConfig)
        {
            await channel.ExchangeDeclareAsync(config.Key, config.Value.ExchangeType, durable: true, autoDelete: false);
            foreach (var queue in config.Value.QueueConfigs)
            {
                await channel.QueueDeclareAsync(queue.QueueName, 
                                                durable: true,
                                                exclusive: false,
                                                autoDelete: false,
                                                arguments: null);

                await channel.QueueBindAsync(queue.QueueName, config.Key, string.Empty);

                new AsyncEventingBasicConsumer(channel).ReceivedAsync += queue.Consumer;
            }
        }
    }
}
