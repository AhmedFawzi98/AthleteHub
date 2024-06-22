using AthleteHub.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AthleteHub.Infrastructure.Persistance;

internal class AthleteHubDbContext : IdentityDbContext<ApplicationUser>
{
    public AthleteHubDbContext(DbContextOptions options) : base(options)
    {
        
    }
    public virtual DbSet<Athlete> Athletes { get; set; }
    public virtual DbSet<AthleteActiveSubscribtion> AthleteActiveSubscribtions { get; set; }
    public virtual DbSet<AthleteCoach> AthletesCoaches { get; set; }
    public virtual DbSet<AthleteSubscribtionHistory> AthletesSubscribtionsHistory { get; set; }
    public virtual DbSet<Coach> Coaches { get; set; }
    public virtual DbSet<Content> Contents { get; set; }
    public virtual DbSet<Feature> Features { get; set; }
    public virtual DbSet<Measurement> Measurements { get; set; }
    public virtual DbSet<Post> Posts { get; set; }
    public virtual DbSet<PostContent> PostContents { get; set; }
    public virtual DbSet<Subscribtion> Subscribtions { get; set; }
    public virtual DbSet<SubscribtionFeature> SubscribtionFeatures { get; set; }
    public virtual DbSet<AthleteFavouriteCoach> AthletesFavouriteCoaches { get; set; }
    public virtual DbSet<CoachRating> CoachesRatings { get; set; }
    public virtual DbSet<CoachBlockedAthlete> CoachesBlockedAthletees { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
    public override int SaveChanges()
    {
        HandleCascadeDeletes();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        HandleCascadeDeletes();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void HandleCascadeDeletes()
    {
        var entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);

        foreach (var entry in entries)
        {
            if (entry.Entity is Athlete athlete)
            {
                var athleteCoaches = AthletesCoaches.Where(ac => ac.AthleteId == athlete.Id).ToList();
                AthletesCoaches.RemoveRange(athleteCoaches);

                var athleteActiveSubscribtions = AthleteActiveSubscribtions.Where(aas => aas.AthleteId == athlete.Id).ToList();
                AthleteActiveSubscribtions.RemoveRange(athleteActiveSubscribtions);

                var athleteFavouriteCoaches = AthletesFavouriteCoaches.Where(afc => afc.AthleteId == athlete.Id).ToList();
                AthletesFavouriteCoaches.RemoveRange(athleteFavouriteCoaches);

                var athleteCoachesRatings = CoachesRatings.Where(acr => acr.AthleteId == athlete.Id).ToList();
                CoachesRatings.RemoveRange(athleteCoachesRatings);

                var coachesBlockedAthletees = CoachesBlockedAthletees.Where(cba => cba.AthleteId == athlete.Id).ToList();
                CoachesBlockedAthletees.RemoveRange(coachesBlockedAthletees);
            }
            else if(entry.Entity is Content content)
            {
                var postsContents = PostContents.Where(pc => pc.ContentId == content.Id).ToList();
                PostContents.RemoveRange(postsContents);
            }
            else if(entry.Entity is Feature feature)
            {
                var subscribtionFeatures = SubscribtionFeatures.Where(sf => sf.FeatureId == feature.Id).ToList();
                SubscribtionFeatures.RemoveRange(subscribtionFeatures);
            }
        }
    }
}
