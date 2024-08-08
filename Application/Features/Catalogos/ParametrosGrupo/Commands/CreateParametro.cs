using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.ParametrosGrupo.Commands
{
    public class CreateParametro : IRequest<Response<bool>>
    {
        public int Id { get; set; }
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

        public async Task<Response<bool>> Handle(CreateParametro request, CancellationToken cancellationToken)
        {
            //Buscamos que no exista ya un registro con esa clave.
            var registroDB = await _parametroRepository.ObtenerElementosPorCriterioAsync(x => x.ClaveParametro == request.Clave.Trim());

            if (registroDB.Any())
            {
                throw new ArgumentException($"No se pudo registrar el parámetro. La clave {request.Clave}, ya se encuentra registrada.");
            }

            var parametro = new Domain.Entities.ParametrosGrupo()
            {
                ClaveParametro = request.Clave.Trim(),
                Descripcion = request.Descripcion,
                GrupoParametroId = request.GrupoId,
                IdSubgrupo = request.SubgrupoId,
                IdUnidadMedida = request.UnidadMedidaId
            };

            _parametroRepository.Insertar(parametro);
            return new Response<bool>(true);
        }
    }
}
