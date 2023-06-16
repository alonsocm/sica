using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Replicas.Queries.ReplicasTotal
{
    public class GetAllReplicaResultado : IRequest<Response<List<ReplicaResultadoDto>>>
    {

    }

    public class GetAllReplicaResultadoHandler : IRequestHandler<GetAllReplicaResultado, Response<List<ReplicaResultadoDto>>>
    {
        private readonly IResultado _resultadoRepository;

        public GetAllReplicaResultadoHandler(IResultado resultadoRepository)
        {
            _resultadoRepository=resultadoRepository;
        }

        public async Task<Response<List<ReplicaResultadoDto>>> Handle(GetAllReplicaResultado request, CancellationToken cancellationToken)
        {
            var replicasResultados = await _resultadoRepository.ObtenerReplicasResultados();
            return new Response<List<ReplicaResultadoDto>>(replicasResultados.ToList());
        }
    }
}
