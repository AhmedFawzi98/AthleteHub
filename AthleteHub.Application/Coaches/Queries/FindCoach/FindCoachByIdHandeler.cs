using AthleteHub.Application.Coaches.Dtoes;
using AthleteHub.Application.Coaches.Queries.GetAllCoaches;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
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

namespace AthleteHub.Application.Coaches.Queries.FindCoach
{
    public class FindCoachByIdHandeler : IRequestHandler<FindCoachByIdQuery, CoachDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FindCoachByIdHandeler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CoachDto> Handle(FindCoachByIdQuery request, CancellationToken cancellationToken)
        {
            Dictionary<Expression<Func<Coach, object>>, KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>> includes = new()
            {
                {c=>c.ApplicationUser ,
                             new KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>(null,null) },
                {c=>c.Subscribtions, new KeyValuePair<Expression<Func<object, object>>,
                                              Expression<Func<object, object>>>(s=>((Subscribtion)s).SubscribtionsFeatures,null)}
            };
            Expression<Func<object, object>> exp1 = (cr => ((CoachRating)cr).Athlete);
            Expression<Func<object, object>> exp2 = (a => ((Athlete)a).ApplicationUser);
            includes.Add(c => c.CoachesRatings,
                new KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>(exp1, exp2));

            
            var coach = await _unitOfWork.Coaches.FindAsync(c => c.Id==request.Id, includes);
            
            if (coach == null) throw new NotFoundException(nameof(Coach),request.Id.ToString()); 
            
            var coatchDto = _mapper.Map<CoachDto>(coach);
            return coatchDto;
        }
        
    }
}
