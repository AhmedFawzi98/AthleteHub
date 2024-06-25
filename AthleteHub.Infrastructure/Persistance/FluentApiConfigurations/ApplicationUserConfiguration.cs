using AthleteHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AthleteHub.Infrastructure.Persistance.FluentApiConfigurations;

internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasOne(u => u.Athlete).WithOne(a => a.ApplicationUser).HasForeignKey<Athlete>(a => a.ApplicationUserId);
        builder.HasOne(u => u.Coach).WithOne(a => a.ApplicationUser).HasForeignKey<Coach>(c => c.ApplicationUserId);

        builder.Property(u => u.FirstName).HasMaxLength(100);
        builder.Property(u => u.LastName).HasMaxLength(100);
        builder.Property(u => u.PhoneNumber).HasMaxLength(15);
    }

}
