using ChatService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatService.Infrastructure.Configurations
{
    public class ChatSessionConfiguration :  IEntityTypeConfiguration<ChatSession>
    {
        public void Configure(EntityTypeBuilder<ChatSession> entity)
        {
            entity.ToTable("ChatSessions", b => b.IsTemporal(t =>
            {
                t.HasPeriodStart("ValidFrom");
                t.HasPeriodEnd("ValidTo");
                t.UseHistoryTable("ChatSessionsHistory");
            }));

            entity.Property<DateTime>("ValidFrom") 
                   .ValueGeneratedOnAddOrUpdate();

            entity.Property<DateTime>("ValidTo") 
                   .ValueGeneratedOnAddOrUpdate();
        }
    }
}
