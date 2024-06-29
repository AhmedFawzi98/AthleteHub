using AthleteHub.Application.Athletes.Dtos;
using AthleteHub.Application.Services.BlobStorageService;
using AthleteHub.Application.Services.FilterService;
using AthleteHub.Application.Services.SearchService;
using AthleteHub.Application.Users;
namespace AthleteHub.Application.Athletes.Queries.GetAthletesBySubscriptionId
{
    //public class GetAthletesBySubscriptionIdQueryHandler(IUnitOfWork _unitOfWork, IMapper _mapper,ISearchService _searchService, IFilterService _filterService ,IBlobStorageService _blobStorageService, IUserContext _userContext) : IRequestHandler<GetAthletesBySubscriptionIdQuery, PageResultsDto<AthleteDto>>
    //{
    //    public async Task<PageResultsDto<AthleteDto>> Handle(GetAthletesBySubscriptionIdQuery request, CancellationToken cancellationToken)
    //    {
    //        var currentUser = _userContext.GetCurrentUser();
    //        if (currentUser==null||!(currentUser.IsInRole(RolesConstants.Admin)|| currentUser.IsInRole(RolesConstants.Coach)))
    //            throw new UnAuthorizedException();

    //        await _unitOfWork.AthleteActiveSubscribtions.GetAllAsync();

    //    }
    //}
}
