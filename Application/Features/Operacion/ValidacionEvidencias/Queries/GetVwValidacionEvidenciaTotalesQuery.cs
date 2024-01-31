using Application.DTOs.EvidenciasMuestreo;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.ValidacionEvidencias.Queries
{
    public class GetVwValidacionEvidenciaTotalesQuery : IRequest<Response<List<VwValidacionEvidenciaTotales>>>
    {
    }

    public class GetVwValidacionEvidenciaTotalesQueryHandler : IRequestHandler<GetVwValidacionEvidenciaTotalesQuery, Response<List<VwValidacionEvidenciaTotales>>>
    {

        private readonly IVwValidacionEvidenciaTotalesRepository _resultadosEvidenciaRepository;
        public GetVwValidacionEvidenciaTotalesQueryHandler(IVwValidacionEvidenciaTotalesRepository resultadosEvidenciaRepository)
        {
            _resultadosEvidenciaRepository = resultadosEvidenciaRepository;

        }

        public async Task<Response<List<VwValidacionEvidenciaTotales>>> Handle(GetVwValidacionEvidenciaTotalesQuery request, CancellationToken cancellationToken)
        {
            var datos = _resultadosEvidenciaRepository.ObtenerResultadosValidacion();
            return new Response<List<VwValidacionEvidenciaTotales>>((datos == null) ? new List<VwValidacionEvidenciaTotales>() : datos.ToList());
        }
    }
}
