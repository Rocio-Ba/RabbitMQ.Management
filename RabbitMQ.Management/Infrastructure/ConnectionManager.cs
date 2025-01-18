

using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace RabbitMQ.Management.Infrastructure;

public class ConnectionManager : IConnectionManager
{
    private readonly RabbitMQSettings _setting;
    public ConnectionManager(IOptions<RabbitMQSettings> options)=> _setting = options.Value;
  
    public async Task<IConnection> Connect(ushort dispatchConcurrency = 1)
    {
        var connection = new ConnectionFactory()
        {
            HostName = _setting.HostName,
            UserName = _setting.UserName,
            Password = _setting.Password
        };

        connection.ConsumerDispatchConcurrency = dispatchConcurrency;
        return await connection.CreateConnectionAsync();
    }
}
