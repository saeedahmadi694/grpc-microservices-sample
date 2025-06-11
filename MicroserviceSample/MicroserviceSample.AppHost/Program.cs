var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("Sample-db")
    .WithPgAdmin();

var rabbitMq = builder.AddRabbitMQ("Sample-mq")
    .WithManagementPlugin();

builder.AddProject<Projects.Service1>("service1")
    .WithReference(postgres)
    .WithReference(rabbitMq)
    .WaitFor(postgres)
    .WaitFor(rabbitMq);

builder.AddProject<Projects.Service2>("service2")
    .WithReference(postgres)
    .WithReference(rabbitMq)
    .WaitFor(postgres)
    .WaitFor(rabbitMq);

builder.Build().Run();
