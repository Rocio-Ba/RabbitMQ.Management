using RabbitMQ.Client;

namespace RabbitMQ.Management.Producers;

public record FanoutExchangeType<T>(T Message,string Exchange);
public record TopicOrDirectExchangeType<T>(T Message, string Exchange,string RoutingKey) : FanoutExchangeType<T>(Message,Exchange);
public record HeaderExchangeType<T>(T Message, string Exchange, BasicProperties BasicProperties) : FanoutExchangeType<T>(Message, Exchange);
