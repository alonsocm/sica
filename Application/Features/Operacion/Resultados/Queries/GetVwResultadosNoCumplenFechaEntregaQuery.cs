using Application.Features.Operacion.ValidacionEvidencias.Queries;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Resultados.Queries
{
    public class GetVwResultadosNoCumplenFechaEntregaQuery: IRequest<Response<List<VwResultadosNoCumplenFechaEntrega>>>
    {
        public List<long> muestreosId { get; set; } = new List<long>();
    }

    public class GetVwResultadosNoCumplenFechaEntregaQueryHandler : IRequestHandler<GetVwResultadosNoCumplenFechaEntregaQuery, Response<List<VwResultadosNoCumplenFechaEntrega>>>
    {

        private readonly IVwResultadosNoCumplenFechaEntregaRepository _resultadosEvidenciaRepository;
        public GetVwResultadosNoCumplenFechaEntregaQueryHandler(IVwResultadosNoCumplenFechaEntregaRepository resultadosEvidenciaRepository)
        {
            _resultadosEvidenciaRepository = resultadosEvidenciaRepository;

        }

        public async Task<Response<List<VwResultadosNoCumplenFechaEntrega>>> Handle(GetVwResultadosNoCumplenFechaEntregaQuery request, CancellationToken cancellationToken)
        {
            var datos = _resultadosEvidenciaRepository.ObtenerDatos().Where(x => request.muestreosId.Contains(x.MuestreoId));
            return new Response<List<VwResultadosNoCumplenFechaEntrega>>((datos == null) ? new List<VwResultadosNoCumplenFechaEntrega>() : datos.ToList());
        }
    }
}
