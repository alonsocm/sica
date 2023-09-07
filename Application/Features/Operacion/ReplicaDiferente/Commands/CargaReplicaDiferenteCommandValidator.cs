using Application.Interfaces.IRepositories;
using FluentValidation;

namespace Application.Features.ReplicaDiferente.Commands
{
    public class CargaReplicaDiferenteCommandValidator : AbstractValidator<CargaReplicaDiferenteCommand>
    {
        private readonly IVwReplicaRevisionResultadoRepository _vwReplicaRevisionResultado;

        public CargaReplicaDiferenteCommandValidator(IVwReplicaRevisionResultadoRepository vwReplicaRevisionResultado)
        {
            _vwReplicaRevisionResultado = vwReplicaRevisionResultado;





            RuleForEach(x => x.Resultados).ChildRules(resultado =>
            {
                resultado.RuleFor(x => x.NoEntrega).Cascade(CascadeMode.Stop)
                                           .NotEmpty().WithMessage(resultado => $"El campo {{PropertyName}} no puede estar vacío. Linea: {resultado.Linea}")
                                           .Must((resultado, noEntrega) => { return ExisteClaveUnica(resultado.ClaveUnica, noEntrega).Result; })
                                           .WithMessage(resultado => $"Los datos cargados {{PropertyValue}} no se encontran en la BD. Linea:{resultado.Linea}");



            });
        }

        public async Task<bool> ExisteClaveUnica(string claveUnica, string noEntrega)
        {
            return await _vwReplicaRevisionResultado.ExisteElementoAsync(x => x.NumeroEntrega == Convert.ToInt32(noEntrega) && x.ClaveUnica == claveUnica);
        }

    }
}