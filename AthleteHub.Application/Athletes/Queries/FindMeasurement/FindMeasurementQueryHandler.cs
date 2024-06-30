using AthleteHub.Application.Athletes.Dtos;
using AthleteHub.Application.Users;
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
    internal class FindMeasurementQueryHandler(IMapper _mapper, IUnitOfWork _unitOfWork, IUserContext _userContext) : IRequestHandler<FindMeasurementQuery, MeasurementDto>
    {
        public async Task<MeasurementDto> Handle(FindMeasurementQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser() ?? throw new UnAuthorizedException();

            var athleteMeasurements = await _unitOfWork.Measurements.GetAllAsync(m => m.AthleteId == request.AthleteId)
                ?? throw new NotFoundException(nameof(Measurement), request.AthleteId.ToString());
            var measurement = athleteMeasurements.OrderByDescending(m => m.Date).FirstOrDefault();
            var measurementDto = _mapper.Map<MeasurementDto>(measurement);
            return measurementDto;
        }
    }
}
