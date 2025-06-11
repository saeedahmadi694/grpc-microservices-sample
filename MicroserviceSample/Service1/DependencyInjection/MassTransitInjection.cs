using Service1.Configurations;
using Service1.RabbitMqService;

namespace Service1.DependencyInjection;

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

