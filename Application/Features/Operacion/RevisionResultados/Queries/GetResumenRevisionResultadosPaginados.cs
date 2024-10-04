using Application.DTOs;
using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.ResumenResultados.Queries
{
    public class GetResumenRevisionResultadosPaginados : IRequest<PagedResponse<IEnumerable<ResultadoMuestreoDto>>>
    {
        public int EstatusId { get; set; }
        public int UserId { get; set; }
        public bool isOCDL { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<Filter> Filter { get; set; }
        public OrderBy? OrderBy { get; set; }

        public GetResumenRevisionResultadosPaginados()
        {
            this.isOCDL = false;
        }


    }

    public class GetResumenRevisionResultadosPaginadosHandler : IRequestHandler<GetResumenRevisionResultadosPaginados, PagedResponse<IEnumerable<ResultadoMuestreoDto>>>
    {
        private readonly IResumenResRepository _repository;
        private readonly IMapper _mapper;

        public GetResumenRevisionResultadosPaginadosHandler(IResumenResRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //Obtener todos los registros de las tablas Muestreo y ResultadoMuestreo acorde al idMuestreo
        public async Task<PagedResponse<IEnumerable<ResultadoMuestreoDto>>> Handle(GetResumenRevisionResultadosPaginados request, CancellationToken cancellationToken)
        {
            var datos = await _repository.GetResumenResultadosMuestreoAsync(request.EstatusId, request.UserId, request.isOCDL);
            if (datos == null)
            {
                throw new KeyNotFoundException($"No se encontraron datos asociados a resultados revisados");
            }

            var datosDto = _mapper.Map<IEnumerable<ResultadoMuestreoDto>>(datos); // Mapear datos a DTO

            if (request.Filter != null && request.Filter.Any())
            {
                var expresiones = QueryExpression<ResultadoMuestreoDto>.GetExpressionList(request.Filter);

                foreach (var filtro in expresiones)
                {
                    datosDto = datosDto.AsQueryable().Where(filtro);
                }
            }

            if (request.OrderBy != null && !string.IsNullOrEmpty(request.OrderBy.Type))
            {
                if (request.OrderBy.Type == "asc")
                {
                    datosDto = datosDto.AsQueryable().OrderBy(QueryExpression<ResultadoMuestreoDto>.GetOrderByExpression(request.OrderBy.Column));
                }
                else if (request.OrderBy.Type == "desc")
                {
                    datosDto = datosDto.AsQueryable().OrderByDescending(QueryExpression<ResultadoMuestreoDto>.GetOrderByExpression(request.OrderBy.Column));
                }
            }

            return PagedResponse<ResultadoMuestreoDto>.CreatePagedReponse(datosDto, request.Page, request.PageSize);
        }
    }
}