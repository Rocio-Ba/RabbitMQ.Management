

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RabbitMQ.Management.Infrastructure;

public static class ConfigureServices
{
    public static void ConfigureRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMQSettings>(p => configuration.GetSection(RabbitMQSettings.Name).Bind(p));
        services.AddSingleton<IConnectionManager, ConnectionManager>();
    }
}
 