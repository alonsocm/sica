using Application.DTOs;
using Application.Expressions;
using Application.Features.Localidades.Queries;
using Application.Interfaces.IRepositories;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Catalogos.Laboratorios.Queries
{
    public class GetLaboratoriosQuery: IRequest<Response<List<LaboratoriosDto>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<Filter> Filter { get; set; }
        public OrderBy? OrderBy { get; set; }
    }
    public class GetLaboratoriosQueryHandler : IRequestHandler<GetLaboratoriosQuery, Response<List<LaboratoriosDto>>>
    {
        private readonly IRepositoryAsync<Domain.Entities.Laboratorios> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetLaboratoriosQueryHandler(IRepositoryAsync<Domain.Entities.Laboratorios> repositoryAsync, IMapper mapper)
        {
            this._repositoryAsync = repositoryAsync;
            this._mapper = mapper;
        }
        public async Task<Response<List<LaboratoriosDto>>> Handle(GetLaboratoriosQuery request, CancellationToken cancellationToken)
        {
            var laboratorios = await _repositoryAsync.ListAsync();
            var laboratoriosDto = _mapper.Map<List<LaboratoriosDto>>(laboratorios);

            if (request.Filter.Any())
            {
                var expressions = QueryExpression<LaboratoriosDto>.GetExpressionList(request.Filter);
                List<LaboratoriosDto> lstSitio = new();

                foreach (var filter in expressions)
                {
                    if (request.Filter.Count == 2 && request.Filter[0].Conditional == "equals" && request.Filter[1].Conditional == "equals")
                    {
                        var dataFinal = laboratoriosDto;
                        dataFinal = (List<LaboratoriosDto>)dataFinal.AsQueryable().Where(filter);
                        lstSitio.AddRange(dataFinal);
                        laboratoriosDto = lstSitio;
                    }
                    else
                    {
                        laboratoriosDto = (List<LaboratoriosDto>)laboratoriosDto.AsQueryable().Where(filter);
                    }
                }
            }
            if (request.OrderBy != null)
            {
                if (request.OrderBy.Type == "asc")
                {
                    laboratoriosDto = (List<LaboratoriosDto>)laboratoriosDto.AsQueryable().OrderBy(QueryExpression<LaboratoriosDto>.GetOrderByExpression(request.OrderBy.Column));
                }
                else if (request.OrderBy.Type == "desc")
                {
                    laboratoriosDto = (List<LaboratoriosDto>)laboratoriosDto.AsQueryable().OrderByDescending(QueryExpression<LaboratoriosDto>.GetOrderByExpression(request.OrderBy.Column));
                }
            }

            if (laboratorios == null)
            {
                throw new KeyNotFoundException($"No se encontraron datos asociados a resultados revisados");
            }



            return new Response<List<LaboratoriosDto>>(laboratoriosDto);
        }
    }
}
