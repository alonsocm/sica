using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Catalogos.ParametrosGrupo.Commands
{
    public class UpdateParametro : CreateParametro, IRequest<Response<bool>>
    {
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

            //Si se está modificando la clave
            if (parametroBD.ClaveParametro != request.Clave)
            {
                //Buscamos que no exista ya un registro con esa clave.
                var registroDB = await _parametroRepository.ObtenerElementosPorCriterioAsync(x => x.ClaveParametro == request.Clave.Trim());

                if (registroDB.Any())
                {
                    return new Response<bool>(false)
                    {
                        Succeded = false,
                        Message = $"No se pudo actualizar el parámetro. La clave {request.Clave}, ya se encuentra registrada."
                    };
                }
            }

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
