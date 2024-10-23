using Application.DTOs;
using Application.Features.Operacion.Muestreos.Commands.Actualizar;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Resultados.Comands
{
    public class UpdateEstatusResultadosById : IRequest<Response<bool>>
    {
        public int EstatusId { get; set; }
        public IEnumerable<long> lstResultados { get; set; }
    }

    public class UpdateEstatusResultadosByIdHandler : IRequestHandler<UpdateEstatusResultadosById, Response<bool>>
    {
        private readonly IResultado _resultadoRepository;

        public UpdateEstatusResultadosByIdHandler(IResultado resultadoRepository)
        {
            _resultadoRepository = resultadoRepository;
        }

        public async Task<Response<bool>> Handle(UpdateEstatusResultadosById request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.lstResultados == null)
                {
                    return new Response<bool> { Succeded = false };
                }
                else
                {
                    var resultados = await _resultadoRepository.ObtenerElementosPorCriterioAsync(x => request.lstResultados.Contains(x.Id));

                    resultados.ToList().ForEach(resultado =>
                    {
                        resultado.EstatusResultadoId = request.EstatusId;
                        _resultadoRepository.Eliminar(resultado);
                    });
                    return new Response<bool> { Succeded = true };
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
