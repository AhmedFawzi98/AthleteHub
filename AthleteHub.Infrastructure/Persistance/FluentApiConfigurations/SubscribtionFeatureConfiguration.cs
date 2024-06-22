using AthleteHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AthleteHub.Infrastructure.Persistance.FluentApiConfigurations;

internal class SubscribtionFeatureConfiguration : IEntityTypeConfiguration<SubscribtionFeature>
{
    public void Configure(EntityTypeBuilder<SubscribtionFeature> builder)
    {
        builder.HasKey(t => new { t.SubscribtionId, t.FeatureId });

        builder.HasOne(t => t.Subscribtion).WithMany(sub => sub.SubscribtionsFeatures).HasForeignKey(sub => sub.SubscribtionId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(t => t.Feature).WithMany(f => f.SubscribtionsFeatures).HasForeignKey(sub => sub.FeatureId).OnDelete(DeleteBehavior.Restrict);
    }
}
