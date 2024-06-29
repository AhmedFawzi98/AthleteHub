using AthleteHub.Application.Athletes.Dtos;
using AthleteHub.Application.Coaches.Dtoes;
using AthleteHub.Application.Coaches.Queries.GetAllCoaches;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Athletes.Queries.GetAllMeasurement
{
    public class GetAllMeasurementssQueryHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IFilterService _filterService,
        ISearchService _searchService, ISortService _sortService, IBlobStorageService _blobStorageService, IUserContext _usercontext)
        : IRequestHandler<GetAllMeasurementssQuery, PageResultsDto<MeasurementDto>>
    {
        public async Task<PageResultsDto<MeasurementDto>> Handle(GetAllMeasurementssQuery request, CancellationToken cancellationToken)
        {
            var (Measurement, totalCount) = await _unitOfWork.Measurements.GetAllAsync(request.PageSize, request.PageNumber, request.SortingDirection
                , null, null, a => a.AthleteId == request.AthleteId);


            var MeasurementDtos = _mapper.Map<IEnumerable<MeasurementDto>>(Measurement);

            return new PageResultsDto<MeasurementDto>(MeasurementDtos, totalCount, request.PageNumber, request.PageSize);

        }
    }
}
