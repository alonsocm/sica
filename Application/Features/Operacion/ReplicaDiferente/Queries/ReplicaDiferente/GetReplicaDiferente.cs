using Application.DTOs;
using Application.Features.Replicas.Queries;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.ReplicaDiferente.Querys.ReplicaDiferente
{
    public class GetReplicaDiferente : IRequest<Response<List<ReplicaDiferenteObtenerDto>>>
    {

    }
    public class GetReplicaDiferenteHandler : IRequestHandler<GetReplicaDiferente, Response<List<ReplicaDiferenteObtenerDto>>>
    {
        private readonly IVwReplicaRevisionResultadoRepository _repositoryAsync;

        public GetReplicaDiferenteHandler(IVwReplicaRevisionResultadoRepository repository)
        {
            _repositoryAsync = repository;
        }
        //Obtener todos los registros de las tablas Muestreo,  ResultadoMuestreo y AprobacionResultadoMuestreo acorde al idMuestreo
        public async Task<Response<List<ReplicaDiferenteObtenerDto>>> Handle(GetReplicaDiferente request, CancellationToken cancellationToken)
        {
            var datos = await _repositoryAsync.GetReplicaDiferenteAsync();
            return datos == null
                ? throw new KeyNotFoundException($"No se encontraron datos asociados a replicas")
                : new Response<List<ReplicaDiferenteObtenerDto>>(datos.ToList());
        }
    }
}
