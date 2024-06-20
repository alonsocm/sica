using Application.DTOs;
using Application.Expressions;
using Application.Features.Operacion.Resultados.Queries;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Resultados.Comands
{
    public class DeleteResultadosByMuestreoIdCommand : IRequest<Response<bool>>
    {
        public List<long> lstMuestreoId { get; set; }

    }

    public class DeleteResultadosByMuestreoIdCommandHandler : IRequestHandler<DeleteResultadosByMuestreoIdCommand, Response<bool>>
    {

        private readonly IResultado _resultadoRepository;
        public DeleteResultadosByMuestreoIdCommandHandler(IResultado resultadoRepository)
        {
            _resultadoRepository = resultadoRepository;
        }
        public async Task<Response<bool>> Handle(DeleteResultadosByMuestreoIdCommand request, CancellationToken cancellationToken)
        {
            foreach (long idResultado in request.lstMuestreoId)
            { _resultadoRepository.Eliminar(x => x.MuestreoId == idResultado); }
            return new Response<bool> { Succeded = true };
        }
    }
}
