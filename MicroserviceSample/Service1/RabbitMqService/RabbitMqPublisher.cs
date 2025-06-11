using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Service1.Configurations;
using System.Text;
namespace Service1.RabbitMqService;

public class RabbitMqPublisher : IRabbitMqPublisher
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly RabbitMqConfig _rabbitMqConfig;

    public RabbitMqPublisher(IOptions<RabbitMqConfig> rabbitMqConfig)
    {
        _rabbitMqConfig = rabbitMqConfig.Value;
        var factory = new ConnectionFactory()
        {
            HostName = _rabbitMqConfig.Host,
            Port = (int)_rabbitMqConfig.Port,
            UserName = _rabbitMqConfig.Username,
            Password = _rabbitMqConfig.Password,
            VirtualHost = _rabbitMqConfig.VirtualHost,
            DispatchConsumersAsync = false
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void Publish(string queue, string message)
    {
        _channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false);
        var body = Encoding.UTF8.GetBytes(message);

        var props = _channel.CreateBasicProperties();
        props.Persistent = true;

        _channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: props, body: body);
    }
}
