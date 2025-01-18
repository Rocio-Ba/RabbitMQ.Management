

using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Management.Infrastructure;
using System.Text;

namespace RabbitMQ.Management.Producers;

public class Producer : IProducer
{
    private readonly IConnectionManager _connectionManager;
    public Producer(IConnectionManager connectionManager) => _connectionManager = connectionManager;

    public async Task PublishMessage<T>(ProducerConfig<T> producerConfig)
    {
        var connection = await _connectionManager.Connect();
        using var channel = await connection.CreateChannelAsync();

        var props = new BasicProperties() { Headers = producerConfig.BasicProperties };
        var msg = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(producerConfig.Message));

        await channel.BasicPublishAsync(exchange: producerConfig.Exchange,
                                        routingKey: producerConfig.RoutingKey,
                                        mandatory:true,
                                        basicProperties: producerConfig.BasicProperties,
                                        body: msg);

    }
}
