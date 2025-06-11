using Service2.Configurations;
using Service2.RabbitMqService;

namespace Service2.DependencyInjection;

public static class MassTransitInjection
{
    public static IServiceCollection AddConfiguredMassTransit(this IServiceCollection services,
        IConfiguration configuration)
    {

        var rabbitOption = new RabbitMqConfig();
        configuration.GetSection(RabbitMqConfig.Key).Bind(rabbitOption);

        services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();
        services.AddSingleton<IRabbitMqConsumer, RabbitMqConsumer>();
        return services;
    }
}

