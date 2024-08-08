using FluentValidation;

namespace Application.Features.Catalogos.ParametrosGrupo.Commands
{
    public class CreateParametroValidator : AbstractValidator<CreateParametro>
    {
        public CreateParametroValidator()
        {
            RuleFor(x => x.Clave.Trim()).NotEmpty().WithMessage("La clave es requerida");
            RuleFor(x => x.Descripcion.Trim()).NotEmpty().WithMessage("La descripción es requerida");
        }
    }
}
