using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Coaches.Commands.AddContent
{
    public class AddContentCommandValidator:AbstractValidator<AddContentCommand>    
    {
       private readonly List<string> _validContentTypes = new List<string>
    {
        "image/jpeg",
        "image/png",
        "image/jpg",
        "application/pdf",
        "video/mp4",
        "video/webm",
        "video/ogg"
    };

    public AddContentCommandValidator()
    {
        RuleFor(dto => dto.Files)
            .NotEmpty().WithMessage("Content is required.")
            .Must(BeValidContent).WithMessage("All files must be of valid types.");
    }

    private bool BeValidContent(IEnumerable<IFormFile> files)
    {
        foreach (var file in files)
        {
            if (!_validContentTypes.Contains(file.ContentType.ToLower()))
            {
                return false;
            }
        }
        return true;
    }
    }
}
