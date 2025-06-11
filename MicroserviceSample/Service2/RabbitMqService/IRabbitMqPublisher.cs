namespace Service2.RabbitMqService;

public interface IRabbitMqPublisher
{
    void Publish(string queue, string message);
}
