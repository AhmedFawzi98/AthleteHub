using AthleteHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AthleteHub.Infrastructure.Persistance.FluentApiConfigurations;

internal class PostContentConfiguration : IEntityTypeConfiguration<PostContent>
{
    public void Configure(EntityTypeBuilder<PostContent> builder)
    {
        builder.HasKey(t => new { t.PostId, t.ContentId });

        builder.HasOne(t => t.Post).WithMany(a => a.PostsContents).HasForeignKey(t => t.PostId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(t => t.Content).WithMany(s => s.PostsContents).HasForeignKey(t => t.ContentId).OnDelete(DeleteBehavior.Restrict);
    }

}
