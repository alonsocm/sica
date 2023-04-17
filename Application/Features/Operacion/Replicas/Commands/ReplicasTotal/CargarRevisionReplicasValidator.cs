using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Replicas.Commands.ReplicasTotal
{
    public class CargarRevisionReplicasValidator : AbstractValidator<CargarRevisionReplicas>
    {
        private readonly IVwReplicaRevisionResultadoRepository _replicasRepository;
        public CargarRevisionReplicasValidator(IVwReplicaRevisionResultadoRepository replicasRepository)
        {
            _replicasRepository = replicasRepository;

            RuleForEach(x => x.Replicas).ChildRules(replica =>
            {
                replica.RuleFor(x => x.ClaveUnica).Cascade(CascadeMode.Stop)
                                                  .NotEmpty().WithMessage(replica => $"La campo {{PropertyName}} no puede estar vacio. Linea: {replica.Linea}")
                                                  .Must((replica, claveUnica) => _replicasRepository.ExisteElemento(x => x.ClaveUnica == claveUnica).Result)
                                                  .WithMessage((replica) => $"La clave única {{PropertyValue}} no se encontró registrada. Linea: {replica.Linea}");

                replica.RuleFor(x => x.ResultadoReplica).NotEmpty().WithMessage(replica => $"La campo {{PropertyName}} no puede estar vacio. Linea: {replica.Linea}");

                replica.RuleFor(x => x.SeAceptaRechazo).NotEmpty().WithMessage(replica => $"La campo {{PropertyName}} no puede estar vacio. Linea: {replica.Linea}");
            });
        }
    }
}
