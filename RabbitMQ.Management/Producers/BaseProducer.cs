

using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Management.Infrastructure;
using System.Text;

namespace RabbitMQ.Management.Producers;

public class BaseProducer : IBaseProducer
{
    private readonly IConnectionManager _connectionManager;
    public BaseProducer(IConnectionManager connectionManager) => _connectionManager = connectionManager;

    public async Task FanoutPublish<T>(FanoutExchangeType<T> producerConfig)
    {
        var connection = await _connectionManager.Connect();
        using var channel = await connection.CreateChannelAsync();
      
        await channel.BasicPublishAsync(exchange: producerConfig.Exchange,
                                        routingKey:"",
                                        mandatory:true,
                                        basicProperties: new BasicProperties(),
                                        body: ByteEncoding(producerConfig.Message));

    }

    public async Task TopicOrDirectPublish<T>(TopicOrDirectExchangeType<T> producerConfig)
    {
        var connection = await _connectionManager.Connect();
        using var channel = await connection.CreateChannelAsync();

        await channel.BasicPublishAsync(exchange: producerConfig.Exchange,
                                        routingKey: producerConfig.RoutingKey,
                                        mandatory: true,
                                        basicProperties: new BasicProperties(),
                                        body: ByteEncoding(producerConfig.Message));

    }

    public async Task HeaderPublish<T>(HeaderExchangeType<T> producerConfig)
    {
        var connection = await _connectionManager.Connect();
        using var channel = await connection.CreateChannelAsync();

        await channel.BasicPublishAsync(exchange: producerConfig.Exchange,
                                 routingKey: "",
                                 mandatory: true,
                                 basicProperties: new BasicProperties(),
                                 body: ByteEncoding(producerConfig.Message));
    } 

    private byte[] ByteEncoding<T>(T message) => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
}
