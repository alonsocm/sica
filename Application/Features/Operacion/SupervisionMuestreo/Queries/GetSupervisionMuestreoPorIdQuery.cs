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
        public GetSupervisionMuestreoPorIdHandler(ISupervisionMuestreoRepository supervisionRepository, IValoresSupervisionMuestreoRepository valoresSupevisionRepositiry)
        {
            _supervisionRepository = supervisionRepository;
            _valoresSupevisionRepositiry = valoresSupevisionRepositiry;
        }

        public async Task<Response<SupervisionMuestreoDto>> Handle(GetSupervisionMuestreoPorIdQuery request, CancellationToken cancellationToken)
        {
            SupervisionMuestreoDto supervision = new SupervisionMuestreoDto();
            var supervisionDetalle = await _supervisionRepository.ObtenerElementoPorIdAsync(request.SupervisionMuestreoId);
            var valoresDetalle = await _valoresSupevisionRepositiry.ObtenerElementosPorCriterioAsync(x => x.SupervisionMuestreoId == request.SupervisionMuestreoId);


            supervision.FechaMuestreo = supervisionDetalle.FehaMuestreo.ToString();
            supervision.HoraInicio = supervisionDetalle.HoraInicio.ToString();
            supervision.HoraTermino = supervisionDetalle.HoraTermino.ToString();
            supervision.HoraTomaMuestra = supervisionDetalle.HoraTomaMuestra.ToString();
            supervision.PuntajeObtenido = supervisionDetalle.PuntajeObtenido;
            supervision.OrganismoCuencaReportaId = supervisionDetalle.OrganismoCuencaReportaId;
            supervision.SupervisorConagua = supervisionDetalle.SupervisorConagua;
            supervision.SitioId = supervisionDetalle.SitioId;
            supervision.ClaveMuestreo = supervisionDetalle.ClaveMuestreo;
            supervision.LatitudToma = float.Parse(supervisionDetalle.LatitudToma.ToString());
            supervision.LongitudToma = float.Parse(supervisionDetalle.LongitudToma.ToString());
            supervision.LaboratorioRealizaId = supervisionDetalle.LaboratorioRealizaId;
            supervision.ResponsableTomaId = supervisionDetalle.ResponsableTomaId;
            supervision.ResponsableMedicionesId = supervisionDetalle.ResponsableMedicionesId;

            if (supervisionDetalle.EvidenciaSupervisionMuestreo.Count > 0)
            {

                supervisionDetalle.EvidenciaSupervisionMuestreo.ToList().ForEach(evidencia =>
                {
                    var evidenciaDto = new EvidenciaSupervisionDto()
                    {
                        SupervisionMuestreoId = evidencia.SupervisionMuestreoId,
                        NombreArchivo = evidencia.NombreArchivo,
                        TipoEvidencia =evidencia.TipoEvidenciaId
                    };
                    supervision.LstEvidencia.Add(evidenciaDto);
                });
            }
            if (valoresDetalle.ToList().Count > 0)
            {

                supervision.Clasificaciones = _valoresSupevisionRepositiry.ValoresSupervisionMuestreoDtoPorId(valoresDetalle.ToList()).Result.ToList();



            }

            return new Response<SupervisionMuestreoDto>(supervision);
        }
    }
}
