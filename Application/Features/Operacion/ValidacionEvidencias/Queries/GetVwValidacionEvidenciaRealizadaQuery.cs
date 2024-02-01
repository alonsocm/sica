using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.ValidacionEvidencias.Queries
{
    public class GetVwValidacionEvidenciaRealizadaQuery: IRequest<Response<List<VwValidacionEvidenciaRealizada>>>
    {
        public bool rechazo { get; set; }
    }

    public class GetVwValidacionEvidenciaRealizadaQueryHandler : IRequestHandler<GetVwValidacionEvidenciaRealizadaQuery, Response<List<VwValidacionEvidenciaRealizada>>>
    {

        private readonly IVwValidacionEvidenciaRealizadaRepository _resultadosEvidenciaRepository;
        public GetVwValidacionEvidenciaRealizadaQueryHandler(IVwValidacionEvidenciaRealizadaRepository resultadosEvidenciaRepository)
        {
            _resultadosEvidenciaRepository = resultadosEvidenciaRepository;

        }

        public async Task<Response<List<VwValidacionEvidenciaRealizada>>> Handle(GetVwValidacionEvidenciaRealizadaQuery request, CancellationToken cancellationToken)
        {
            var datos = _resultadosEvidenciaRepository.ObtenerValidacionesRealizadas(request.rechazo);
            return new Response<List<VwValidacionEvidenciaRealizada>>((datos == null) ? new List<VwValidacionEvidenciaRealizada>() : datos.ToList());
        }
    }
}
