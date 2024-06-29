using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Admin.Commands.CoachStatus
{
    public class CoachStatusCommand:IRequest<string>
    {
        public int CoachId { get; init; }
        public bool IsSuspend { get; init; }
    }
}
