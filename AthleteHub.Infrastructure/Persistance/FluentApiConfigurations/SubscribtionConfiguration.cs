using AthleteHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AthleteHub.Infrastructure.Persistance.FluentApiConfigurations;

internal class SubscribtionConfiguration : IEntityTypeConfiguration<Subscribtion>
{
    public void Configure(EntityTypeBuilder<Subscribtion> builder)
    {
        builder.Property(s => s.Name).HasMaxLength(100);
        builder.Property(u => u.price).HasColumnType("decimal(8, 2)");
    }

}
