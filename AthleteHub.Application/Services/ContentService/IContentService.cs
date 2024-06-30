using AthleteHub.Application.Coaches.Dtoes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Services.ContentService
{
    public interface IContentService
    {
        Task<(ContentDto, string)> UploadFileAsync(IFormFile file);
    }
}
