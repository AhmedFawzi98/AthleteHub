using AthleteHub.Application.Athletes.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Athletes.Queries.FindAthleteById
{
    public class FindAthleteByIDQuery:IRequest<AthleteDto>
    {
        public int Id { get; set; } 
    }
}
