using ChatService.Application.Interfaces;
using ChatService.Application.Services;
using ChatService.Infrastructure.Data;
using ChatService.Infrastructure.Repositories;
using ChatService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace ChatService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MbDbContext>(opt =>
               opt.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

            services.AddSingleton<IConnectionMultiplexer>(_ =>
            {
                var config = ConfigurationOptions.Parse(configuration.GetConnectionString("RedisConnection")!);
                config.AbortOnConnectFail = false;
                return ConnectionMultiplexer.Connect(config);
            });

            #region Redis Services
            services.AddScoped<IChatSessionQueueService, ChatSessionQueueService>();
            services.AddScoped<IChatSessionPolllingService, ChatSessionPolllingService>();
            #endregion

            #region Repository
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            #endregion

            return services;
        }

    }
}
