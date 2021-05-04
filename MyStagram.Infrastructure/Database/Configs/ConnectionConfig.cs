using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyStagram.Core.Models.Domain.Connection;

namespace MyStagram.Infrastructure.Database.Configs
{
    public class ConnectionConfig : IEntityTypeConfiguration<Connection>
    {
        public void Configure(EntityTypeBuilder<Connection> builder)
        {
            builder.HasKey(c => new { c.UserId, c.ConnectionId });


            builder.HasOne(c => c.User)
            .WithMany(c => c.Connections)
            .HasForeignKey(c => c.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}