using AthleteHub.Application.Subscribtions.Dtos;
using AthleteHub.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Subscribtions.Queries.FindSubscribtion
{
    public class FindSubscribtionQuery : IRequest<SubscribtionDto>
    {
        public int Id { get; set; }
        public bool Includes { get; set; }
        public Expression<Func<Subscribtion, bool>> Criteria { get; set; }
    }
}
