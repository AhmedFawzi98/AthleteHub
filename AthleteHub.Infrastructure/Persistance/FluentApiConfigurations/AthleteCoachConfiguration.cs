using AthleteHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AthleteHub.Infrastructure.Persistance.FluentApiConfigurations;

internal class AthleteCoachConfiguration : IEntityTypeConfiguration<AthleteCoach>
{
    public void Configure(EntityTypeBuilder<AthleteCoach> builder)
    {
        builder.HasKey(t => new { t.AthleteId, t.CoachId });

        builder.HasOne(t => t.Coach).WithMany(c => c.AthleteCoaches).HasForeignKey(t => t.CoachId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(t => t.Athlete).WithMany(a => a.AthletesCoaches).HasForeignKey(t => t.AthleteId).OnDelete(DeleteBehavior.Restrict);
    }
}
