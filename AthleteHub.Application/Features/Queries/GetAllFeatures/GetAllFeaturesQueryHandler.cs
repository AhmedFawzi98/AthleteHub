using AthleteHub.Application.Features.Dtos;
using AthleteHub.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Features.Queries.GetAllFeatures
{
    public class GetAllFeaturesQueryHandler(IUnitOfWork _unitOfWork,IMapper _mapper) : IRequestHandler<GetAllFeaturesQuery, GetAllFeaturesResponseDto>
    {
        public async Task<GetAllFeaturesResponseDto> Handle(GetAllFeaturesQuery request, CancellationToken cancellationToken)
        {
            var features = await _unitOfWork.Features.GetAllAsync();
            var featuresDtos= _mapper.Map<IEnumerable<FeatureDto>>(features);
            var getAllFeaturesResponseDto = new GetAllFeaturesResponseDto() { Features=featuresDtos.ToList()};
            return getAllFeaturesResponseDto;
        }
    }
}
