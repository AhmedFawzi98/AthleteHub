﻿using AthleteHub.Application.Subscribtions.Dtos;
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

namespace AthleteHub.Application.Subscribtions.Queries.FindSubscribtion
{
    public class FindSubscribtionQueryHandler : IRequestHandler<FindSubscribtionQuery,SubscribtionDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FindSubscribtionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SubscribtionDto> Handle(FindSubscribtionQuery request, CancellationToken cancellationToken)
        {
            string[] includes = null!;
            if (request.Includes)
                includes = ["SubscribtionsFeatures"];

            var subscribtion = await _unitOfWork.Subscribtions.FindAsync(request.Criteria, includes) ?? throw new NotFoundException(nameof(Subscribtion), request.Id.ToString());

            var subscribtionDto = _mapper.Map<SubscribtionDto>(subscribtion);

            return subscribtionDto;
        }
    }
}
