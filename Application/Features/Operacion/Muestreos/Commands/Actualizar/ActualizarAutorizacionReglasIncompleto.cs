using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Commands.Actualizar
{
    public class ActualizarAutorizacionReglasIncompleto : IRequest<Response<bool>>
    {
        public AutorizacionReglasIncompletoDTO Registro { get; set; } = new AutorizacionReglasIncompletoDTO();
    }

    public class ActualizarAutorizacionReglasIncompletoHandler : IRequestHandler<ActualizarAutorizacionReglasIncompleto, Response<bool>>
    {
        private readonly IMuestreoRepository _muestreoRepository;

        public ActualizarAutorizacionReglasIncompletoHandler(IMuestreoRepository muestreoRepository)
        {
            _muestreoRepository=muestreoRepository;
        }

        public async Task<Response<bool>> Handle(ActualizarAutorizacionReglasIncompleto request, CancellationToken cancellationToken)
        {
            var muestreo = await _muestreoRepository.ObtenerElementoPorIdAsync(request.Registro.Muestreo);

            if (muestreo is not null)
            {
                muestreo.AutorizacionIncompleto = request.Registro.AutorizacionIncompleto;
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
