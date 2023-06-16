using Application.Wrappers;
using AutoMapper;
using MediatR;
using Application.DTOs;
using Domain.Entities;
using Application.Specifications;
using Application.Interfaces.IRepositories;

namespace Application.Features.Paginas.Queries
{
    public class GetAllPaginasByPerfil : IRequest<Response<List<PaginaDto>>>
    {
        public string Perfil { get; set; }
    }

    public class GetAllMenuPaginasHandler : IRequestHandler<GetAllPaginasByPerfil, Response<List<PaginaDto>>>
    {
        private readonly IRepositoryAsync<PerfilPagina> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllMenuPaginasHandler(IRepositoryAsync<PerfilPagina> repository, IMapper mapper)
        {
            _repositoryAsync = repository;
            _mapper = mapper;
        }

        //Obtener todos los registros de la tabla Pagina con estado activo = 1
        public async Task<Response<List<PaginaDto>>> Handle(GetAllPaginasByPerfil request, CancellationToken cancellationToken)
        {
            var datos = await _repositoryAsync.ListAsync(new PaginasByPerfilSpec(request.Perfil),cancellationToken);

            if (datos == null)
            {
                throw new KeyNotFoundException($"Datos de menus no encontrados en estado activo");
            }

            var menus = _mapper.Map<List<PaginaDto>>(datos);
            return new Response<List<PaginaDto>>(menus);
        }
    }
}
