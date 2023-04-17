using Application.DTOs;
using Application.DTOs.Users;
using Application.Features.Muestreos.Queries;
using Application.Features.ResumenResultados.Queries;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Features.Replicas.Queries
{
    public class GetReplicaRevicionResultados : IRequest<Response<List<AprobacionResultadoMuestreoDto>>>
    {
        public int UserId { get; set; }
    }

    public class GetReplicaRevicionResultadosHandler : IRequestHandler<GetReplicaRevicionResultados, Response<List<AprobacionResultadoMuestreoDto>>>
    {
        private readonly IReplicas _repositoryAsync;

        public GetReplicaRevicionResultadosHandler(IReplicas repository)
        {
            _repositoryAsync = repository;
        }
        //Obtener todos los registros de las tablas Muestreo,  ResultadoMuestreo y AprobacionResultadoMuestreo acorde al idMuestreo
        public async Task<Response<List<AprobacionResultadoMuestreoDto>>> Handle(GetReplicaRevicionResultados request, CancellationToken cancellationToken)
        {
            var datos = await _repositoryAsync.GetResumenResultadosReplicaAsync(request.UserId); 
            return datos == null
                ? throw new KeyNotFoundException($"No se encontraron datos asociados a replicas")
                : new Response<List<AprobacionResultadoMuestreoDto>>(datos.ToList());
        }
    }
}
