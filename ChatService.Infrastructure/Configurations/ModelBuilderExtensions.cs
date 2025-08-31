using Microsoft.EntityFrameworkCore;

namespace ChatService.Infrastructure.Configurations
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ModelBuilderExtensions).Assembly
            );
        }
    }
}