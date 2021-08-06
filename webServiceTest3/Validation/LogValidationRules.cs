using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webServiceTest3.Models;

namespace webServiceTest3.Validation
{
    public class LogValidationRules : AbstractValidator<Log>
    {
        public LogValidationRules()
        {
            RuleFor(x => x.Level)
                .MaximumLength(128).WithMessage("Máximo de 128 caracteres para o Level!");
            RuleFor(x => x.TimeStamp)
               .NotNull().WithMessage("O timestamp é obrigatório!");
            RuleFor(x => x.UserName)
                .MaximumLength(200).WithMessage("Máximo de 200 caracters para o UserName!");
            RuleFor(x => x.Ip)
                .MaximumLength(200).WithMessage("Máximo de 200 caracteres para o Ip!");
        }
        
    }
}
