using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyStagram.Core.Models.Domain.Auth;

namespace MyStagram.Infrastructure.Database.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.UserName).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasMany(u => u.Posts)
                        .WithOne(p => p.User)
                        .HasForeignKey(p => p.UserId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Comments)
                        .WithOne(c => c.User)
                        .HasForeignKey(c => c.UserId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Likes)
                        .WithOne(l => l.User)
                        .HasForeignKey(l => l.UserId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}