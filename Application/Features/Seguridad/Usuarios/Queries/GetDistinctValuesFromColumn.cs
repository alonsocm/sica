using Application.DTOs;
using Application.DTOs.Users;
using Application.Expressions;
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

namespace Application.Features.Seguridad.Usuarios.Queries
{
    public class GetDistinctValuesFromColumn:IRequest<Response<IEnumerable<object>>>
    {
        public string Column { get; set; }
        public List<Filter> Filters { get; set; }
    }

    public class GetDistinctValuesFromColumnHandler : IRequestHandler<GetDistinctValuesFromColumn, Response<IEnumerable<object>>>
    {
        private readonly IMuestreoRepository _repositoryAsync;
        private IRepositoryAsync<Usuario> _repository;
        private readonly IMapper _mapper;

        public GetDistinctValuesFromColumnHandler(IMuestreoRepository repositoryAsync, IRepositoryAsync<Usuario> repository, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<object>>> Handle(GetDistinctValuesFromColumn request, CancellationToken cancellationToken)
        {
            //var data = await _repositoryAsync.GetResumenMuestreosAsync(new List<long> { (long)Enums.EstatusMuestreo.Cargado, (long)Enums.EstatusMuestreo.EvidenciasCargadas });

            var data = await _repository.ListAsync(new UsuariosPerfilesSpec(), cancellationToken);
            var userDto = _mapper.Map<List<UserDto>>(data);

            foreach (var user in userDto)
            {
                user.NombreCompleto = user.Nombre + ' ' + user.ApellidoPaterno + ' ' + user.ApellidoMaterno;
                
            }

            if (request.Filters.Any())
            {
                var expressions =  QueryExpression<UserDto>.GetExpressionList(request.Filters);              

                foreach (var filter in expressions)
                {
                    //data = data.AsQueryable().Where(filter);

                }
            }

            var response = _repositoryAsync.GetDistinctValuesFromColumn(request.Column, userDto);

            return new Response<IEnumerable<object>>(response);
        }
    }
}
