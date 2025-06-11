namespace Service2.RabbitMqService;

public interface IRabbitMqConsumer
{
    void Consume(string queue, Action<string> handleMessage);
}
