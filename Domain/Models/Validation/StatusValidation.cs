using FluentValidation;

namespace Domain.Models.Validation
{
    public class StatusValidation : AbstractValidator<Status>
    {
        public StatusValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O Campo {PropertyName} precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
            
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("O Campo {PropertyName} precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");            
        }
    }
}
