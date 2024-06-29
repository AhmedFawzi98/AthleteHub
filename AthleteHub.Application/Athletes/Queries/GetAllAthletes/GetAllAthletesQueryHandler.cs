using AthleteHub.Application.Athletes.Dtos;
using AthleteHub.Application.Services.BlobStorageService;
using AthleteHub.Application.Services.FilterService;
using AthleteHub.Application.Services.SearchService;
using AthleteHub.Application.Services.SortingService;
using AthleteHub.Application.Users;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Resturants.Application.Common;
using Resturants.Domain.Enums;
using System.Linq.Expressions;


namespace AthleteHub.Application.Athletes.Queries.GetAllAthletes
{
    public class GetAllAthletesQueryHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IFilterService _filterService, ISearchService _searchService, ISortService _sortService, IBlobStorageService _blobStorageService, IUserContext _userContext) : IRequestHandler<GetAllAthletesQuery, PageResultsDto<AthleteDto>>
    {
        public async Task<PageResultsDto<AthleteDto>> Handle(GetAllAthletesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Athlete> athletes;

            int totalCount;
            Dictionary<Expression<Func<Athlete, object>>, KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>> includes = new()
            {
                {a=>a.ApplicationUser , new (null,null) },
            };

            IEnumerable<Expression<Func<Athlete, bool>>> filterExpressions = _filterService.GetAthleteFilterExpressions(request.GenderFilterCritrea, request.AgeFilterCritrea);


            Expression<Func<Athlete, bool>> searchExperssion = _searchService.GetAthleteSearchExpression(request.SearchCritrea);



            (athletes, totalCount) = await _unitOfWork.Athletes.GetAllAsync(request.PageSize, request.PageNumber,
                                                       SortingDirection.Ascending, null, filterExpressions, searchExperssion, includes);
            
            var athletesDtos = _mapper.Map<IEnumerable<AthleteDto>>(athletes);

            foreach (var dto in athletesDtos)
            {
                if (!string.IsNullOrEmpty(dto.ProfilePicture))
                {
                    dto.SasProfilePicture = _blobStorageService.GetBlobSasUrl(dto.ProfilePicture);
                }
            }

            return new PageResultsDto<AthleteDto>(athletesDtos, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
