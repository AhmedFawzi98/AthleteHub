using AthleteHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AthleteHub.Infrastructure.Persistance.FluentApiConfigurations;

internal class MeasurementConfiguration : IEntityTypeConfiguration<Measurement>
{
    public void Configure(EntityTypeBuilder<Measurement> builder)
    {
        builder.HasKey(t => new {t.AthleteId, t.Date});

        builder.Property(u => u.WeightInKg).HasColumnType("decimal(5, 2)");
        builder.Property(u => u.BMI).HasColumnType("decimal(5, 2)");
        builder.Property(u => u.BodyFatPercentage).HasColumnType("decimal(5, 2)");
    }
}
