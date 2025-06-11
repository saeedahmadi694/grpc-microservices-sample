namespace Service1.RabbitMqService;

public interface IRabbitMqPublisher
{
    void Publish(string queue, string message);
}
