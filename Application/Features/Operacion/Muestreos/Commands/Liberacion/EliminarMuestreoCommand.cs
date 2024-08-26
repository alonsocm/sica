using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Commands.Liberacion
{
    public class EliminarMuestreoCommand : IRequest<Response<bool>>
    {
        public IEnumerable<long> Muestreos { get; set; }
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
            if (request.Muestreos.Any())
            {
                var muestreos = await _muestreoRepository.ObtenerElementosPorCriterioAsync(x => request.Muestreos.Contains(x.Id) && x.TipoCargaId == (int)Enums.TipoCarga.Manual);

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
                        _archivo.EliminarEvidencias(datosMuestreo.ClaveMuestreo);
                    }
                }
            }

            return new Response<bool> { Succeded=true };
        }
    }
}
