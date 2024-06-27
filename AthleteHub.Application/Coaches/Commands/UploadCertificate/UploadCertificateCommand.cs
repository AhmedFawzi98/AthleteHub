

using AthleteHub.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AthleteHub.Application.Coaches.Commands.UploadCertificate
{
    public class UploadCertificateCommand:IRequest<FileSasUrlDto>
    {

        public int CoachId { get; private set; }
        public IFormFile File { get; init; }

        public void SetId(int id)
        {
            CoachId = id;
        }
    }
}
