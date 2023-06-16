using Application.DTOs;
using Application.DTOs.Users;
using Application.Features.CargaMasivaEvidencias.Commands;
using Application.Features.Muestreos.Commands.Liberacion;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Models;
using Application.Wrappers;
using Domain.Entities;
//using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Operacion.Replicas.Commands
{
    public class CargaRevicionResultadoCommand : IRequest<Response<bool>>
    {
        public List<ResultadosconEstatus> Revision { get; set; } = new List<ResultadosconEstatus>();
        public long UsuairioId { get; set; }
    }
    public class CargaRevicionResultadoHandler : IRequestHandler<CargaRevicionResultadoCommand, Response<bool>>
    {
        private readonly IArchivoService _archivos;      
        private readonly IMuestreoRepository _muestreoRepository;                
        private readonly IReplicas _aprovacionResultado;
        private readonly IVwReplicaRevisionResultadoRepository _vwReplica;

        public CargaRevicionResultadoHandler(IArchivoService archivos, IMuestreoRepository muestreoRepository, IReplicas aprovacionResultado, IVwReplicaRevisionResultadoRepository vwReplica)
        {
            _archivos = archivos;                 
            _muestreoRepository = muestreoRepository;          
            _aprovacionResultado = aprovacionResultado;
            _vwReplica = vwReplica;
        }  
        public async Task<Response<bool>> Handle(CargaRevicionResultadoCommand request, CancellationToken cancellationToken)
        {
            if (request.Revision.Count > 0)
            { 
                foreach (var revision in request.Revision)
                {
                    if (revision.Aprueba_Resultado== "")
                    {
                        continue;
                    }
                    else
                    {
                        string ApruebaResultado = revision.Aprueba_Resultado.ToUpper();
                        ApruebaResultado = ApruebaResultado.Trim();
                        if (ApruebaResultado == "SI" || ApruebaResultado == "NO")
                        {
                            var AprobacionResult = await _vwReplica.ObtenerElementosPorCriterioAsync(c => c.ClaveUnica == revision.Clave_Unica);

                            AprobacionResultadoMuestreo aprobacion = new AprobacionResultadoMuestreo();
                            aprobacion.ApruebaResultado = (ApruebaResultado == "SI" ? true : false);
                            aprobacion.ComentariosAprobacionResultados = revision.Comentarios_Aprobacion_Resultados;
                            aprobacion.FechaAprobRechazo = DateTime.Now;
                            aprobacion.UsuarioRevisionId = request.UsuairioId;
                            aprobacion.ResultadoMuestreoId = AprobacionResult.FirstOrDefault().ResultadoMuestreoId;
                            if (ApruebaResultado == "SI" && revision.Comentarios_Aprobacion_Resultados != "")
                            {
                                continue;
                            }
                            _aprovacionResultado.Insertar(aprobacion);                           
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            return new Response<bool>(true);
        }
    }
}
