using AthleteHub.Application.Subscribtions.Dtos;
using AthleteHub.Application.Users;
using AthleteHub.Domain.Constants;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Subscribtions.Commands.CreateSubscribtion
{
    public class CreateSubscribtionCommandHandler : IRequestHandler<CreateSubscribtionCommand, SubscribtionDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        private readonly ILogger<CreateSubscribtionCommandHandler> _logger;

        public CreateSubscribtionCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IUserContext userContext, ILogger<CreateSubscribtionCommandHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _logger = logger;
        }
        public async Task<SubscribtionDto> Handle(CreateSubscribtionCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser() ??
                throw new UnAuthorizedException();

            var coach = await _unitOfWork.Coaches.FindAsync(c => c.ApplicationUserId == currentUser.Id) ??
                throw new NotFoundException(nameof(Coach), currentUser.Id.ToString());

            var newSubscribtion = _mapper.Map<Subscribtion>(request);
            newSubscribtion.CoachId = coach.Id;
            foreach (var featureDto in request.SubscribtionsFeatures)
            {
                var subscribtionFeature = new SubscribtionFeature
                {
                    FeatureId = featureDto.FeatureId,
                    Subscribtion = newSubscribtion
                };
                newSubscribtion.SubscribtionsFeatures.Add(subscribtionFeature);
            }

            await _unitOfWork.Subscribtions.AddAsync(newSubscribtion);
            await _unitOfWork.CommitAsync();

            var addedSubscribtion = _mapper.Map<SubscribtionDto>(newSubscribtion);
            _logger.LogInformation("CreateSubscribtionCommand was invoked by {username} with id = {userid}, Created restaurant: {@newSubscribtion}", currentUser.Name, currentUser.Id, newSubscribtion);

            return addedSubscribtion;
        }
    }
}
