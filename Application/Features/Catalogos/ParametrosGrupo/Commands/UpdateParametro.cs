using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.ParametrosGrupo.Commands
{
    public class UpdateParametro : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public int UnidadMedidaId { get; set; }
        public int GrupoId { get; set; }
        public int SubgrupoId { get; set; }
        public int? ParametroPadreId { get; set; }
    }

    public class UpdateParametroHandler : IRequestHandler<UpdateParametro, Response<bool>>
    {
        private readonly IParametroRepository _parametroRepository;

        public UpdateParametroHandler(IParametroRepository parametroRepository)
        {
            _parametroRepository=parametroRepository;
        }

        public async Task<Response<bool>> Handle(UpdateParametro request, CancellationToken cancellationToken)
        {
            var parametroBD = await _parametroRepository.ObtenerElementoPorIdAsync(request.Id);

            if (parametroBD == null)
                throw new KeyNotFoundException();

            parametroBD.ClaveParametro = request.Clave;
            parametroBD.Descripcion = request.Descripcion;
            parametroBD.GrupoParametroId = request.GrupoId;
            parametroBD.IdSubgrupo = request.SubgrupoId;
            parametroBD.IdUnidadMedida = request.UnidadMedidaId;
            parametroBD.ParametroPadreId = request.ParametroPadreId;

            _parametroRepository.Actualizar(parametroBD);
            return new Response<bool>(true);
        }
    }
}
