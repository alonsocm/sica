using Application.DTOs;
using Application.Features.Replicas.Queries;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Replicas.Commandas
{
    public class updateAutorRechaAprobacionCommand : IRequest<Response<bool>>
    {
        public long MuestreoId { get; set; }
        public long UserId { get; set; }
        public long ResultadoMuestreoId { get; set; }
        public bool ApruebaResultado { get; set; }

    }

    public class updateAutorRechaAprobacionHandler : IRequestHandler<updateAutorRechaAprobacionCommand, Response<bool>>
    {
        private readonly IReplicas _repositoryAsync;

        public updateAutorRechaAprobacionHandler(IReplicas repository)
        {
            _repositoryAsync = repository;
        }
        //Obtener todos los registros de las tablas Muestreo,  ResultadoMuestreo y AprobacionResultadoMuestreo acorde al idMuestreo
        public async Task<Response<bool>> Handle(updateAutorRechaAprobacionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                AprobacionResultadoMuestreo resultado = new AprobacionResultadoMuestreo();
                //resultado.Id = 3;
                resultado.ApruebaResultado = request.ApruebaResultado;
                resultado.ComentariosAprobacionResultados = "";
                resultado.FechaAprobRechazo = DateTime.Now;
                resultado.UsuarioRevisionId = request.UserId;
                resultado.ResultadoMuestreoId = request.ResultadoMuestreoId;
                _repositoryAsync.Insertar(resultado);
                return new Response<bool>(true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false);
                throw new ApplicationException(ex.Message);

            }
        }
    }
}
