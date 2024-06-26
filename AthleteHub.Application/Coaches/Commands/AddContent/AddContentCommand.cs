using AthleteHub.Application.Coaches.Dtoes;
using AthleteHub.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Coaches.Commands.AddContent
{
    public class AddContentCommand:IRequest<AddedContentsResponseDto>
    {
        public IEnumerable<IFormFile> Files {  get; init; }
    }
}
