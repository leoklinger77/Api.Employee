using FluentValidation;

namespace Domain.Models.Validation
{
    public class EmailValidation : AbstractValidator<Email>
    {
        public EmailValidation()
        {
            RuleFor(x=>x.EmailAddress)
                .NotEmpty().WithMessage("O Campo {PropertyName} precisa ser fornecido")
                .Length(5, 255).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
