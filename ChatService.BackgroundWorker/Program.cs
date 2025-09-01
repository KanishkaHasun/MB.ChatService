using ChatService.Background.Services;
using ChatService.BackgroundWorker.Services;
using ChatService.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<ShiftService>();
builder.Services.AddHostedService<AssignmentService>();
builder.Services.AddHostedService<QueueMonitorService>();

builder.Services.AddInfrastructure(builder.Configuration);

var host = builder.Build();
host.Run();
