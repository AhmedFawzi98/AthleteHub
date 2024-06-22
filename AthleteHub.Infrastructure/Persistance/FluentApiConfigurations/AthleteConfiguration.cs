using AthleteHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AthleteHub.Infrastructure.Persistance.FluentApiConfigurations;

internal class AthleteConfiguration : IEntityTypeConfiguration<Athlete>
{
    public void Configure(EntityTypeBuilder<Athlete> builder)
    {
        builder.HasMany(a => a.Measurements).WithOne(m => m.Athlete).HasForeignKey(m => m.AthleteId);
        builder.HasMany(a => a.Posts).WithOne(p => p.Athlete).HasForeignKey(p => p.AthleteId);
    }
}
