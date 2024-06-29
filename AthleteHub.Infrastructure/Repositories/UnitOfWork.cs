using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Interfaces.Repositories;
using AthleteHub.Infrastructure.Persistance;

namespace AthleteHub.Infrastructure.Repositories;

internal class UnitOfWork(AthleteHubDbContext _context) : IUnitOfWork
{
    private bool _isDisposed;
    private IGenericRepository<Athlete> _athletes;
    private IGenericRepository<AthleteActiveSubscribtion> _athleteActiveSubscribtions;
    private IGenericRepository<AthleteFavouriteCoach> _athleteFavouriteCoach;
    private IGenericRepository<CoachRating> _coachRatings;
    private IGenericRepository<AthleteCoach> _athletesCoaches;
    private IGenericRepository<AthleteSubscribtionHistory> _athletesSubscribtionsHistory;
    private IGenericRepository<Coach> _coaches;
    private IGenericRepository<Content> _contents;
    private IGenericRepository<Feature> _features;
    private IGenericRepository<Measurement> _measurements;
    private IGenericRepository<Post> _posts;
    private IGenericRepository<PostContent> _postContents;
    private IGenericRepository<Subscribtion> _subscribtions;
    private IGenericRepository<SubscribtionFeature> _subscribtionFeatures;

    public IGenericRepository<Athlete> Athletes
    {
        get
        {
            _athletes ??= new GenericRepository<Athlete>(_context);
            return _athletes;
        }
    }

    public IGenericRepository<CoachRating> CoachRatings
    {
        get
        {
            _coachRatings ??= new GenericRepository<CoachRating>(_context);
            return _coachRatings;
        }
    }
    public IGenericRepository<AthleteActiveSubscribtion> AthleteActiveSubscribtions
    {
        get
        {
            _athleteActiveSubscribtions ??= new GenericRepository<AthleteActiveSubscribtion>(_context);
            return _athleteActiveSubscribtions;
        }
    }
    public IGenericRepository<AthleteFavouriteCoach> AthleteFavouriteCoach
    {
        get
        {
            _athleteFavouriteCoach ??= new GenericRepository<AthleteFavouriteCoach>(_context);
            return _athleteFavouriteCoach;
        }
    }

    public IGenericRepository<AthleteCoach> AthletesCoaches
    {
        get
        {
            _athletesCoaches ??= new GenericRepository<AthleteCoach>(_context);
            return _athletesCoaches;
        }
    }

    public IGenericRepository<AthleteSubscribtionHistory> AthletesSubscribtionsHistory
    {
        get
        {
            _athletesSubscribtionsHistory ??= new GenericRepository<AthleteSubscribtionHistory>(_context);
            return _athletesSubscribtionsHistory;
        }
    }

    public IGenericRepository<Coach> Coaches
    {
        get
        {
            _coaches ??= new GenericRepository<Coach>(_context);
            return _coaches;
        }
    }

    public IGenericRepository<Content> Contents
    {
        get
        {
            _contents ??= new GenericRepository<Content>(_context);
            return _contents;
        }
    }

    public IGenericRepository<Feature> Features
    {
        get
        {
            _features ??= new GenericRepository<Feature>(_context);
            return _features;
        }
    }

    public IGenericRepository<Measurement> Measurements
    {
        get
        {
            _measurements ??= new GenericRepository<Measurement>(_context);
            return _measurements;
        }
    }

    public IGenericRepository<Post> Posts
    {
        get
        {
            _posts ??= new GenericRepository<Post>(_context);
            return _posts;
        }
    }

    public IGenericRepository<PostContent> PostContents
    {
        get
        {
            _postContents ??= new GenericRepository<PostContent>(_context);
            return _postContents;
        }
    }

    public IGenericRepository<Subscribtion> Subscribtions
    {
        get
        {
            _subscribtions ??= new GenericRepository<Subscribtion>(_context);
            return _subscribtions;
        }
    }

    public IGenericRepository<SubscribtionFeature> SubscribtionFeatures
    {
        get
        {
            _subscribtionFeatures ??= new GenericRepository<SubscribtionFeature>(_context);
            return _subscribtionFeatures;
        }
    }


    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool isDisposing)
    {
        if (_isDisposed)
            return;
        if(isDisposing)
        {
            _context.Dispose();
            _isDisposed = true;
        }
    }
}
