using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Muestreos.Commands.Liberacion
{
    public class DeleteAllCommand : IRequest<Response<bool>>
    {
        public List<Filter> Filters { get; set; }
    }

    public class DeleteAllCommandHandler : IRequestHandler<DeleteAllCommand, Response<bool>>
    {
        private readonly IMuestreoRepository _muestreoRepository;
        private readonly IResultado _resultadoRepository;
        private readonly IEvidenciaMuestreoRepository _evidenciaMuestreoRepository;

        public DeleteAllCommandHandler(IMuestreoRepository muestreoRepository, IResultado resultadoRepository, IEvidenciaMuestreoRepository evidenciaMuestreoRepository)
        {
            _muestreoRepository=muestreoRepository;
            _resultadoRepository=resultadoRepository;
            _evidenciaMuestreoRepository=evidenciaMuestreoRepository;
        }

        public async Task<Response<bool>> Handle(DeleteAllCommand request, CancellationToken cancellationToken)
        {
            var data = await _muestreoRepository.GetResumenMuestreosAsync(new List<long> { (long)Enums.EstatusMuestreo.Cargado, (long)Enums.EstatusMuestreo.EvidenciasCargadas });
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
