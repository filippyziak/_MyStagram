using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyStagram.Core.Models.Domain.Social;

namespace MyStagram.Infrastructure.Database.Configs
{
    public class FollowerConfig : IEntityTypeConfiguration<Follower>
    {
        public void Configure(EntityTypeBuilder<Follower> builder)
        {
            builder.HasKey(f => new { f.SenderId, f.RecipientId });
            builder.HasOne(f => f.Sender)
                         .WithMany(u => u.Following)
                         .HasForeignKey(f => f.SenderId)
                         .IsRequired()
                         .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(f => f.Recipient)
                            .WithMany(u => u.Followers)
                            .HasForeignKey(f => f.RecipientId)
                            .IsRequired()
                            .OnDelete(DeleteBehavior.Cascade);
        }
        
    }
}