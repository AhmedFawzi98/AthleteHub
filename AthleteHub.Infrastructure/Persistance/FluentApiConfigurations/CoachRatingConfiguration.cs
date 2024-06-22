using AthleteHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AthleteHub.Infrastructure.Persistance.FluentApiConfigurations;

internal class CoachRatingConfiguration : IEntityTypeConfiguration<CoachRating>
{
    public void Configure(EntityTypeBuilder<CoachRating> builder)
    {
        builder.HasKey(t => new { t.AthleteId, t.CoachId });

        builder.HasOne(t => t.Coach).WithMany(c => c.CoachesRatings).HasForeignKey(t => t.CoachId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(t => t.Athlete).WithMany(a => a.CoachesRatings).HasForeignKey(t => t.AthleteId).OnDelete(DeleteBehavior.Restrict);
    }
}
