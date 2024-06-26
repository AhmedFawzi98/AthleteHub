using AthleteHub.Application.Coaches.Commands.UploadCertificate;
using AthleteHub.Application.Common;
using AthleteHub.Application.Services.BlobStorageService;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Users.UploadProfilePicture
{
    internal class UploadProfilePictureCommandHandler(IUnitOfWork _unitOfWork, IBlobStorageService _blobStorageService, IUserContext _userContext, UserManager<ApplicationUser> _userManager) : IRequestHandler<UploadProfilePictureCommand, FileSasUrlDto>
    {
        public async Task<FileSasUrlDto> Handle(UploadProfilePictureCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser()
            ?? throw new UnAuthorizedException();

            var userToUpdate = await _userManager.FindByIdAsync(currentUser.Id)
                ?? throw new NotFoundException(nameof(ApplicationUser), currentUser.Id!);

            using var stream = request.File.OpenReadStream();
            var fileName = $"{Guid.NewGuid().ToString()}_{request.File.FileName}";

            if (!string.IsNullOrEmpty(userToUpdate.ProfilePicture))
            {
                await _blobStorageService.DeleteBlobAsync(userToUpdate.ProfilePicture);
            }
            string profilePicture = await _blobStorageService.UploadToBlobAsync(fileName, stream, request.File.ContentType);
            userToUpdate.ProfilePicture = profilePicture;

            await _unitOfWork.CommitAsync();

            string SasLogoUrl = _blobStorageService.GetBlobSasUrl(userToUpdate.ProfilePicture);

            return new FileSasUrlDto() { SasUrl = SasLogoUrl };
        }
    }
}
