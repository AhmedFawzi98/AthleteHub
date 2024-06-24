using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Subscribtions.Commands.DeleteSubscribtion
{
    public class DeleteSubscribtionCommandHandler : IRequestHandler<DeleteSubscribtionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteSubscribtionCommandHandler> _logger;

        public DeleteSubscribtionCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteSubscribtionCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task Handle(DeleteSubscribtionCommand request, CancellationToken cancellationToken)
        {
            var subscribtionToBeDeleted = await _unitOfWork.Subscribtions.FindAsync(r => r.Id == request.Id)
                ?? throw new NotFoundException(nameof(Subscribtion), request.Id.ToString());

            _unitOfWork.Subscribtions.Delete(subscribtionToBeDeleted);
            await _unitOfWork.CommitAsync();
            _logger.LogInformation("DeleteSubscribtionCommand was invoked, deleted subscribtion: {@deletedSubscribtion}", subscribtionToBeDeleted);
        }
    }
}
