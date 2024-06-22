using AthleteHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AthleteHub.Infrastructure.Persistance.FluentApiConfigurations;

internal class AthleteActiveSubscribtionConfiguration : IEntityTypeConfiguration<AthleteActiveSubscribtion>
{
    public void Configure(EntityTypeBuilder<AthleteActiveSubscribtion> builder)
    {
        builder.HasKey(t => new { t.AthleteId, t.SubscribtionId });

        builder.HasOne(t => t.Subscribtion).WithMany(s => s.AthletesActiveSubscribtions).HasForeignKey(t => t.SubscribtionId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(t => t.Athlete).WithMany(a => a.AthletesActiveSubscribtions).HasForeignKey(t => t.AthleteId).OnDelete(DeleteBehavior.Restrict);
    }
}
