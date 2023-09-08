using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.SupervisionMuestreo.Queries
{
    public class DeleteSupervisionMuestreo : IRequest<Response<bool>>
    {
        public long SupervisionId { get; set; }
    }

    public class DeleteSupervisionMuestreoHandler : IRequestHandler<DeleteSupervisionMuestreo, Response<bool>>
    {
        private readonly ISupervisionMuestreoRepository _supervisionMuestreoRepository;
        private readonly IValoresSupervisionMuestreoRepository _valoresSupervisionMuestreoRepository;
        private readonly IEvidenciaSupervisionMuestreoRepository _evidenciasSupervisionMuestreoRepository;
        private readonly IArchivoService _archivoService;

        public DeleteSupervisionMuestreoHandler(
            ISupervisionMuestreoRepository supervisionMuestreoRepository,
            IValoresSupervisionMuestreoRepository valoresSupervisionMuestreoRepository,
            IEvidenciaSupervisionMuestreoRepository evidenciaSupervisionMuestreoRepository,
            IArchivoService archivoService)
        {
            _supervisionMuestreoRepository=supervisionMuestreoRepository;
            _valoresSupervisionMuestreoRepository=valoresSupervisionMuestreoRepository;
            _evidenciasSupervisionMuestreoRepository=evidenciaSupervisionMuestreoRepository;
            _archivoService=archivoService;
        }

        public async Task<Response<bool>> Handle(DeleteSupervisionMuestreo request, CancellationToken cancellationToken)
        {
            var resultados = _supervisionMuestreoRepository.ObtenerElementoConInclusiones(x => x.Id == request.SupervisionId, y => y.ValoresSupervisionMuestreo, z => z.EvidenciaSupervisionMuestreo);

            if (!resultados.Any())
                throw new KeyNotFoundException($"No se encontró el registro de supervisión de muestreo");

            var supervision = resultados.FirstOrDefault();
            var eliminado = await _supervisionMuestreoRepository.EliminarSupervision(supervision.Id);

            if (eliminado && supervision.EvidenciaSupervisionMuestreo.Any())
            {
                foreach (var archivo in supervision.EvidenciaSupervisionMuestreo)
                {
                    _archivoService.EliminarArchivoSupervisionMuestreo(archivo.NombreArchivo, archivo.SupervisionMuestreoId.ToString());
                }
            }

            return new Response<bool>(true);
        }
    }
}
