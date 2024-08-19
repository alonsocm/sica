using Application.Enums;
using Application.Expressions;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.CargaMasivaEvidencias.Commands
{
    public class DeleteEvidenciasByFilterCommand : IRequest<Response<bool>>
    {
        public List<Filter> Filters { get; set; } = new List<Filter>();
    }

    public class DeleteEvidenciasByFilterCommandHandler : IRequestHandler<DeleteEvidenciasByFilterCommand, Response<bool>>
    {
        private readonly IArchivoService _archivos;
        private readonly IEvidenciaMuestreoRepository _evidenciaMuestreoRepository;
        private readonly IVwClaveMonitoreo _vwClaveMonitoreoRepository;
        private readonly IMuestreoRepository _muestreoRepository;

        public DeleteEvidenciasByFilterCommandHandler(IArchivoService archivos, IEvidenciaMuestreoRepository evidenciaMuestreoRepository, IVwClaveMonitoreo vwClaveMonitoreoRepository, IMuestreoRepository muestreoRepository)
        {
            _archivos = archivos;
            _evidenciaMuestreoRepository = evidenciaMuestreoRepository;
            _vwClaveMonitoreoRepository = vwClaveMonitoreoRepository;
            _muestreoRepository = muestreoRepository;
        }

        public async Task<Response<bool>> Handle(DeleteEvidenciasByFilterCommand request, CancellationToken cancellationToken)
        {
            var muestreos = await _muestreoRepository.GetResumenMuestreosAsync(new List<int>() { (int)EstatusMuestreo.EvidenciasCargadas });
            muestreos = muestreos.AsQueryable();

            if (request.Filters.Any())
            {
                var expressions = MuestreoExpression.GetExpressionList(request.Filters);

                foreach (var filter in expressions)
                {
                    muestreos = muestreos.AsQueryable().Where(filter);
                }
            }

            foreach (var claveMuestreo in muestreos.Select(s => s.ClaveMonitoreo))
            {
                var programaMuestreoId = _vwClaveMonitoreoRepository.ObtenerElementosPorCriterio(x => x.ClaveMuestreo == claveMuestreo).FirstOrDefault()?.ProgramaMuestreoId;

                if (programaMuestreoId == null)
                    throw new KeyNotFoundException($"La clave de muestreo: {claveMuestreo} no sé encontró");

                var muestreo = _muestreoRepository.ObtenerElementosPorCriterio(x => x.ProgramaMuestreoId == programaMuestreoId).FirstOrDefault();

                if (muestreo == null)
                    throw new KeyNotFoundException($"No se encontraron los datos del muestreo: {claveMuestreo}");

                _evidenciaMuestreoRepository.Eliminar(x => x.MuestreoId == muestreo.Id);
                _archivos.EliminarEvidencias(claveMuestreo);

                muestreo.EstatusId = (int)EstatusMuestreo.Cargado;
                _muestreoRepository.Actualizar(muestreo);
            }

            return new Response<bool>(true);
        }
    }
}
