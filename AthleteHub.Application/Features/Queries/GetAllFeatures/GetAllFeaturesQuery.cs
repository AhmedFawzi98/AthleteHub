using AthleteHub.Application.Features.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Features.Queries.GetAllFeatures
{
    public class GetAllFeaturesQuery:IRequest<GetAllFeaturesResponseDto>
    {

    }
}
