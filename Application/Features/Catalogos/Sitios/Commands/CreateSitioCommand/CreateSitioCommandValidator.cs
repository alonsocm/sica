using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Sitios.Commands.CreateSitioCommand
{
    public class CreateSitioCommandValidator : AbstractValidator<CreateSitioCommand>
    {
        public CreateSitioCommandValidator()
        {
            RuleFor(p => p.NombreSitio)
                    .NotEmpty().WithMessage("{PropertyName} es requerido")
                    .MinimumLength(5).WithMessage("{PropertyName} debe tener un minimo de {MinLength} caracteres")
                    .MaximumLength(20).WithMessage("{Property} no debe exceder de {MinimumLength} caracteres");

            RuleFor(p => p.ClaveSitio)
                    .NotEmpty().WithMessage("{PropertyName} es requerido")
                    .MinimumLength(5).WithMessage("{PropertyName} debe tener un minimo de {MinLength} caracteres")
                    .MaximumLength(20).WithMessage("{Property} no debe exceder de {MaximumLength} caracteres");
        }
    }
}
