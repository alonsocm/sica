using Application.DTOs.Catalogos;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using FluentValidation;
using FluentValidation.Results;
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
            _parametroRepository=parametroRepository;
        }

        public async Task<Response<bool>> Handle(CreateParametros request, CancellationToken cancellationToken)
        {
            var grupos = _parametroRepository.GetGruposParametros();
            var subgrupos = _parametroRepository.GetSubGrupoAnalitico();
            var unidadesMedida = _parametroRepository.GetUnidadesMedida();

            foreach (var parametro in request.Parametros)
            {
                var parametroBD = await _parametroRepository.ObtenerElementosPorCriterioAsync(x => x.ClaveParametro == parametro.Clave);

                if (parametroBD.Any())
                {
                    var errors = new List<ValidationFailure>
                    {
                        new("clave", "")
                    };

                    throw new ValidationException(errors);
                }
                else
                {
                    //Buscamos grupo
                    var grupo = grupos.Where(w => w.Descripcion == parametro.Grupo).FirstOrDefault();
                    //Buscamos subgrupo
                    var subgrupo = subgrupos.Where(w => w.Descripcion == parametro.Subgrupo).FirstOrDefault();
                    //Buscamos unidad de medida
                    var unidadMedida = unidadesMedida.Where(w => w.Descripcion == parametro.UnidadMedida).FirstOrDefault();

                    var nuevoRegistro = new Domain.Entities.ParametrosGrupo()
                    {
                        ClaveParametro = parametro.Clave,
                        Descripcion = parametro.Descripcion,
                        GrupoParametroId = grupo.Id,
                        IdSubgrupo = subgrupo.Id,
                        IdUnidadMedida = unidadMedida.Id,
                        Orden = parametro.Orden
                    };

                    _parametroRepository.Insertar(nuevoRegistro);
                }
            }

            return new Response<bool>(true);
        }
    }
}
