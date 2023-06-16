using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Sitios.Commands.UpdateSitioCommand
{
    public class UpdateSitioCommandValidator : AbstractValidator<UpdateSitioCommand>
    {
        public UpdateSitioCommandValidator()
        {
            RuleFor(p => p.Nombre)
                    .NotEmpty().WithMessage("{PropertyName} es requerido")
                    .MinimumLength(5).WithMessage("{PropertyName} debe tener un minimo de {MinLength} caracteres")
                    .MaximumLength(20).WithMessage("{Property} no debe exceder de {MinimumLength} caracteres");

            RuleFor(p => p.Clave)
                    .NotEmpty().WithMessage("{PropertyName} es requerido")
                    .MinimumLength(5).WithMessage("{PropertyName} debe tener un minimo de {MinLength} caracteres")
                    .MaximumLength(20).WithMessage("{Property} no debe exceder de {MaximumLength} caracteres");
        }
    }
}
