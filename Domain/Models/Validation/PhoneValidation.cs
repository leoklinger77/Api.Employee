using Domain.Models.Enumerations;
using FluentValidation;

namespace Domain.Models.Validation
{
    public class PhoneValidation : AbstractValidator<Phone>
    {
        public PhoneValidation()
        {
            RuleFor(x => x.Ddd)
                .NotEmpty().WithMessage("O Campo {PropertyName} precisa ser fornecido")
                .Length(2, 2).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(x => x.PhoneType == PhoneType.Residencial, () =>
             {
                 RuleFor(x => x.Number.Length).Equal(8).WithMessage("O campo Número precisa ter 8 caracteres para telefone residencial e foi fornecido {PropertyValue}.");
             });

            When(x => x.PhoneType == PhoneType.Celular, () =>
            {
                RuleFor(x => x.Number.Length).Equal(9).WithMessage("O campo Número precisa ter 9 caracteres para telefone celular e foi fornecido {PropertyValue}.");
            });

            When(x => x.PhoneType == PhoneType.Comercial, () =>
            {
                RuleFor(x => x.Number)
                    .Length(8, 9).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres para telefones comercial");
            });
        }
    }
}
