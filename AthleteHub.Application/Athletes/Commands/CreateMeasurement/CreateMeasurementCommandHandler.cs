using AthleteHub.Application.Athletes.Dtos;
using AthleteHub.Application.Users;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Resturants.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Athletes.Commands.CreateMeasurement
{
    public class CreateMeasurementCommandHandler : IRequestHandler<CreateMeasurementCommand, MeasurementDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        private readonly ILogger<CreateMeasurementCommandHandler> _logger;

        public CreateMeasurementCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<CreateMeasurementCommandHandler> logger, IUserContext userContext)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userContext = userContext;
        }
        public async Task<MeasurementDto> Handle(CreateMeasurementCommand command, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser() ?? throw new UnAuthorizedException();

            Dictionary<Expression<Func<Athlete, object>>, KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>> includes = new()
            {
                {c => c.Measurements, new(null,null)}
            };

            Athlete athlete = await _unitOfWork.Athletes.FindAsync(a => a.ApplicationUserId == currentUser.Id, includes) ??
              throw new NotFoundException(nameof(ApplicationUser), currentUser.Id.ToString());
            
            var lastMeasurement = athlete.Measurements.Where(m => m.AthleteId == athlete.Id && m.Date == command.Date).OrderByDescending(a=>a.Date).FirstOrDefault();

            if (lastMeasurement != null && (command.Date.ToDateTime(new TimeOnly()) - lastMeasurement.Date.ToDateTime(new TimeOnly())).TotalDays < 7)
            {
                throw new InvalidOperationException("It must be at least 7 days since the last measurement.");
            }

            var newMeasurement = _mapper.Map<Measurement>(command);

            await _unitOfWork.Measurements.AddAsync(newMeasurement);
            await _unitOfWork.CommitAsync();
            var addedRestuarant = _mapper.Map<MeasurementDto>(newMeasurement);

            //_logger.LogInformation("Create Measurement Command Was Invoked by {username} with id = {userid}, Created restaurant: {@newRestaurant}", currentUser.Name, currentUser.Id, newMeasurement);

            return addedRestuarant;
        }
    }
}
