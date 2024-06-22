using AthleteHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AthleteHub.Infrastructure.Persistance.FluentApiConfigurations;

internal class CoachBlockedAthleteConfiguration : IEntityTypeConfiguration<CoachBlockedAthlete>
{
    public void Configure(EntityTypeBuilder<CoachBlockedAthlete> builder)
    {
        builder.HasKey(t => new { t.AthleteId, t.CoachId });

        builder.HasOne(t => t.Coach).WithMany(c => c.CoachesBlockedAthletees).HasForeignKey(t => t.CoachId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(t => t.Athlete).WithMany(a => a.CoachesBlockedAthletees).HasForeignKey(t => t.AthleteId).OnDelete(DeleteBehavior.Restrict);
    }
}
