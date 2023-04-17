using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Evidencias.Queries
{
    public class GetEvidenciasByMuestreo : IRequest<Response<List<ArchivoDto>>>
    {
        public List<int> MuestreosId { get; set; }
    }

    public class GetEvidenciasByMuestreoHandler : IRequestHandler<GetEvidenciasByMuestreo, Response<List<ArchivoDto>>>
    {
        private readonly IEvidenciaMuestreoRepository _evidenciaMuestreoRepository;
        private readonly IArchivoService _archivoService;

        public GetEvidenciasByMuestreoHandler(IEvidenciaMuestreoRepository evidenciaMuestreoRepository, IArchivoService archivoService)
        {
            _evidenciaMuestreoRepository=evidenciaMuestreoRepository;
            _archivoService=archivoService;
        }

        public async Task<Response<List<ArchivoDto>>> Handle(GetEvidenciasByMuestreo request, CancellationToken cancellationToken)
        {
            var evidencias = await _evidenciaMuestreoRepository.ObtenerElementosPorCriterioAsync(x => request.MuestreosId.Contains((int)x.MuestreoId));

            if (!evidencias.Any())
            {
                throw new KeyNotFoundException($"No se encontraron evidencias para los monitoreos solicitados");
            }

            var evidenciasDto = new List<ArchivoDto>();
            var clavesMuestreos = evidencias.Select(s => s.NombreArchivo[..s.NombreArchivo.LastIndexOf('-')]).Distinct();

            foreach (var clave in clavesMuestreos)
            {
                evidenciasDto.AddRange(_archivoService.ObtenerEvidenciasPorMuestreo(clave));
            }

            return new Response<List<ArchivoDto>>(evidenciasDto);
        }
    }
}
