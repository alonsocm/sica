using Application.DTOs.Users;
using Application.Features.Catalogos.Emergencias.Commands;
using Application.Features.Usuarios.Queries;
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

namespace Application.Features.Operacion.SupervisionMuestreo.Queries
{
    public class GetClavesSitiosQuery: IRequest<Response<List<string>>>
    {
        public long OrganismosDireccionesRealizaId { get; set; }
    }

    public class GetClavesSitiosHandler : IRequestHandler<GetClavesSitiosQuery, Response<List<string>>>
    {
        private readonly IVw_SitiosRepository _sitiosRepository;
        public GetClavesSitiosHandler(IVw_SitiosRepository sitiosRepository)
        {
            _sitiosRepository = sitiosRepository;
        }

        public async Task<Response<List<string>>> Handle(GetClavesSitiosQuery request, CancellationToken cancellationToken)
        {
            var claveUnicas = await _sitiosRepository.ObtenerElementosPorCriterioAsync(x => x.CuencaDireccionesLocalesId == request.OrganismosDireccionesRealizaId);
            List<string> lstclavesUnicas = claveUnicas.Select(x => x.ClaveSitio).Distinct().ToList();

            return new Response<List<string>>(lstclavesUnicas);
        }
    }
}
