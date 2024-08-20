using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.CargaMasivaEvidencias.Commands
{
    public class DeleteEvidenciasCommand : IRequest<Response<bool>>
    {
        public List<string> Muestreos { get; set; } = new List<string>();
    }

    public class DeleteEvidenciasCommandHandler : IRequestHandler<DeleteEvidenciasCommand, Response<bool>>
    {
        private readonly IArchivoService _archivos;
        private readonly IEvidenciaMuestreoRepository _evidenciaMuestreoRepository;
        private readonly IVwClaveMonitoreo _vwClaveMonitoreoRepository;
        private readonly IMuestreoRepository _muestreoRepository;

        public DeleteEvidenciasCommandHandler(IArchivoService archivos, IEvidenciaMuestreoRepository evidenciaMuestreoRepository, IVwClaveMonitoreo vwClaveMonitoreoRepository, IMuestreoRepository muestreoRepository)
        {
            _archivos = archivos;
            _evidenciaMuestreoRepository = evidenciaMuestreoRepository;
            _vwClaveMonitoreoRepository = vwClaveMonitoreoRepository;
            _muestreoRepository = muestreoRepository;
        }

        public async Task<Response<bool>> Handle(DeleteEvidenciasCommand request, CancellationToken cancellationToken)
        {
            foreach (var claveMuestreo in request.Muestreos)
            {
                var programaMuestreoId = _vwClaveMonitoreoRepository.ObtenerElementosPorCriterio(x => x.ClaveMuestreo == claveMuestreo).FirstOrDefault()?.ProgramaMuestreoId;

                if (programaMuestreoId == null)
                    throw new KeyNotFoundException($"La clave de muestreo: {claveMuestreo} no sé encontró");

                var muestreo = _muestreoRepository.ObtenerElementosPorCriterio(x => x.ProgramaMuestreoId == programaMuestreoId).FirstOrDefault();

                if (muestreo == null)
                    throw new KeyNotFoundException($"No se encontraron los datos del muestreo: {claveMuestreo}");

                _evidenciaMuestreoRepository.Eliminar(x => x.MuestreoId == muestreo.Id);
                _archivos.EliminarEvidencias(claveMuestreo);

                muestreo.EstatusId = (int)Enums.EstatusMuestreo.CargaResultados;
                _muestreoRepository.Actualizar(muestreo);
            }

            return new Response<bool>(true);
        }
    }
}
