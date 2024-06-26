﻿using AthleteHub.Application.Coaches.Dtoes;
using AthleteHub.Application.Services.BlobStorageService;
using AthleteHub.Application.Services.FilterService;
using AthleteHub.Application.Services.SearchService;
using AthleteHub.Application.Services.SortingService;
using AthleteHub.Application.Users;
using AthleteHub.Domain.Constants;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Resturants.Application.Common;
using System.Linq.Expressions;


namespace AthleteHub.Application.Coaches.Queries.GetAllCoaches
{
    public class GetAllCoachesHandeler : IRequestHandler<GetAllCoachesQuery, PageResultsDto<CoachDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFilterService _filterService;
        private readonly ISearchService _searchService;
        private readonly ISortService _sortService;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IUserContext _userContext;
        public GetAllCoachesHandeler(IUnitOfWork unitOfWork, IMapper mapper, IFilterService filterService, ISearchService searchService, ISortService sortService, IBlobStorageService blobStorageService,IUserContext usercontext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _filterService = filterService;
            _searchService = searchService;
            _sortService = sortService;
            _blobStorageService = blobStorageService;
            _userContext = usercontext;
        }

        public async Task<PageResultsDto<CoachDto>> Handle(GetAllCoachesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Coach> coaches;
            
            int totalCount;
            Dictionary<Expression<Func<Coach, object>>, KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>> includes = new()
            {
                {c=>c.ApplicationUser , 
                             new KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>(null,null) },
                {c=>c.Subscribtions, new KeyValuePair<Expression<Func<object, object>>,
                                              Expression<Func<object, object>>>(s=>((Subscribtion)s).SubscribtionsFeatures,null)}
            };

            var currentUser= _userContext.GetCurrentUser();
            Athlete currentAthlete = null;  
            if(currentUser != null&&currentUser.IsInRole(RolesConstants.Athlete))
            {
                currentAthlete = await _unitOfWork.Athletes.FindAsync(a => a.ApplicationUserId == currentUser.Id);
            }

            if (request.IncludeCoachesRatings)
            {
                Expression<Func<object, object>> exp1 = (cr => ((CoachRating)cr).Athlete);
                Expression<Func<object, object>> exp2 = (a => ((Athlete)a).ApplicationUser);
                includes.Add(c => c.CoachesRatings,
                    new KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>(exp1, exp2));
            }
                
            IEnumerable<Expression<Func<Coach, bool>>> filterExpressions = _filterService.GetCoachFilterExpressions(request.GenderFilterCritrea,
                                              request.RateFilterCritrea, request.AgeFilterCritrea, request.PriceFilterCritrea);
            
            
            Expression<Func<Coach, bool>> searchExperssion = _searchService.GetCoachSearchExpression(request.SearchCritrea, currentAthlete?.Id??null);
            
            Expression<Func<Coach, object>> sortExperssion = _sortService.GetCoachSortingExpression(request.SortByCritrea, request.SortingDirection);
            
            (coaches, totalCount) = await _unitOfWork.Coaches.GetAllAsync(request.PageSize, request.PageNumber, 
                                      request.SortingDirection, sortExperssion, filterExpressions, searchExperssion, includes);

            var coachesDtos = _mapper.Map<IEnumerable<CoachDto>>(coaches);
            foreach (var dto in coachesDtos)
            {
                if (!string.IsNullOrEmpty(dto.ProfilePicture))
                {
                    dto.SasProfilePicture = _blobStorageService.GetBlobSasUrl(dto.ProfilePicture);
                }
                if (!string.IsNullOrEmpty(dto.Certificate))
                {
                    dto.SasCertificate = _blobStorageService.GetBlobSasUrl(dto.Certificate);
                }
            }

            return new PageResultsDto<CoachDto>(coachesDtos, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
