using AthleteHub.Application.Coaches.Dtoes;
using AthleteHub.Application.Coaches.Queries.GetAllCoaches;
using AthleteHub.Application.Services.BlobStorageService;
using AthleteHub.Application.Users;
using AthleteHub.Domain.Constants;
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
        private readonly IBlobStorageService _blobStorageService;
        private readonly IUserContext _userContext;
        public FindCoachByIdHandeler(IUnitOfWork unitOfWork, IMapper mapper, IBlobStorageService blobStorageService,IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _blobStorageService = blobStorageService;
            _userContext =userContext;
        }

        public async Task<CoachDto> Handle(FindCoachByIdQuery request, CancellationToken cancellationToken)
        {
            Dictionary<Expression<Func<Coach, object>>, KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>> includes = new()
            {
                {c=>c.ApplicationUser ,
                             new KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>(null,null) },
                {c=>c.Subscribtions, new KeyValuePair<Expression<Func<object, object>>,
                                              Expression<Func<object, object>>>(s=>((Subscribtion)s).SubscribtionsFeatures,null)},
                
            };
            Expression<Func<object, object>> exp1 = (cr => ((CoachRating)cr).Athlete);
            Expression<Func<object, object>> exp2 = (a => ((Athlete)a).ApplicationUser);
            includes.Add(c => c.CoachesRatings,
                new KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>(exp1, exp2));

            var currentUser = _userContext.GetCurrentUser();
            Athlete currentAthlete = null;
            
            if (currentUser != null && currentUser.IsInRole(RolesConstants.Athlete))
            {
                currentAthlete = await _unitOfWork.Athletes.FindAsync(a => a.ApplicationUserId == currentUser.Id);
            }
            Expression<Func<Coach, bool>> critrea = null;
            if (currentAthlete != null)
                critrea = c => c.Id == request.Id && !c.CoachesBlockedAthletees.Any(cba => cba.AthleteId == currentAthlete.Id);
            else
                critrea = c => c.Id == request.Id;

            var coach = await _unitOfWork.Coaches.FindAsync(critrea, includes);
            
            if (coach == null) throw new NotFoundException(nameof(Coach),request.Id.ToString()); 
            

            var coatchDto = _mapper.Map<CoachDto>(coach);
            if (!string.IsNullOrEmpty(coatchDto.ProfilePicture))
            {
                coatchDto.SasProfilePicture = _blobStorageService.GetBlobSasUrl(coatchDto.ProfilePicture);
            }
            if (!string.IsNullOrEmpty(coatchDto.Certificate))
            {
                coatchDto.SasCertificate = _blobStorageService.GetBlobSasUrl(coatchDto.Certificate);
            }
            return coatchDto;
        }
        
    }
}
