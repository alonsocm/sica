using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.SustitucionLimites.Queries
{
    public class ValidarSustitucionPreviaEmergenciasQuery : IRequest<Response<bool>>
    {
        public int Periodo { get; set; }
    }

    public class ValidarSustitucionPreviaEmergenciasQueryHandler : IRequestHandler<ValidarSustitucionPreviaEmergenciasQuery, Response<bool>>
    {
        private readonly IHistorialSusticionEmergenciaRepository _historialSustitucionRepository;

        public ValidarSustitucionPreviaEmergenciasQueryHandler(IHistorialSusticionEmergenciaRepository historialSustitucionRepository)
        {
            _historialSustitucionRepository = historialSustitucionRepository;
        }

        public async Task<Response<bool>> Handle(ValidarSustitucionPreviaEmergenciasQuery request, CancellationToken cancellationToken)
        {
            var existeSustitucionPrevia = await _historialSustitucionRepository.ExisteElementoAsync(x => x.Anio == request.Periodo);

            return new Response<bool>(existeSustitucionPrevia);
        }
    }
}
