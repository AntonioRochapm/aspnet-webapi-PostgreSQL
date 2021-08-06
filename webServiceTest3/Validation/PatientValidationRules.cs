using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webServiceTest3.Validation;

namespace webServiceTest3.Models
{
    public class PatientValidationRules : AbstractValidator<Patient>
    {
        public PatientValidationRules()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("O primeiro nome é obrigatório!")
                .Length(1,50).WithMessage("Tem que ter entre 1 e 50 caracteres!");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("O último nome é obrigatório!")
                .Length(1, 50).WithMessage("Tem que ter entre 1 e 50 caracteres!");
            RuleFor(x => x.Birthdate)
                .NotEmpty().WithMessage("A data é obrigatória!");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("A morada é obrigatória!")
                .Length(5,120).WithMessage("Tem que ter entre 5 e 120 caracteres!");
        }
    }
}
