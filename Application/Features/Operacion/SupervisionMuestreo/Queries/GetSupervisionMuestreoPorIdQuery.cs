using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
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
        private readonly IEvidenciaSupervisionMuestreoRepository _evidenciaMuestreoRepository;
        private readonly IVwOrganismosDireccionesRepository _orgadisreccionesRepository;
        private readonly IRepositoryAsync<OrganismoCuenca> _repositorycuenca;
        private readonly ILaboratorioRepository _labratorios;
        private readonly IMuestreadoresRepository _muestradores;


        public GetSupervisionMuestreoPorIdHandler(ISupervisionMuestreoRepository supervisionRepository, IValoresSupervisionMuestreoRepository valoresSupevisionRepositiry,
            ISitioRepository sitioRepository, IEvidenciaSupervisionMuestreoRepository evidenciaMuestreoRepository,
            IVwOrganismosDireccionesRepository orgadisreccionesRepository, IRepositoryAsync<OrganismoCuenca> repositorycuenca,
            ILaboratorioRepository labratorios, IMuestreadoresRepository muestreadores)
        {
            _supervisionRepository = supervisionRepository;
            _valoresSupevisionRepositiry = valoresSupevisionRepositiry;
            _sitioRepository = sitioRepository;
            _evidenciaMuestreoRepository = evidenciaMuestreoRepository;
            _orgadisreccionesRepository = orgadisreccionesRepository;
            _repositorycuenca = repositorycuenca;
            _labratorios = labratorios;
            _muestradores = muestreadores;
        }

        public async Task<Response<SupervisionMuestreoDto>> Handle(GetSupervisionMuestreoPorIdQuery request, CancellationToken cancellationToken)
        {
            var supervision = await _supervisionRepository.ObtenerElementoPorIdAsync(request.SupervisionMuestreoId);
            var valoresDetalle = await _valoresSupevisionRepositiry.ObtenerElementosPorCriterioAsync(x => x.SupervisionMuestreoId == request.SupervisionMuestreoId);
            var sitio = _sitioRepository.ObtenerElementoConInclusiones(p => p.Id == supervision.SitioId, x => x.CuerpoTipoSubtipoAgua.TipoCuerpoAgua);
            var evidencias = await _evidenciaMuestreoRepository.ObtenerElementosPorCriterioAsync(x => x.SupervisionMuestreoId == request.SupervisionMuestreoId);

            var organismosdireccionesRealiza = await _orgadisreccionesRepository.ObtenerElementosPorCriterioAsync(x => x.Id == supervision.OrganismosDireccionesRealizaId);
            var OrganismosCuenca = await _repositorycuenca.ListAsync(cancellationToken);
            //await _repositorycuenca.ListAsync().Result.ToList().Select(x => x.Id== supervision.OrganismoCuencaReportaId)
            var laboratorioRealiza = await _labratorios.ObtenerElementoPorIdAsync(supervision.LaboratorioRealizaId);
            var respToma = await _muestradores.ObtenerElementosPorCriterioAsync(x => x.Id == supervision.ResponsableTomaId);

            var respMediciones = await _muestradores.ObtenerElementosPorCriterioAsync(x => x.Id == supervision.ResponsableMedicionesId);

            SupervisionMuestreoDto supervisionDto = new()
            {
                Id = supervision.Id,
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
                ObservacionesMuestreo = supervision.ObservacionesMuestreo,
                OrganismosDireccionesRealiza = organismosdireccionesRealiza.FirstOrDefault().OrganismoCuencaDireccionLocal,
                OrganismoCuencaReporta = OrganismosCuenca.FirstOrDefault().Descripcion,
                LaboratorioRealiza = laboratorioRealiza.Descripcion,
                ResponsableToma = respToma.FirstOrDefault().Nombre + ' ' + respToma.FirstOrDefault().ApellidoPaterno + ' ' + respToma.FirstOrDefault().ApellidoMaterno,
                ResponsableMediciones = respMediciones.FirstOrDefault().Nombre + ' ' + respMediciones.FirstOrDefault().ApellidoPaterno + ' ' + respMediciones.FirstOrDefault().ApellidoMaterno,
            };

            if (evidencias.ToList().Count > 0)
            {
                evidencias.ToList().ForEach(evidencia =>
                {
                    var evidenciaDto = new EvidenciaSupervisionDto()
                    {
                        Id = evidencia.Id,
                        SupervisionMuestreoId = evidencia.SupervisionMuestreoId,
                        NombreArchivo = evidencia.NombreArchivo,
                        TipoEvidencia = evidencia.TipoEvidenciaId
                    };
                    supervisionDto.Archivos.Add(evidenciaDto);
                });
            }

            if (valoresDetalle.ToList().Count > 0)
            {
                List<ClasificacionCriterioDto> lstcriterios = _supervisionRepository.ObtenerCriterios().Result.ToList();

                foreach (var dat in lstcriterios)
                {
                    foreach (var item in dat.Criterios)
                    {
                        var valSupervision = valoresDetalle.Where(x => x.CriterioSupervisionId == item.Id).FirstOrDefault();

                        if (valSupervision != null)
                        {
                            item.ValoresSupervisonMuestreoId = valSupervision.Id;
                            item.Cumplimiento = valSupervision.Resultado;
                            item.Observacion = valSupervision.ObservacionesCriterio;
                        }
                    }
                }

                supervisionDto.Clasificaciones = lstcriterios;
            }

            return new Response<SupervisionMuestreoDto>(supervisionDto);
        }
    }
}
