using AthleteHub.Domain.Entities;

namespace AthleteHub.Domain.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    public IGenericRepository<Athlete> Athletes { get; }
    public IGenericRepository<AthleteActiveSubscribtion> AthleteActiveSubscribtions { get; }
    public IGenericRepository<AthleteCoach> AthletesCoaches { get; }
    public IGenericRepository<AthleteSubscribtionHistory> AthletesSubscribtionsHistory { get; }
    public IGenericRepository<AthleteFavouriteCoach> AthleteFavouriteCoach { get; }
    public IGenericRepository<Coach> Coaches { get; }
    public IGenericRepository<Content> Contents { get; }
    public IGenericRepository<Feature> Features { get; }
    public IGenericRepository<Measurement> Measurements { get; }
    public IGenericRepository<Post> Posts { get; }
    public IGenericRepository<PostContent> PostContents { get; }
    public IGenericRepository<Subscribtion> Subscribtions { get; }
    public IGenericRepository<SubscribtionFeature> SubscribtionFeatures { get; }

    Task<int> CommitAsync();
}
