using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Domain.Exceptions
{
    public class AlreadyAddedToFavouriteException:Exception
    {
        public AlreadyAddedToFavouriteException(int athleteId,int coachId)
            : base($"This coach with id: {coachId} is already added to the favourite list of athlete with id: {athleteId}") { }
       
    }
}
