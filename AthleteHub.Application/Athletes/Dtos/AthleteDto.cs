using AthleteHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AthleteHub.Application.Athletes.Dtos
{
    public class AthleteDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        [JsonIgnore]
        public string ProfilePicture { get; set; }
        public string? SasProfilePicture { get; set; }
        public string? Bio { get; set; }
        public decimal HeightInCm { get; set; }
    }
}
