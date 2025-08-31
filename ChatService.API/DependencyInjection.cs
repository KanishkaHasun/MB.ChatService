using ChatService.Application.Interfaces;

namespace ChatService.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(IRepositoryManager).Assembly));
            
            return services;
        }
    }
}
