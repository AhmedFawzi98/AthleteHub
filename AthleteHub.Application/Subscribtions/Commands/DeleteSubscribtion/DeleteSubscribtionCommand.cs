using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Subscribtions.Commands.DeleteSubscribtion
{
    public class DeleteSubscribtionCommand : IRequest
    {
        public int Id { get; init; }
    }
}
