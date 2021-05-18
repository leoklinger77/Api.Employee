using Domain.Models.Validation.Documents;
using FluentValidation;

namespace Domain.Models.Validation
{
    public class EmployeeValidation : AbstractValidator<Employee>
    {
        public EmployeeValidation()
        {            
            RuleFor(c => c.FullName)                
                .NotEmpty().WithMessage("O Campo {PropertyName} precisa ser fornecido")
                .Length(10, 255).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage("O Campo {PropertyName} precisa ser fornecido")
                .Length(11, 11).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
            RuleFor(f => ValidationCpf.IsCpf(f.Cpf)).Equal(true)
                    .WithMessage("O Cpf fornecido é inválido.");
            
            RuleFor(c => c.Rg)                
                .Length(7, 10).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");                 

        }
    }
}
