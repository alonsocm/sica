using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Commands.Liberacion
{
    public class EnviarAprobacionResultadosCommand : IRequest<Response<bool>>
    {
        public long ResultadoMuestreoId { get; set; }
        public int? EstatusId { get; set; }
    }

    public class EnviarAprobacionResultadosHandler : IRequestHandler<EnviarAprobacionResultadosCommand, Response<bool>>
    {
        private readonly IResumenResRepository _resultadomuestreo;

        public EnviarAprobacionResultadosHandler(IResumenResRepository repositoryAsync)
        {
            _resultadomuestreo = repositoryAsync;
        }
        public async Task<Response<bool>> Handle(EnviarAprobacionResultadosCommand request, CancellationToken cancellationToken)
        {
            var muestreo = await _resultadomuestreo.ObtenerElementoPorIdAsync(request.ResultadoMuestreoId);

            if (muestreo != null)
            {
                muestreo.EstatusResultado = (int)request.EstatusId;
                _resultadomuestreo.Actualizar(muestreo);
            }
            return new Response<bool>(true);
        }


    }
}
