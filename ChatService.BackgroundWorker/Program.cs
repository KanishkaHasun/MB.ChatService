using ChatService.BackgroundWorker.Services;
using ChatService.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ShiftService>();

builder.Services.AddInfrastructure(builder.Configuration);

var host = builder.Build();
host.Run();
