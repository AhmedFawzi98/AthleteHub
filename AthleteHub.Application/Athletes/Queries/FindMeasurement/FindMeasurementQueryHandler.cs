using AthleteHub.Application.Athletes.Dtos;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Athletes.Queries.FindMeasurement
{
    internal class FindMeasurementQueryHandler : IRequestHandler<FindMeasurementQuery, MeasurementDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FindMeasurementQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<MeasurementDto> Handle(FindMeasurementQuery request, CancellationToken cancellationToken)
        {
            var measurement = await _unitOfWork.Measurements.FindAsync(m => m.AthleteId == request.AthleteId && m.Date == request.Date)
                ?? throw new NotFoundException(nameof(Measurement), request.AthleteId.ToString());
            var measurementDto = _mapper.Map<MeasurementDto>(measurement);
            return measurementDto;
        }
    }
}
