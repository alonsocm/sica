using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.MuestreosEmergencias.Queries
{
    public class ExisteMuestreoEmergenciaQuery : IRequest<Response<bool>>
    {
        public string NombreEmergencia { get; set; }
        public string NombreSitio { get; set; }
    }

    public class ExisteMuestreoEmergenciaQueryHandler : IRequestHandler<ExisteMuestreoEmergenciaQuery, Response<bool>>
    {
        private readonly IMuestreoEmergenciasRepository _muestreosEmergenciasRepository;
        public ExisteMuestreoEmergenciaQueryHandler(IMuestreoEmergenciasRepository muestreoEmergenciasRepository)
        {
            _muestreosEmergenciasRepository = muestreoEmergenciasRepository;
        }

        public async Task<Response<bool>> Handle(ExisteMuestreoEmergenciaQuery request, CancellationToken cancellationToken)
        {
            var existeEmergencia = await _muestreosEmergenciasRepository.ExisteCargaPreviaAsync(request.NombreEmergencia, request.NombreSitio);

            return new Response<bool>(existeEmergencia);
        }
    }
}
