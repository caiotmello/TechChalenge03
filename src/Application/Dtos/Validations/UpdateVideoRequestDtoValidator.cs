using Application.Dtos.Request;
using FluentValidation;

namespace Application.Dtos.Validations
{
    public class UpdateVideoRequestDtoValidator : AbstractValidator<UpdateVideoRequestDto>
    {
        public UpdateVideoRequestDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Field Id is mandatory!");
        }
    }
}
