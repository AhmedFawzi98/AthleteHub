using AthleteHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AthleteHub.Infrastructure.Persistance.FluentApiConfigurations;

internal class CoachConfiguration : IEntityTypeConfiguration<Coach>
{
    public void Configure(EntityTypeBuilder<Coach> builder)
    {

        builder.HasMany(coach => coach.Subscribtions).WithOne(sub => sub.Coach).HasForeignKey(sub => sub.CoachId);
        builder.HasMany(coach => coach.Contents).WithOne(content => content.Coach).HasForeignKey(content => content.CoachId);
    }
}
