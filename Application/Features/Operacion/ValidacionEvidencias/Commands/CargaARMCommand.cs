using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Operacion.ValidacionEvidencias.Commands
{
    public class CargaARMCommand : IRequest<Response<bool>>
    {
        public List<AvisoRealizacionDto> Muestreos { get; set; } = new List<AvisoRealizacionDto>();

    }

    public class CargaARMCommandHandler : IRequestHandler<CargaARMCommand, Response<bool>>
    {
        private readonly IAvisoRealizacionRepository _repository;
        private readonly IResultado _resultadosRepository;

        public CargaARMCommandHandler(IAvisoRealizacionRepository repositoryAsync, IResultado resultadosRepository)
        {
            _repository = repositoryAsync;
            _resultadosRepository = resultadosRepository;
        }

        public async Task<Response<bool>> Handle(CargaARMCommand request, CancellationToken cancellationToken)
        {
            var muestreos = _repository.ConvertToMuestreosList(request.Muestreos);
            _repository.InsertarRango(muestreos);
            return new Response<bool>(true);
        }


    }

}
