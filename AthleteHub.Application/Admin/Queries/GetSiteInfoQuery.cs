using AthleteHub.Application.Athletes.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Admin.Queries
{
    public class GetSiteInfoQuery : IRequest<SiteInfoDto>
    {
    }
}
