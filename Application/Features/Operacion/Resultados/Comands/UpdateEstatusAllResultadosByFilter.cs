using Application.DTOs;
using Application.DTOs.Catalogos;
using Application.Expressions;
using Application.Features.Operacion.Muestreos.Commands.Actualizar;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Resultados.Comands
{
    public class UpdateEstatusAllResultadosByFilter: IRequest<Response<bool>>
    {
        public int EstatusIdActual { get; set; }
        public int EstatusId { get; set; }
        public List<Filter> Filters { get; set; }
    }

    public class UpdateEstatusAllResultadosByFilterHandler : IRequestHandler<UpdateEstatusAllResultadosByFilter, Response<bool>>
    {
        private readonly IResultado _resultadoRepository;

        public UpdateEstatusAllResultadosByFilterHandler(IResultado resultadoRepository, IMapper mapper)
        {
            _resultadoRepository = resultadoRepository;
        }

        public async Task<Response<bool>> Handle(UpdateEstatusAllResultadosByFilter request, CancellationToken cancellationToken)
        {
            //Obtenemos los registros, con los estatus requeridos.
            var data = _resultadoRepository.ObtenerElementosPorCriterioAsync(x => x.EstatusResultadoId == request.EstatusIdActual).Result.ToList();

            if (request.Filters.Any())
            {
                var expressions = QueryExpression<Domain.Entities.ResultadoMuestreo>.GetExpressionList(request.Filters);                
                foreach (var filter in expressions)
                {                    data = (List<Domain.Entities.ResultadoMuestreo>)data.AsQueryable().Where(filter);                }
                data.Select(x => x.EstatusResultadoId == request.EstatusId);
            }

           await _resultadoRepository.ActualizarBulkAsync(data.ToList());
            return new Response<bool>(true);
        }
    }
}
