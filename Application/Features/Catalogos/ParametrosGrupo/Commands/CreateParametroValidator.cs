using Application.Interfaces.IRepositories;
using FluentValidation;

namespace Application.Features.Catalogos.ParametrosGrupo.Commands
{
    public class CreateParametroValidator : AbstractValidator<CreateParametro>
    {
        private readonly IParametroRepository _parametroRepository;
        public CreateParametroValidator(IParametroRepository parametroRepository)
        {
            _parametroRepository = parametroRepository;

            RuleFor(x => x.Clave.Trim()).NotEmpty().WithMessage("La clave es requerida");
            RuleFor(x => x.Descripcion.Trim()).NotEmpty().WithMessage("La descripción es requerida");
        }
    }
}
