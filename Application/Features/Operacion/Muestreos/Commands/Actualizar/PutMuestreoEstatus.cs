using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Commands.Actualizar
{
    public class PutMuestreoEstatus : IRequest<Response<bool>>
    {
        public int estatus { get; set; }
        public long muestreoId { get; set; }
    }

    public class PutMuestreoEstatusHandler : IRequestHandler<PutMuestreoEstatus, Response<bool>>
    {
        private readonly IMuestreoRepository _muestreoRepository;


        public PutMuestreoEstatusHandler(IMuestreoRepository muestreoRepository)
        {
            _muestreoRepository = muestreoRepository;
        }
        public async Task<Response<bool>> Handle(PutMuestreoEstatus request, CancellationToken cancellationToken)
        {
            if (request.muestreoId == 0)
            {
                return new Response<bool> { Succeded = false };
            }
            var muestreo = await _muestreoRepository.ObtenerElementoPorIdAsync(request.muestreoId);
            if (muestreo != null)
            {
                muestreo.EstatusId = request.estatus;
                _muestreoRepository.Actualizar(muestreo);
                return new Response<bool> { Succeded = true };
            }
            return new Response<bool> { Succeded = false };
        }
    }
}
