using AthleteHub.Application.Athletes.Dtos;
using AthleteHub.Domain.Enums;
using MediatR;
using Resturants.Application.Common;
using Resturants.Domain.Enums;


namespace AthleteHub.Application.Athletes.Queries.GetAllAthletes
{
    public class GetAllAthletesQuery: IRequest<PageResultsDto<AthleteDto>>
    {
        public string? SearchCritrea { get; init; }
        public Gender? GenderFilterCritrea { get; init; }
        public AgeFilter? AgeFilterCritrea { get; init; }
        public int PageSize { get; init; }
        public int PageNumber { get; init; }
    }
}
