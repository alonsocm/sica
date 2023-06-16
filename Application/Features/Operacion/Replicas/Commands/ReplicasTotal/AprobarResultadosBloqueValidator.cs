using Application.Interfaces.IRepositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Replicas.Commands.ReplicasTotal
{
    public class AprobarResultadosBloqueValidator : AbstractValidator<AprobarResultadosBloque>
    {
        private readonly IVwReplicaRevisionResultadoRepository _replicasRepository;
        public AprobarResultadosBloqueValidator(IVwReplicaRevisionResultadoRepository replicasRepository)
        {
            //_replicasRepository = replicasRepository;

            //RuleForEach(x => x.ClavesUnicas).ChildRules(replica =>
            //{
            //    replica.RuleFor(x => x).Cascade(CascadeMode.Stop)
            //                                      .Must((replica, claveUnica) => _replicasRepository.ExisteElemento(x => x.ClaveUnica == claveUnica).Result)
            //                                      .WithMessage((replica) => $"La clave única {{PropertyValue}} no se encontró registrada");
            //});
        }
    }
}
