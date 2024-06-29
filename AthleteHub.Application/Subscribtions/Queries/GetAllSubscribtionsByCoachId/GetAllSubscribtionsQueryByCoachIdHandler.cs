using AthleteHub.Application.Services.SortingService;
using AthleteHub.Application.Subscribtions.Dtos;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Resturants.Application.Common;
using Resturants.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Subscribtions.Queries.GetAllSubscribtionsByCoachId
{
    public class GetAllSubscribtionsQueryByCoachIdHandler : IRequestHandler<GetAllSubscribtionsQueryByCoachId, PageResultsDto<SubscribtionDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISortingService _sortingService;
        

        public GetAllSubscribtionsQueryByCoachIdHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PageResultsDto<SubscribtionDto>> Handle(GetAllSubscribtionsQueryByCoachId request, CancellationToken cancellationToken)
        {
            IEnumerable<Subscribtion> subscribtions;
            int totalCount;
            var coach = await _unitOfWork.Coaches.FindAsync(c => c.Id == request.CoachId)??
                throw new NotFoundException(nameof(Coach),request.CoachId.ToString());
            Dictionary<Expression<Func<Subscribtion, object>>, KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>> includes = new();
            if (request.Includes)
            {
                Expression<Func<object, object>> exp1 = (sf => ((SubscribtionFeature)sf).Feature);
                includes.Add(s => s.SubscribtionsFeatures,
                    new KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>(exp1, null));
            }

            Expression<Func<Subscribtion, bool>> criteria = s => s.CoachId == request.CoachId;
            (subscribtions, totalCount) = await _unitOfWork.Subscribtions.GetAllAsync(
                request.PageSize, request.PageNumber, SortingDirection.Ascending, 
                null, null, criteria, includes);

            var subscribtionsDtos = _mapper.Map<IEnumerable<SubscribtionDto>>(subscribtions);
            
            return new PageResultsDto<SubscribtionDto>(subscribtionsDtos, totalCount, request.PageNumber, request.PageSize);
        }

    }
}
