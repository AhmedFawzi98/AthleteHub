using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Domain.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message = "access forbidden") : base(message)
        {
        }
    }

}
