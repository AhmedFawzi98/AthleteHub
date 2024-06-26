

using AthleteHub.Application.Common;
using AthleteHub.Application.Services.BlobStorageService;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using MediatR;

namespace AthleteHub.Application.Coaches.Commands.UploadCertificate
{
    public class UploadCertificateCommandHandler(IUnitOfWork _unitOfWork,IBlobStorageService _blobStorageService) : IRequestHandler<UploadCertificateCommand, FileSasUrlDto>
    {
        public async Task<FileSasUrlDto> Handle(UploadCertificateCommand request, CancellationToken cancellationToken)
        {
            var coachToUpdate = await _unitOfWork.Coaches.FindAsync(c => c.Id == request.CoachId) 
                  ?? throw new NotFoundException(nameof(Coach), request.CoachId.ToString());

            using var stream = request.File.OpenReadStream();
            var fileName = $"{Guid.NewGuid().ToString()}_{request.File.FileName}";

            if (!string.IsNullOrEmpty(coachToUpdate.Certificate))
            {
                await _blobStorageService.DeleteBlobAsync(coachToUpdate.Certificate);
            }
            string certificate = await _blobStorageService.UploadToBlobAsync(fileName, stream, request.File.ContentType);
            coachToUpdate.Certificate = certificate;

            await _unitOfWork.CommitAsync();

            string SasLogoUrl = _blobStorageService.GetBlobSasUrl(coachToUpdate.Certificate);

            return new FileSasUrlDto() { SasUrl = SasLogoUrl };
        }
        
    }
}
