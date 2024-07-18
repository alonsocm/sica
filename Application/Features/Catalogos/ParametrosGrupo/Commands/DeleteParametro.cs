using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.ParametrosGrupo.Commands
{
    public class DeleteParametro : IRequest<Response<bool>>
    {
        public int ParametroId { get; set; }
    }

    public class DeleteParametroHandler : IRequestHandler<DeleteParametro, Response<bool>>
    {
        private readonly IParametroRepository _parametroRepository;

        public DeleteParametroHandler(IParametroRepository parametroRepository)
        {
            _parametroRepository=parametroRepository;
        }

        public async Task<Response<bool>> Handle(DeleteParametro request, CancellationToken cancellationToken)
        {
            var parametroBD = await _parametroRepository.ObtenerElementoPorIdAsync(request.ParametroId);

            if (parametroBD == null)
                throw new KeyNotFoundException();

            _parametroRepository.Eliminar(parametroBD);
            return new Response<bool>(true);
        }
    }
}
