using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.SupervisionMuestreo.Queries
{
    public class GetSupervisionMuestreoPorIdQuery : IRequest<Response<SupervisionMuestreoDto>>
    {
        public long SupervisionMuestreoId { get; set; }
    }

    public class GetSupervisionMuestreoPorIdHandler : IRequestHandler<GetSupervisionMuestreoPorIdQuery, Response<SupervisionMuestreoDto>>
    {
        private readonly ISupervisionMuestreoRepository _supervisionRepository;
        private readonly IValoresSupervisionMuestreoRepository _valoresSupevisionRepositiry;
        private readonly ISitioRepository _sitioRepository;
        public GetSupervisionMuestreoPorIdHandler(ISupervisionMuestreoRepository supervisionRepository, IValoresSupervisionMuestreoRepository valoresSupevisionRepositiry, ISitioRepository sitioRepository)
        {
            _supervisionRepository = supervisionRepository;
            _valoresSupevisionRepositiry = valoresSupevisionRepositiry;
            _sitioRepository=sitioRepository;
        }

        public async Task<Response<SupervisionMuestreoDto>> Handle(GetSupervisionMuestreoPorIdQuery request, CancellationToken cancellationToken)
        {
            var supervision = await _supervisionRepository.ObtenerElementoPorIdAsync(request.SupervisionMuestreoId);
            var valoresDetalle = await _valoresSupevisionRepositiry.ObtenerElementosPorCriterioAsync(x => x.SupervisionMuestreoId == request.SupervisionMuestreoId);
            var sitio = _sitioRepository.ObtenerElementoConInclusiones(p => p.Id == supervision.SitioId, x => x.CuerpoTipoSubtipoAgua.TipoCuerpoAgua);

            SupervisionMuestreoDto supervisionDto = new()
            {
                FechaMuestreo = supervision.FehaMuestreo.ToString("yyyy-MM-dd"),
                HoraInicio = supervision.HoraInicio.ToString(),
                HoraTermino = supervision.HoraTermino.ToString(),
                HoraTomaMuestra = supervision.HoraTomaMuestra.ToString(),
                PuntajeObtenido = supervision.PuntajeObtenido,
                OrganismoCuencaReportaId = supervision.OrganismoCuencaReportaId,
                SupervisorConagua = supervision.SupervisorConagua,
                SitioId = supervision.SitioId,
                ClaveMuestreo = supervision.ClaveMuestreo,
                LatitudToma = float.Parse(supervision.LatitudToma.ToString()),
                LongitudToma = float.Parse(supervision.LongitudToma.ToString()),
                LaboratorioRealizaId = supervision.LaboratorioRealizaId,
                ResponsableTomaId = supervision.ResponsableTomaId,
                ResponsableMedicionesId = supervision.ResponsableMedicionesId,
                OrganismosDireccionesRealizaId = supervision.OrganismosDireccionesRealizaId,
                TipoCuerpoAgua = sitio.FirstOrDefault().CuerpoTipoSubtipoAgua.TipoCuerpoAgua.Descripcion,
                NombreSitio = sitio.FirstOrDefault().NombreSitio,
                LatitudSitio = sitio.FirstOrDefault().Latitud.ToString(),
                LongitudSitio = sitio.FirstOrDefault().Longitud.ToString(),
                ClaveSitio = sitio.FirstOrDefault().ClaveSitio,
                ObservacionesMuestreo = supervision.ObservacionesMuestreo
            };

            if (supervision.EvidenciaSupervisionMuestreo.Count > 0)
            {
                supervision.EvidenciaSupervisionMuestreo.ToList().ForEach(evidencia =>
                {
                    var evidenciaDto = new EvidenciaSupervisionDto()
                    {
                        Id = evidencia.Id,
                        SupervisionMuestreoId = evidencia.SupervisionMuestreoId,
                        NombreArchivo = evidencia.NombreArchivo,
                        TipoEvidencia = evidencia.TipoEvidenciaId
                    };
                    supervisionDto.LstEvidencia.Add(evidenciaDto);
                });
            }

            if (valoresDetalle.ToList().Count > 0)
            {
                supervisionDto.Clasificaciones = _valoresSupevisionRepositiry.ValoresSupervisionMuestreoDtoPorId(valoresDetalle.ToList()).Result.ToList();
            }

            return new Response<SupervisionMuestreoDto>(supervisionDto);
        }
    }
}
