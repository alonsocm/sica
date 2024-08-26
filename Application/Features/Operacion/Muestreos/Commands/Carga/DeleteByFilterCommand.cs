using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Commands.Carga
{
    public class DeleteByFilterCommand : IRequest<Response<bool>>
    {
        public IEnumerable<long> Muestreos { get; set; }
    }

    public class DeleteAllCommandHandler : IRequestHandler<DeleteByFilterCommand, Response<bool>>
    {
        private readonly IMuestreoRepository _muestreoRepository;
        private readonly IResultado _resultadoRepository;
        private readonly IEvidenciaMuestreoRepository _evidenciaMuestreoRepository;
        private readonly IArchivoService _archivos;
        private readonly IVwClaveMonitoreo _claveMonitoreo;

        public DeleteAllCommandHandler(IMuestreoRepository muestreoRepository, IResultado resultadoRepository, IEvidenciaMuestreoRepository evidenciaMuestreoRepository, IArchivoService archivos, IVwClaveMonitoreo claveMonitoreo)
        {
            _muestreoRepository = muestreoRepository;
            _resultadoRepository = resultadoRepository;
            _evidenciaMuestreoRepository = evidenciaMuestreoRepository;
            _claveMonitoreo = claveMonitoreo;
            _archivos = archivos;
            _claveMonitoreo=claveMonitoreo;
        }

        public async Task<Response<bool>> Handle(DeleteByFilterCommand request, CancellationToken cancellationToken)
        {
            if (request.Muestreos.Any())
            {
                var muestreos = await _muestreoRepository.ObtenerElementosPorCriterioAsync(x => request.Muestreos.Contains(x.Id));

                foreach (var muestreo in muestreos)
                {
                    if (muestreo is null)
                    {
                        throw new KeyNotFoundException($"No se encontró el identificador: {muestreo.Id}");
                    }

                    _evidenciaMuestreoRepository.EliminarEvidenciasMuestreo(muestreo.Id);
                    var resultados = await _resultadoRepository.ObtenerElementosPorCriterioAsync(r => r.MuestreoId == muestreo.Id);

                    if (resultados.Any())
                    {
                        resultados.ToList().ForEach(resultado =>
                        {
                            _resultadoRepository.Eliminar(resultado);
                        });
                    }

                    _muestreoRepository.Eliminar(muestreo);

                    var datosMuestreo = _claveMonitoreo.ObtenerElementosPorCriterio(x => x.ProgramaMuestreoId == muestreo.ProgramaMuestreoId).FirstOrDefault();

                    if (datosMuestreo != null)
                    {
                        _archivos.EliminarEvidencias(datosMuestreo.ClaveMuestreo);
                    }
                }

                return new Response<bool> { Succeded=true };
            }

            throw new ArgumentException("No se especificó ningún muestreo para eliminar");
        }
    }
}
