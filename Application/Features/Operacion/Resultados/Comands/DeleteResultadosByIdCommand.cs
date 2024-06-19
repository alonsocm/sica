using Application.Expressions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Resultados.Comands
{
    public class DeleteResultadosByIdCommand : IRequest<Response<bool>>
    {
        public List<long> lstResultadosId { get; set; }
    }

    public class DeleteResultadosByIdCommandHandler : IRequestHandler<DeleteResultadosByIdCommand, Response<bool>>
    {
        private readonly IMuestreoRepository _repositoryAsync;
        private readonly IResultado _resultadoRepository;
        public DeleteResultadosByIdCommandHandler(IMuestreoRepository repositoryAsync, IResultado resultadoRepository)
        {
            _repositoryAsync = repositoryAsync;
            _resultadoRepository = resultadoRepository;
        }
        public async Task<Response<bool>> Handle(DeleteResultadosByIdCommand request, CancellationToken cancellationToken)
        {
            foreach (var idResultado in request.lstResultadosId)
            { _resultadoRepository.Eliminar(x => x.Id == idResultado); }
            return new Response<bool> { Succeded = true };
        }
    }
}
