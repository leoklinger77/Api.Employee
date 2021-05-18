using FluentValidation;

namespace Domain.Models.Validation
{
    public class JobRoleValidation : AbstractValidator<JobRole>
    {
        public JobRoleValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("O Campo {PropertyName} precisa ser fornecido")
                .Length(2, 255).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
            
            RuleFor(c => c.Description)                
                .Length(2, 255).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
