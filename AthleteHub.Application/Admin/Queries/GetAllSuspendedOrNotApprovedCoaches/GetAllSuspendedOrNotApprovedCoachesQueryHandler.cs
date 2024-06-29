using AthleteHub.Application.Coaches.Dtoes;
using AthleteHub.Application.Services.BlobStorageService;
using AthleteHub.Application.Users;
using AthleteHub.Domain.Constants;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Resturants.Application.Common;
using Resturants.Domain.Enums;
using System.Linq.Expressions;

namespace AthleteHub.Application.Admin.Queries.GetAllSuspendedOrNotApprovedCoaches
{
    public class GetAllSuspendedOrNotApprovedCoachesQueryHandler(IUnitOfWork _unitOfWork, IUserContext _userContext, IMapper _mapper,IBlobStorageService _blobStorageService) : IRequestHandler<GetAllSuspendedOrNotApprovedCoachesQuery, PageResultsDto<CoachDto>>
    {
        public async Task<PageResultsDto<CoachDto>> Handle(GetAllSuspendedOrNotApprovedCoachesQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (!currentUser.IsInRole(RolesConstants.Admin))
                throw new UnAuthorizedException();

            IEnumerable<Coach> Coaches=null;

            int totalCount=0;
            Dictionary<Expression<Func<Coach, object>>, KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>> includes = new()
            {
                {c=>c.ApplicationUser , new (null,null) }
            };

            if(request.SuspendedCoaches)
                (Coaches, totalCount) = await _unitOfWork.Coaches.GetAllAsync(request.PageSize, request.PageNumber,
                                          SortingDirection.Ascending, null, null, c => c.IsSuspended == true, includes);
            else if(request.NotApprovedCoaches)
                (Coaches, totalCount) = await _unitOfWork.Coaches.GetAllAsync(request.PageSize, request.PageNumber,
                                          SortingDirection.Ascending, null, null, c => c.IsApproved == false, includes);

            var coachesDtos = _mapper.Map<IEnumerable<CoachDto>>(Coaches);

            //foreach (var dto in coachesDtos)
            //{
            //    if (!string.IsNullOrEmpty(dto.ProfilePicture))
            //    {
            //        dto.SasProfilePicture = _blobStorageService.GetBlobSasUrl(dto.ProfilePicture);
            //    }
            //    if (!string.IsNullOrEmpty(dto.Certificate))
            //    {
            //        dto.SasCertificate = _blobStorageService.GetBlobSasUrl(dto.Certificate);
            //    }
            //}

            return new PageResultsDto<CoachDto>(coachesDtos, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
