using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authenticate.Commands.RegisterCommand
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(p => p.Nombre)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(p => p.ApellidoPaterno)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.ApellidoMaterno)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.Email)
                .NotEmpty()
                .MaximumLength(50)
                .EmailAddress();

            RuleFor(p => p.PerfilId)
                .NotEmpty()
                .NotNull();
        }
    }
}
