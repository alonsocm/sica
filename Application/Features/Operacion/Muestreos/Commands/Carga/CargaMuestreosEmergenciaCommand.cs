using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.Muestreos.Commands.Carga
{
    public class CargaMuestreosEmergenciaCommand : IRequest<Response<bool>>
    {
        public List<CargaMuestreoEmergenciaDto> Muestreos { get; set; } = new List<CargaMuestreoEmergenciaDto>();
    }

    public class CargaMuestreosEmergenciaCommandHandler : IRequestHandler<CargaMuestreosEmergenciaCommand, Response<bool>>
    {
        private readonly IMuestreoEmergenciasRepository _muestreoEmergenciasRepository;

        public CargaMuestreosEmergenciaCommandHandler(IMuestreoEmergenciasRepository repositoryAsync)
        {
            _muestreoEmergenciasRepository=repositoryAsync;
        }

        public async Task<Response<bool>> Handle(CargaMuestreosEmergenciaCommand request, CancellationToken cancellationToken)
        {
            var muestreos = _muestreoEmergenciasRepository.ConvertToMuestreosList(request.Muestreos);
            _muestreoEmergenciasRepository.InsertarRango(muestreos);

            return new Response<bool>(true);
        }
    }
}
