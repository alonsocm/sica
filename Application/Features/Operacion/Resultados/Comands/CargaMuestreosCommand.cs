using Application.Interfaces.IRepositories;
using Application.Models;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Commands.Carga
{
    public class CargaObservacionesResumenValidacionReglasCommand : IRequest<Response<bool>>
    {
        public List<ResumenValidacionReglasExcel> Resultados { get; set; } = new List<ResumenValidacionReglasExcel>();
    }

    public class CargaObservacionesResumenValidacionReglasCommandHandler : IRequestHandler<CargaObservacionesResumenValidacionReglasCommand, Response<bool>>
    {
        private readonly IMuestreoRepository _repository;
        private readonly IResultado _resultadosRepository;

        public CargaObservacionesResumenValidacionReglasCommandHandler(IMuestreoRepository repositoryAsync, IResultado resultadosRepository)
        {
            _repository = repositoryAsync;
            _resultadosRepository = resultadosRepository;
        }

        public async Task<Response<bool>> Handle(CargaObservacionesResumenValidacionReglasCommand request, CancellationToken cancellationToken)
        {
            foreach (var resultado in request.Resultados)
            {
                var resultadosBD = await _resultadosRepository.ObtenerElementosPorCriterioAsync(w => w.Parametro.ClaveParametro == resultado.ClaveParametro && w.Muestreo.ProgramaMuestreo.NombreCorrectoArchivo == resultado.ClaveMuestreo);

                var registro = resultadosBD.FirstOrDefault();

                if (registro != null)
                {
                    registro.ObservacionFinal = resultado.ValidacionFinal.Trim().ToUpper();
                    _resultadosRepository.Actualizar(registro);
                }
            }

            return new Response<bool>(true);
        }
    }
}
