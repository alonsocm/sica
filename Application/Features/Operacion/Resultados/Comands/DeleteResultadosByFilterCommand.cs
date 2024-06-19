using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Reflection.Metadata.Ecma335;

namespace Application.Features.Operacion.Resultados.Comands
{
    public class DeleteResultadosByFilterCommand : IRequest<Response<bool>>
    {
        public List<Filter> Filters { get; set; }
        public int estatusId { get; set; }

    }

    public class DeleteResultadosByFilterCommandHandler : IRequestHandler<DeleteResultadosByFilterCommand, Response<bool>>
    {
        private readonly IMuestreoRepository _repositoryAsync;
        private readonly IResultado _resultadoRepository;
        public DeleteResultadosByFilterCommandHandler(IMuestreoRepository repositoryAsync, IResultado resultadoRepository)
        {
            _repositoryAsync = repositoryAsync;
            _resultadoRepository = resultadoRepository;
        }
        public async Task<Response<bool>> Handle(DeleteResultadosByFilterCommand request, CancellationToken cancellationToken)
        {
            var data = await _repositoryAsync.GetResultadosMuestreoEstatusMuestreoAsync(request.estatusId);
            data = data.AsQueryable();

            if (request.Filters.Any())
            {
                var expressions = MuestreoExpression.GetExpressionList(request.Filters);
                foreach (var filter in expressions)
                { data = (IEnumerable<DTOs.AcumuladosResultadoDto>)data.AsQueryable().Where(filter); }
            }

            List<long> lstresultados = data.Select(x => x.resultadoMuestreoId).Distinct().ToList();
            foreach (var idResultado in lstresultados)
            { _resultadoRepository.Eliminar(x => x.Id == idResultado); }
            return new Response<bool> { Succeded = true };
        }
    }
}
