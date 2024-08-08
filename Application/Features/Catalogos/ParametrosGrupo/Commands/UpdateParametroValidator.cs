using FluentValidation;

namespace Application.Features.Catalogos.ParametrosGrupo.Commands
{
    public class UpdateParametroValidator : AbstractValidator<UpdateParametro>
    {
        public UpdateParametroValidator()
        {
            RuleFor(x => x).SetValidator(new CreateParametroValidator());
        }
    }
}
