using AthleteHub.Application.Athletes.Dtos;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AthleteHub.Application.Athletes.Commands.AddToFavourite
{
    public class AddToFavouriteCommandHandeler : IRequestHandler<AddToFavouriteCommand, AthleteFavouriteCoachDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddToFavouriteCommandHandeler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<AthleteFavouriteCoachDto> Handle(AddToFavouriteCommand request, CancellationToken cancellationToken)
        {
            var athlete = await _unitOfWork.Athletes.FindAsync(a => a.Id == request.AthleteId);
            if (athlete == null) 
                throw new NotFoundException(nameof(Athlete), request.AthleteId.ToString());
            var coach = await _unitOfWork.Coaches.FindAsync(c => c.Id == request.CoachId);
            if (coach == null)
                throw new NotFoundException(nameof(Coach), request.CoachId.ToString());
            var athletefavcoach = await _unitOfWork.AthleteFavouriteCoach
                                  .FindAsync(a=>a.AthleteId == request.AthleteId&&a.CoachId==request.CoachId);
            //Edit
            if (athletefavcoach != null)
               return new AthleteFavouriteCoachDto() { AthleteId=request.AthleteId,CoachId=request.CoachId,CanAddToFavourite=false};    

            athletefavcoach = new AthleteFavouriteCoach();
            athletefavcoach.AthleteId = request.AthleteId;
            athletefavcoach.CoachId = request.CoachId;
            await _unitOfWork.AthleteFavouriteCoach.AddAsync(athletefavcoach);
            await _unitOfWork.CommitAsync();
            var athletefavcoachDto = _mapper.Map<AthleteFavouriteCoachDto>(athletefavcoach);
            athletefavcoachDto.CanAddToFavourite = true;
            return athletefavcoachDto;
        }
    }
}
