using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.SustitucionLimites.Queries
{
    public class ValidarSustitucionPreviaQuery : IRequest<Response<bool>>
    {
        public string Periodo { get; set; }
    }

    public class ValidarSustitucionPreviaQueryHandler : IRequestHandler<ValidarSustitucionPreviaQuery, Response<bool>>
    {
        private readonly IMuestreoRepository _muestreoRepository;

        public ValidarSustitucionPreviaQueryHandler(IMuestreoRepository muestreoRepository)
        {
            _muestreoRepository=muestreoRepository;
        }

        public async Task<Response<bool>> Handle(ValidarSustitucionPreviaQuery request, CancellationToken cancellationToken)
        {
            var existeSustitucionPrevia = await _muestreoRepository.ExisteSustitucionPrevia(request.Periodo);

            return new Response<bool>(existeSustitucionPrevia);
        }
    }
}
