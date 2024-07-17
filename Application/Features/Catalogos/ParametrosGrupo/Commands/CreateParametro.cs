using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.ParametrosGrupo.Commands
{
    public class CreateParametro : IRequest<Response<bool>>
    {
        public int ParametroId { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public int UnidadMedidaId { get; set; }
        public int GrupoId { get; set; }
        public int SubgrupoId { get; set; }
        public int? ParametroPadreId { get; set; }
    }

    public class CreateParametroHandler : IRequestHandler<CreateParametro, Response<bool>>
    {
        private readonly IParametroRepository _parametroRepository;

        public CreateParametroHandler(IParametroRepository parametroRepository)
        {
            _parametroRepository=parametroRepository;
        }

        public Task<Response<bool>> Handle(CreateParametro request, CancellationToken cancellationToken)
        {
            var parametro = new Domain.Entities.ParametrosGrupo()
            {
                ClaveParametro = request.Clave,
                Descripcion = request.Descripcion,
                GrupoParametroId = request.GrupoId,
                IdSubgrupo = request.SubgrupoId,
                IdUnidadMedida = request.UnidadMedidaId,
                ParametroPadreId = request.ParametroPadreId,
            };

            _parametroRepository.Insertar(parametro);
            return Task.FromResult(new Response<bool>(true));
        }
    }
}
