using AthleteHub.Application.Coaches.Dtoes;
using AthleteHub.Application.Coaches.Queries.GetAllCoaches;
using AthleteHub.Application.Services.BlobStorageService;
using AthleteHub.Application.Services.FilterService;
using AthleteHub.Application.Services.SearchService;
using AthleteHub.Application.Services.SortingService;
using AthleteHub.Application.Users;
using AthleteHub.Domain.Constants;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Resturants.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Athletes.Queries.GetAllfavoriteCoaches
{
    public class GetAllfavoriteCoachesQueryHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IFilterService _filterService,
        ISearchService _searchService, ISortingService _sortService, IBlobStorageService _blobStorageService, IUserContext _usercontext)
        : IRequestHandler<GetAllfavoriteCoachesQuery, PageResultsDto<CoachDto>>
    {
        public async Task<PageResultsDto<CoachDto>> Handle(GetAllfavoriteCoachesQuery request, CancellationToken cancellationToken)
        {

            Dictionary<Expression<Func<AthleteFavouriteCoach, object>>, KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>> includes = new()
            {
                {c=>c.Coach ,
                             new KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>(s=>((Coach)s).ApplicationUser,null) },
            };
            var (athleteFavouriteCoaches, totalCount) = await _unitOfWork.AthleteFavouriteCoach.GetAllAsync(request.PageSize, request.PageNumber, request.SortingDirection
                , null, null, a => a.AthleteId == request.AthleteId, includes);

            var favoriteCoaches = athleteFavouriteCoaches.Select(afc => afc.Coach).ToList();

            var coachDtos = _mapper.Map<IEnumerable<CoachDto>>(favoriteCoaches);
            //foreach (var dto in coachDtos)
            //{
            //    if (!string.IsNullOrEmpty(dto.ProfilePicture))
            //    {
            //        dto.SasProfilePicture = _blobStorageService.GetBlobSasUrl(dto.ProfilePicture);
            //    }
            //    if (!string.IsNullOrEmpty(dto.Certificate))
            //    {
            //        dto.SasCertificate = _blobStorageService.GetBlobSasUrl(dto.Certificate);
            //    }
            //}
            return new PageResultsDto<CoachDto>(coachDtos, totalCount, request.PageNumber, request.PageSize);
        }
    }
}