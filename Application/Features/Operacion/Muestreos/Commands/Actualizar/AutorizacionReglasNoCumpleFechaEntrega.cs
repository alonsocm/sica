using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Commands.Actualizar
{
    public class AutorizacionReglasNoCumpleFechaEntrega : IRequest<Response<bool>>
    {
        public AutorizacionReglasNoCumpleFechaEntregaDTO Registro { get; set; } = new AutorizacionReglasNoCumpleFechaEntregaDTO();
    }

    public class ActualizarAutorizacionReglasNoCumpleFechaEntregaHandler : IRequestHandler<AutorizacionReglasNoCumpleFechaEntrega, Response<bool>>
    {
        private readonly IMuestreoRepository _muestreoRepository;

        public ActualizarAutorizacionReglasNoCumpleFechaEntregaHandler(IMuestreoRepository muestreoRepository)
        {
            _muestreoRepository = muestreoRepository;
        }

        public async Task<Response<bool>> Handle(AutorizacionReglasNoCumpleFechaEntrega request, CancellationToken cancellationToken)
        {
            var muestreo = await _muestreoRepository.ObtenerElementoPorIdAsync(request.Registro.Muestreo);

            if (muestreo is not null)
            {
                muestreo.AutorizacionFechaEntrega = request.Registro.AutorizacionNoCumpleFechaEntrega;
                _muestreoRepository.Actualizar(muestreo);

                return new Response<bool>(true);
            }
            else
            {
                throw new KeyNotFoundException($"No se encontró el muestreo con id {request.Registro.Muestreo}");
            }
        }
    }
}
