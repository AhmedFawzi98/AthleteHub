

using AthleteHub.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AthleteHub.Application.Users.UploadProfilePicture
{
    public class UploadProfilePictureCommand : IRequest<FileSasUrlDto>
    {
        public IFormFile File { get; init; }
    }
    
}
