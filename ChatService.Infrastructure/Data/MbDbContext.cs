using ChatService.Domain.Entities;
using ChatService.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Infrastructure.Data
{
    public class MbDbContext : DbContext
    {
        public MbDbContext(DbContextOptions<MbDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<ChatSession> ChatSessions { get; set; } = null!;
        public DbSet<Agent> Agents { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurations();
            base.OnModelCreating(modelBuilder);
        }

    }
}
