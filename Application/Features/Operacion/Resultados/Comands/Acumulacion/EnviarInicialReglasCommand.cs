using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Operacion.Resultados.Comands.Acumulacion
{
    public class EnviarInicialReglasCommand : IRequest<Response<bool>>
    {
        public IEnumerable<long> Muestreos { get; set; } = new List<long>();
    }

    public class EnviarInicialReglasCommandHandler : IRequestHandler<EnviarInicialReglasCommand, Response<bool>>
    {
        private readonly IMuestreoRepository _muestreoRepository;

        public EnviarInicialReglasCommandHandler(IMuestreoRepository muestreoRepository)
        {
            _muestreoRepository = muestreoRepository;
        }

        public async Task<Response<bool>> Handle(EnviarInicialReglasCommand request, CancellationToken cancellationToken)
        {
            if (request.Muestreos.Any())
            {
                bool actualizados = await _muestreoRepository.CambiarEstatusAsync((int)Enums.EstatusMuestreo.MóduloInicialReglas, request.Muestreos);
                return new Response<bool> { Succeded = actualizados };
            }
            else
            {
                throw new ValidationException("No se han enviado muestreos para actualizar");
            }
        }
    }
}
