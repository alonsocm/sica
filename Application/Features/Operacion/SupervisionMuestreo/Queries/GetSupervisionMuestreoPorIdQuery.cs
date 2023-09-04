using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.SupervisionMuestreo.Queries
{
    public class GetSupervisionMuestreoPorIdQuery: IRequest<Response<SupervisionMuestreoDto>>
    {
        public long supervisionMuestreoId { get; set; }
    }

    public class GetSupervisionMuestreoPorIdHandler : IRequestHandler<GetSupervisionMuestreoPorIdQuery, Response<SupervisionMuestreoDto>>
    {
        private readonly ISupervisionMuestreoRepository _supervisionRepository;
        public GetSupervisionMuestreoPorIdHandler(ISupervisionMuestreoRepository supervisionRepository)
        {
            _supervisionRepository = supervisionRepository;
        }

        public async Task<Response<SupervisionMuestreoDto>> Handle(GetSupervisionMuestreoPorIdQuery request, CancellationToken cancellationToken)
        {
            SupervisionMuestreoDto supervision = new SupervisionMuestreoDto();
            var supervisionDetalle = await _supervisionRepository.ObtenerElementoPorIdAsync(request.supervisionMuestreoId);

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

            if (supervisionDetalle.EvidenciaSupervisionMuestreo.Count > 0) {             
              
                     supervisionDetalle.EvidenciaSupervisionMuestreo.ToList().ForEach(evidencia =>
                     {
                         var evidenciaDto = new EvidenciaSupervisionDto()
                         { SupervisionMuestreoId = evidencia.SupervisionMuestreoId,
                         NombreArchivo = evidencia.NombreArchivo,
                         TipoEvidencia =evidencia.TipoEvidenciaId};
                         supervision.LstEvidencia.Add(evidenciaDto);
                     });
            }
            if (supervisionDetalle.ValoresSupervisionMuestreo.Count > 0) {


               
                supervisionDetalle.ValoresSupervisionMuestreo .ToList().ForEach(valor =>
                {
                    var criteriodto = new ClasificacionCriterioDto()
                    {
                 
                    };
                    supervision.Clasificaciones.Add(criteriodto);
                });

            }

            return new Response<SupervisionMuestreoDto>(supervision);
        }
    }
}
