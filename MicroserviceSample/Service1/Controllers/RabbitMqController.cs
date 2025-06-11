using Microsoft.AspNetCore.Mvc;
using Service1.RabbitMqService;

namespace Service1.Controllers;

[ApiController]
[Route("[controller]")]
public class RabbitMqController : ControllerBase
{

    private readonly ILogger<RabbitMqController> _logger;
    private readonly IRabbitMqPublisher _rabbitMqPublisher;

    public RabbitMqController(ILogger<RabbitMqController> logger, IRabbitMqPublisher rabbitMqPublisher)
    {
        _logger = logger;
        _rabbitMqPublisher = rabbitMqPublisher;
    }

    [HttpGet(Name = "send-message")]
    public async Task<IActionResult> SendMessage(string message)
    {
        _rabbitMqPublisher.Publish("Service1.PublishMessage", message);
        return Ok();
    }
}
