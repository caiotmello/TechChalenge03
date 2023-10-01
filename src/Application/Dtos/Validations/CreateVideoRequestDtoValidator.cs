using Application.Dtos.Request;
using FluentValidation;

namespace Application.Dtos.Validations
{
    public class CreateVideoRequestDtoValidator : AbstractValidator<CreateVideoRequestDto>
    {
        public CreateVideoRequestDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage("Field Title is mandatory!");

            RuleFor(x => x.UrlVideo)
                .NotEmpty()
                .NotNull()
                .WithMessage("Field UrlVideo is mandatory!");

            RuleFor(x => x.AuthorId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Field AuthorId is mandatory!");

        }
    }
}
