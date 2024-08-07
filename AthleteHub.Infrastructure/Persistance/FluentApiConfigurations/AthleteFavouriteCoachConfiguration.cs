﻿using AthleteHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AthleteHub.Infrastructure.Persistance.FluentApiConfigurations;

internal class AthleteFavouriteCoachConfiguration : IEntityTypeConfiguration<AthleteFavouriteCoach>
{
    public void Configure(EntityTypeBuilder<AthleteFavouriteCoach> builder)
    {
        builder.HasKey(t => new { t.AthleteId, t.CoachId });


        builder.HasOne(t => t.Coach).WithMany(c => c.AthletesFavouriteCoaches).HasForeignKey(t => t.CoachId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(t => t.Athlete).WithMany(a => a.AthletesFavouriteCoaches).HasForeignKey(t => t.AthleteId).OnDelete(DeleteBehavior.Restrict);
    }
}
