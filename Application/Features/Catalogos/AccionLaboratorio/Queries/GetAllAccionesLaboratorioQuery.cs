using Application.DTOs.Catalogos;
using Application.Features.Catalogos.Acuiferos.Queries;
using Application.Interfaces.IRepositories;
using Application.Specifications;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Catalogos.AccionLaboratorio.Queries
{
    public class GetAllAccionesLaboratorioQuery: IRequest<IEnumerable<AccionLaboratorioDto>>
    {
    }

    public class GetAllAccionesLaboratorioHandler : IRequestHandler<GetAllAccionesLaboratorioQuery, IEnumerable<AccionLaboratorioDto>>
    {
        private readonly IRepositoryAsync<Domain.Entities.AccionLaboratorio> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllAccionesLaboratorioHandler(IRepositoryAsync<Domain.Entities.AccionLaboratorio> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccionLaboratorioDto>> Handle(GetAllAccionesLaboratorioQuery request, CancellationToken cancellationToken)
        {
            var accionesLaboratorio = await _repositoryAsync.ListAsync();
            return _mapper.Map<IEnumerable<AccionLaboratorioDto>>(accionesLaboratorio);

        }
    }
}
