using AthleteHub.Application.Services.SortingService;
using AthleteHub.Application.Subscribtions.Dtos;
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

namespace AthleteHub.Application.Subscribtions.Queries.GetAllSubscribtions
{
    public class GetAllSubscribtionsQueryHandler : IRequestHandler<GetAllSubscribtionsQuery, PageResultsDto<SubscribtionDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISortingService _sortingService;
        //private readonly IBlobStorageService _blobStorageService;

        public GetAllSubscribtionsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PageResultsDto<SubscribtionDto>> Handle(GetAllSubscribtionsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Subscribtion> subscribtions;
            int totalCount;

            Dictionary<Expression<Func<Subscribtion, object>>, KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>> includes = new();
            if (request.Includes)
            {
                Expression<Func<object, object>> exp1 = (sf => ((SubscribtionFeature)sf).Feature);
                includes.Add(s => s.SubscribtionsFeatures,
                    new KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>(exp1, null));
            }

            Expression<Func<Subscribtion, object>> sortingExpression = null;
            if (request.SortBy != null)
                sortingExpression = _sortingService.GetSubscribtionSortingExpression(request.SortBy);

            //if (!string.IsNullOrEmpty(request.SearchCriteria))
            //{
            //    var lowerSearchCriteria = request.SearchCriteria.Trim().ToLower();

            //    (subscribtions, totalCount) = await _unitOfWork.Subscribtions.GetAllAsync(
            //        request.PageSize, request.PageNumber,
            //        request.SortingDirection, sortingExpression);
            //}
            //else
            (subscribtions, totalCount) = await _unitOfWork.Subscribtions.GetAllAsync(
                request.PageSize, request.PageNumber, request.SortingDirection, 
                sortingExpression, null, null, includes);

            var subscribtionsDtos = _mapper.Map<IEnumerable<SubscribtionDto>>(subscribtions);
            
            return new PageResultsDto<SubscribtionDto>(subscribtionsDtos, totalCount, request.PageNumber, request.PageSize);
        }

    }
}
