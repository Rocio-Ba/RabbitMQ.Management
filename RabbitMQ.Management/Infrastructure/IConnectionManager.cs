
using RabbitMQ.Client;

namespace RabbitMQ.Management.Infrastructure;

public interface IConnectionManager
{
    Task<IConnection> Connect(ushort dispatchConcurrency = 1);
}
