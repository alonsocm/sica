using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Muestreos.Queries
{
    public class GetTotalesMuestreosAdministracionQuery: IRequest<Response<List<VwEstatusMuestreosAdministracion>>>
    {
    }

    public class GetTotalesMuestreosAdministracionQueryHandler : IRequestHandler<GetTotalesMuestreosAdministracionQuery, Response<List<VwEstatusMuestreosAdministracion>>>
    {
        private readonly IVwEstatusMuestreosAdministracionRepository _repository;

        public GetTotalesMuestreosAdministracionQueryHandler(IVwEstatusMuestreosAdministracionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<List<VwEstatusMuestreosAdministracion>>> Handle(GetTotalesMuestreosAdministracionQuery request, CancellationToken cancellationToken)
        {
            //    return new Response<List<int?>>(anios);
            //var totales = await _repository.ObtenerTodosElementosAsync();
            return new Response<List<VwEstatusMuestreosAdministracion>>(_repository.ObtenerAdminsitracionMuestreos());
            
        }
    }
}
