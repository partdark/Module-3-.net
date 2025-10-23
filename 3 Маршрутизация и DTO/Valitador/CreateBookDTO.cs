using FluentValidation;

namespace _3_Маршрутизация_и_DTO.Valitador
{


    public class CreateBookDTOValidator : AbstractValidator<CreateBookDTO>
    {
        public CreateBookDTOValidator()
        {
            RuleFor(x => x.title)
                .NotEmpty()
                .WithMessage("Empty title");
            RuleFor(x => x.year)
                .GreaterThan(1900).WithMessage("Year must greate than 1900")
                .LessThanOrEqualTo(DateTime.Now.Year);

            RuleFor(x => x.author)
                .NotEmpty()
                .WithMessage("Empty author");

        }

    }
}
