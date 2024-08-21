using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Commands.Liberacion
{
    public class AsignarFechaLimiteCommand : IRequest<Response<bool>>
    {
        public IEnumerable<long> Muestreos { get; set; }
        public string FechaLimiteRevision { get; set; }
    }

    public class AsignarFechaLimiteCommandHandler : IRequestHandler<AsignarFechaLimiteCommand, Response<bool>>
    {
        private readonly IMuestreoRepository _repositoryAsync;

        public AsignarFechaLimiteCommandHandler(IMuestreoRepository repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<bool>> Handle(AsignarFechaLimiteCommand request, CancellationToken cancellationToken)
        {
            foreach (var muestreoId in request.Muestreos)
            {
                var muestreo = await _repositoryAsync.ObtenerElementoPorIdAsync(muestreoId);

                if (muestreo != null)
                {
                    muestreo.FechaLimiteRevision = DateTime.TryParse(request.FechaLimiteRevision, out var fechaLimite) ? fechaLimite : DateTime.Now;
                    _repositoryAsync.Actualizar(muestreo);
                }
            }

            return new Response<bool>(true);
        }
    }
}
