using AthleteHub.Application.Coaches.Dtoes;
using AthleteHub.Application.Services.BlobStorageService;
using AthleteHub.Application.Users;
using AthleteHub.Domain.Constants;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Enums;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Coaches.Commands.AddContent
{
    public  class AddContentCommandHandle(IUnitOfWork _unitOfWork, IBlobStorageService _blobStorageService, IUserContext _userContext) : IRequestHandler<AddContentCommand, AddedContentsResponseDto>
    {
        private readonly Dictionary<string, ContentType> _fileTypeToContentType = new Dictionary<string, ContentType>
        {
            { "image/jpeg", ContentType.Picture },
            { "image/png", ContentType.Picture },
            { "image/jpg", ContentType.Picture },
            { "application/pdf", ContentType.Pdf },
            { "video/mp4", ContentType.Video },
            { "video/webm", ContentType.Video },
            { "video/ogg", ContentType.Video }
        };
        public async Task<AddedContentsResponseDto> Handle(AddContentCommand request, CancellationToken cancellationToken)
        {
            var currentUser= _userContext.GetCurrentUser() ?? throw new UnAuthorizedException();
            Dictionary<Expression<Func<Coach, object>>, KeyValuePair<Expression<Func<object, object>>, Expression<Func<object, object>>>> includes = new()
            {
                {c=>c.Contents ,new (null,null) }
            };

            var coach = await _unitOfWork.Coaches.FindAsync(c => c.ApplicationUserId == currentUser.Id, includes) ?? throw new NotFoundException(nameof(Coach), currentUser.Id);

            var addedContentResponseDto = new AddedContentsResponseDto() { ContentsDtos=new()};
            
            foreach (var file in request.Files)
            {
               var (contenDto,url)=await UploadFileAsync(file);
               addedContentResponseDto.ContentsDtos.Add(contenDto);
               await SaveInDataBaseAsync(coach, url, contenDto.ContentType);
            }

            return addedContentResponseDto;

        }
        private async Task<(ContentDto,string)> UploadFileAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var fileName = $"{Guid.NewGuid().ToString()}";
            string url = await _blobStorageService.UploadToBlobAsync(fileName, stream, file.ContentType);
            string SasContentUrl = _blobStorageService.GetBlobSasUrl(url);

            return (new ContentDto()
            {
                ContentType = _fileTypeToContentType[file.ContentType],
                SasUrl = SasContentUrl
            },url); 

        }
        private async Task SaveInDataBaseAsync(Coach coach,string url,ContentType contentType)
        {
            Content addedContent = new Content()
            {
                CoachId = coach.Id,
                ContentType=contentType,
                Url=url,
            };
            coach.Contents.Add(addedContent);
            _unitOfWork.Coaches.Update(coach);
            await _unitOfWork.CommitAsync();
        }
    }
}
