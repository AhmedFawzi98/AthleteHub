using AthleteHub.Application.Subscribtions.Dtos;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Subscribtions.Commands.UpdateSubscribtion
{
    public class UpdateSubscribtionCommandHandler : IRequestHandler<UpdateSubscribtionCommand, SubscribtionDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateSubscribtionCommandHandler> _logger;

        public UpdateSubscribtionCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<UpdateSubscribtionCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<SubscribtionDto> Handle(UpdateSubscribtionCommand request, CancellationToken cancellationToken)
        {
            var subscribtionToBeUpdated = await _unitOfWork.Subscribtions.FindAsync(r => r.Id == request.Id)
                ?? throw new NotFoundException(nameof(Subscribtion), request.Id.ToString());

            _mapper.Map(request, subscribtionToBeUpdated);

            _unitOfWork.Subscribtions.Update(subscribtionToBeUpdated);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("UpdateSubscribtionCommand was invoked, updated subscribtion: {@updatedSubscribtion}", subscribtionToBeUpdated);

            var subscribtionDto = _mapper.Map<SubscribtionDto>(subscribtionToBeUpdated);
            return subscribtionDto;
        }
    }
}
