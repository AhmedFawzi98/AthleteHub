using AthleteHub.Application.Athletes.Dtos;
using AthleteHub.Application.Services.BlobStorageService;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;


namespace AthleteHub.Application.Athletes.Queries.FindAthleteById
{
    public class FindAthleteByIdHandeler(IUnitOfWork _unitOfWork, IMapper _mapper, IBlobStorageService _blobStorageService) : IRequestHandler<FindAthleteByIDQuery, AthleteDto>
    {
        public async Task<AthleteDto> Handle(FindAthleteByIDQuery request, CancellationToken cancellationToken)
        {
            Dictionary<Expression<Func<Athlete, object>>, KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>> includes = new()
            {
                {c=>c.ApplicationUser ,new (null,null) }
            };
            

            var athlete = await _unitOfWork.Athletes.FindAsync(a=>a.Id==request.Id, includes);

            if (athlete == null) throw new NotFoundException(nameof(Athlete), request.Id.ToString());


            var athleteDto = _mapper.Map<AthleteDto>(athlete);
            if (!string.IsNullOrEmpty(athleteDto.ProfilePicture))
            {
                athleteDto.SasProfilePicture = _blobStorageService.GetBlobSasUrl(athleteDto.ProfilePicture);
            }
            
            return athleteDto;
        }
    }
}
