using Application.Interfaces.IRepositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Replicas.Commands.ReplicasTotal
{
    public class CargarRevisionLNRValidator : AbstractValidator<CargarRevisionLNR>
    {
        private readonly IVwReplicaRevisionResultadoRepository _replicasRepository;
        public CargarRevisionLNRValidator(IVwReplicaRevisionResultadoRepository replicasRepository)
        {
            _replicasRepository = replicasRepository;

            RuleForEach(x => x.Replicas).ChildRules(replica =>
            {
                replica.RuleFor(x => x.ClaveUnica).Cascade(CascadeMode.Stop)
                                                  .NotEmpty().WithMessage(replica => $"La campo {{PropertyName}} no puede estar vacio. Linea: {replica.Linea}")
                                                  .Must((replica, claveUnica) => _replicasRepository.ExisteElementoAsync(x => x.ClaveUnica == claveUnica).Result)
                                                  .WithMessage((replica) => $"La clave única {{PropertyValue}} no se encontró registrada. Linea: {replica.Linea}");
            });
        }
    }
}
