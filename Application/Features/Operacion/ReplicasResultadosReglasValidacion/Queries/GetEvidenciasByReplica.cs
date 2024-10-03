using Application.DTOs;
using Application.Features.Evidencias.Queries;
using Application.Interfaces.IRepositories;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.ReplicasResultadosReglasValidacion.Queries
{
    public class GetEvidenciasByReplica: IRequest<Response<List<ArchivoDto>>>
    {
        public List<int> lstReplicasResultadosId { get; set; }
    }

    public class GetEvidenciasByReplicaHandler : IRequestHandler<GetEvidenciasByReplica, Response<List<ArchivoDto>>>
    {
        private readonly IEvidenciasReplicasResultadoReglasValidacionRepository _evidenciaReplicaRepository;
        private readonly IArchivoService _archivoService;

        public GetEvidenciasByReplicaHandler(IEvidenciasReplicasResultadoReglasValidacionRepository evidenciaReplicaRepository, IArchivoService archivoService)
        {
            _evidenciaReplicaRepository = evidenciaReplicaRepository;
            _archivoService = archivoService;
        }

        public async Task<Response<List<ArchivoDto>>> Handle(GetEvidenciasByReplica request, CancellationToken cancellationToken)
        {
            var evidencias = await _evidenciaReplicaRepository.ObtenerElementosPorCriterioAsync(x => request.lstReplicasResultadosId.Contains((int)x.ReplicasResultadoReglasValidacionId));

            if (!evidencias.Any())
            {
                throw new KeyNotFoundException($"No se encontraron evidencias para las replicas solicitadas");
            }

            var evidenciasDto = new List<ArchivoDto>();
            var nameCarpeta = evidencias.Select(s => s.NombreArchivo[..s.NombreArchivo.LastIndexOf('_')]).Distinct();

            foreach (var name in nameCarpeta)
            {
                evidenciasDto.AddRange(_archivoService.ObtenerEvidenciasPorReplica(name));
            }

            return new Response<List<ArchivoDto>>(evidenciasDto);
        }
    }
}
