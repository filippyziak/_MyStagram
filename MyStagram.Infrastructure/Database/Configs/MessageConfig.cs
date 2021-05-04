using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyStagram.Core.Models.Domain.Social;

namespace MyStagram.Infrastructure.Database.Configs
{
    public class MessageConfig : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasOne(m => m.Sender)
              .WithMany(u => u.MessagesSent)
              .HasForeignKey(m => m.SenderId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Recipient)
            .WithMany(u => u.MessagesReceived)
            .HasForeignKey(m => m.RecipientId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}