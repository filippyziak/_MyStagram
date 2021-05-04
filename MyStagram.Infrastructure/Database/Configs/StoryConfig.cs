using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyStagram.Core.Models.Domain.Social;

namespace MyStagram.Infrastructure.Database.Configs
{
    public class StoryConfig : IEntityTypeConfiguration<Story>
    {
        public void Configure(EntityTypeBuilder<Story> builder)
        {
            builder.HasOne(s => s.User)
                        .WithMany(u => u.Stories)
                        .HasForeignKey(s => s.UserId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}