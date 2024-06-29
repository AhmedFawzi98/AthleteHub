using AthleteHub.Application.Subscribtions.Dtos;
using AthleteHub.Domain.Enums;
using MediatR;
using Resturants.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Athletes.Queries.GetAthleteSubscribtion
{
    public class GetAthleteSubscribtionQuery : IRequest<List<SubscribtionDto>>
    {
        public bool IncludeSubscribtionFeature { get; init; }
        public SortBy? SortByCritrea { get; init; } = SortBy.Price;
        public SortingDirection SortingDirection { get; init; } = SortingDirection.Ascending;
    }
}
