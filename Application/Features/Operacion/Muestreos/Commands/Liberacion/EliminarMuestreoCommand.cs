using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Muestreos.Commands.Liberacion
{
    public class EliminarMuestreoCommand : IRequest<Response<bool>>
    {
        public List<int> Muestreos { get; set; }
    }

    public class EliminarMuestreoCommandHandler : IRequestHandler<EliminarMuestreoCommand, Response<bool>>
    {
        private readonly IMuestreoRepository _muestreoRepository;
        private readonly IResultado _resultadoRepository;
        private readonly IEvidenciaMuestreoRepository _evidenciaMuestreoRepository;
        private readonly IArchivoService _archivo;
        private readonly IVwClaveMonitoreo _claveMonitoreo;


        public EliminarMuestreoCommandHandler(IMuestreoRepository muestreoRepository, IResultado resultadoRepository, IEvidenciaMuestreoRepository evidenciaMuestreoRepository, IArchivoService archivo, IVwClaveMonitoreo claveMonitoreo)
        {
            _muestreoRepository=muestreoRepository;
            _resultadoRepository=resultadoRepository;
            _evidenciaMuestreoRepository=evidenciaMuestreoRepository;
            _archivo=archivo;
            _claveMonitoreo = claveMonitoreo;
        }

        public async Task<Response<bool>> Handle(EliminarMuestreoCommand request, CancellationToken cancellationToken)
        {
            if (request.Muestreos.Count > 0)
            {
                foreach (var muestreo in request.Muestreos)
                {
                    var muestreoDb = await _muestreoRepository.ObtenerElementoPorIdAsync(muestreo);

                    if (muestreoDb is null)
                    {
                        throw new KeyNotFoundException($"No se encontró el identificador: {muestreo}");
                    }

                    _evidenciaMuestreoRepository.EliminarEvidenciasMuestreo(muestreoDb.Id);
                    var resultados = await _resultadoRepository.ObtenerElementosPorCriterioAsync(r => r.MuestreoId == muestreo);

                    if (resultados.Any())
                    {
                        resultados.ToList().ForEach(resultado =>
                        {
                            _resultadoRepository.Eliminar(resultado);
                        });
                    }

                    _muestreoRepository.Eliminar(muestreoDb);

                    var datosMuestreo = _claveMonitoreo.ObtenerElementosPorCriterio(x => x.ProgramaMuestreoId == muestreoDb.ProgramaMuestreoId).FirstOrDefault();

                    if (datosMuestreo != null)
                    {
                        _archivo.EliminarEvidencias(datosMuestreo.ClaveMuestreo);
                    }
                }
            }

            return new Response<bool> { Succeded=true };
        }
    }
}
