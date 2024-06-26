using AthleteHub.Application.Users;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Enums;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AthleteHub.Application.Athletes.Commands.DeleteMeasurement
{
    public class DeleteMeasurementCommandHandler(IUserContext _userContext, IUnitOfWork _unitOfWork, ILogger<DeleteMeasurementCommandHandler> _logger)
        : IRequestHandler<DeleteMeasurementCommand>
    {
        public async Task Handle(DeleteMeasurementCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser() ?? throw new UnAuthorizedException();

            
            Dictionary<Expression<Func<Athlete, object>>, KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>> includes = new()
            {
                {a =>a.Measurements, new(null,null)}
            };
            Athlete athlete = await _unitOfWork.Athletes.FindAsync(a => a.ApplicationUserId == currentUser.Id, includes)??
                throw new NotFoundException(nameof(ApplicationUser),currentUser.Id.ToString());


            var measurementToBeDeleted = athlete.Measurements.Where(m=>m.AthleteId == athlete.Id&&m.Date == request.Date).FirstOrDefault();


            _unitOfWork.Measurements.Delete(measurementToBeDeleted);
            await _unitOfWork.CommitAsync();
            _logger.LogInformation("Delete Measurement Command Was executed, deleted restaurant: {@deletedRestaurant}", measurementToBeDeleted);
        }
    }
}
