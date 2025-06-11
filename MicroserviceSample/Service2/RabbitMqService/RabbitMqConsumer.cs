using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Service2.Configurations;
using System.Text;
namespace Service2.RabbitMqService;

public class RabbitMqConsumer : IRabbitMqConsumer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly RabbitMqConfig _rabbitMqConfig;

    public RabbitMqConsumer(IOptions<RabbitMqConfig> rabbitMqConfig)
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

    public void Consume(string queue, Action<string> handleMessage)
    {
        _channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false);
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (_, ea) =>
        {
            var message = Encoding.UTF8.GetString(ea.Body.ToArray());
            handleMessage(message);
            _channel.BasicAck(ea.DeliveryTag, multiple: false);
        };

        _channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
    }
}
