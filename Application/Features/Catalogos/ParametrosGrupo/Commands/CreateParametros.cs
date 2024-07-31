using Application.DTOs.Catalogos;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using FluentValidation;
using MediatR;

namespace Application.Features.Catalogos.ParametrosGrupo.Commands
{
    public class CreateParametros : IRequest<Response<bool>>
    {
        public List<ExcelParametroDTO> Parametros { get; set; }
        public bool Actualizar { get; set; }
    }

    public class CreateParametrosHandler : IRequestHandler<CreateParametros, Response<bool>>
    {
        private readonly IParametroRepository _parametroRepository;

        public CreateParametrosHandler(IParametroRepository parametroRepository)
        {
            _parametroRepository = parametroRepository;
        }

        public async Task<Response<bool>> Handle(CreateParametros request, CancellationToken cancellationToken)
        {
            var grupos = _parametroRepository.GetGruposParametros();
            var subgrupos = _parametroRepository.GetSubGrupoAnalitico();
            var unidadesMedida = _parametroRepository.GetUnidadesMedida();

            foreach (var parametro in request.Parametros)
            {
                //Buscamos grupo
                var grupo = grupos.Where(w => w.Descripcion == parametro.Grupo).FirstOrDefault();
                //Buscamos subgrupo
                var subgrupo = subgrupos.Where(w => w.Descripcion == parametro.Subgrupo).FirstOrDefault();
                //Buscamos unidad de medida
                var unidadMedida = unidadesMedida.Where(w => w.Descripcion == parametro.UnidadMedida).FirstOrDefault();

                var parametroBD = _parametroRepository.ObtenerElementosPorCriterioAsync(x => x.ClaveParametro == parametro.Clave).Result.FirstOrDefault();

                if (parametroBD != null && !request.Actualizar)
                {
                    return new Response<bool> { Succeded = false, Message = "Se encontraron parámetros registrados previamente" };
                }
                else if (parametroBD != null && request.Actualizar)
                {
                    parametroBD.ClaveParametro = parametro.Clave;
                    parametroBD.Descripcion = parametro.Descripcion;
                    parametroBD.GrupoParametroId = grupo.Id;
                    parametroBD.IdSubgrupo = subgrupo.Id;
                    parametroBD.IdUnidadMedida = unidadMedida.Id;

                    _parametroRepository.Actualizar(parametroBD);
                }
                else
                {
                    var ultimoValorOrden = _parametroRepository.ObtenerTodosElementosAsync().Result.Max(x => x.Orden);
                    var nuevoRegistro = new Domain.Entities.ParametrosGrupo()
                    {
                        ClaveParametro = parametro.Clave,
                        Descripcion = parametro.Descripcion,
                        GrupoParametroId = grupo.Id,
                        IdSubgrupo = subgrupo.Id,
                        IdUnidadMedida = unidadMedida.Id,
                        Orden = ultimoValorOrden
                    };

                    _parametroRepository.Insertar(nuevoRegistro);
                }
            }

            return new Response<bool>(true);
        }
    }
}
