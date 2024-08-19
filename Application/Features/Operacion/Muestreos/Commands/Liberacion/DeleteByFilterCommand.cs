using Application.Expressions;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Muestreos.Commands.Liberacion
{
    public class DeleteByFilterCommand : IRequest<Response<bool>>
    {
        public List<Filter> Filters { get; set; }
    }

    public class DeleteAllCommandHandler : IRequestHandler<DeleteByFilterCommand, Response<bool>>
    {
        private readonly IMuestreoRepository _muestreoRepository;
        private readonly IResultado _resultadoRepository;
        private readonly IEvidenciaMuestreoRepository _evidenciaMuestreoRepository;
        private readonly IArchivoService _archivos;

        public DeleteAllCommandHandler(IMuestreoRepository muestreoRepository, IResultado resultadoRepository, IEvidenciaMuestreoRepository evidenciaMuestreoRepository, IArchivoService archivos)
        {
            _muestreoRepository=muestreoRepository;
            _resultadoRepository=resultadoRepository;
            _evidenciaMuestreoRepository=evidenciaMuestreoRepository;
            _archivos = archivos;
        }

        public async Task<Response<bool>> Handle(DeleteByFilterCommand request, CancellationToken cancellationToken)
        {
            var data = await _muestreoRepository.GetResumenMuestreosAsync(new List<int> { (int)Enums.EstatusMuestreo.Cargado, (int)Enums.EstatusMuestreo.EvidenciasCargadas });
            data = data.AsQueryable();

            if (request.Filters.Any())
            {
                var expressions = MuestreoExpression.GetExpressionList(request.Filters);

                foreach (var filter in expressions)
                {
                    data = data.AsQueryable().Where(filter);
                }
            }

            foreach (var muestreo in data)
            {
                var muestreoDb = await _muestreoRepository.ObtenerElementoPorIdAsync(muestreo.MuestreoId);

                if (muestreoDb is null)
                    throw new KeyNotFoundException($"No se encontró el identificador: {muestreo.MuestreoId}");

                _evidenciaMuestreoRepository.EliminarEvidenciasMuestreo(muestreoDb.Id);
                _archivos.EliminarEvidencias(muestreo.ClaveMonitoreo);

                var resultados = await _resultadoRepository.ObtenerElementosPorCriterioAsync(r => r.MuestreoId == muestreo.MuestreoId);

                if (resultados.Any())
                {
                    resultados.ToList().ForEach(resultado =>
                    {
                        _resultadoRepository.Eliminar(resultado);
                    });
                }

                _muestreoRepository.Eliminar(muestreoDb);
            }

            return new Response<bool> { Succeded=true };
        }
    }
}
