using Application.DTOs;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.SustitucionLimites.Commands
{
    public class SustitucionMaximoComunCommand : IRequest<Response<bool>>
    {
        public ParametrosSustitucionLimitesDto ParametrosSustitucion { get; set; }
    }

    public class SustitucionMaximoComunCommandHandler : IRequestHandler<SustitucionMaximoComunCommand, Response<bool>>
    {
        public Task<Response<bool>> Handle(SustitucionMaximoComunCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
