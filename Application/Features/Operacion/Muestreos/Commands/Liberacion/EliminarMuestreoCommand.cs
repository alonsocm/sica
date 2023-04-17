using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Muestreos.Commands.Liberacion
{
    public class EliminarMuestreoCommand : IRequest<Response<bool>>
    {
        public List<int> Muestreos { get; set; }
    }

    public class EliminarMuestreoCommandHandler : IRequestHandler<EliminarMuestreoCommand, Response<bool>>
    {
        private readonly IMuestreoRepository _muestreoRepository;
        private readonly IResultado _resultadoRepository;
        private readonly IEvidenciaMuestreoRepository _evidenciaMuestreoRepository;

        public EliminarMuestreoCommandHandler(IMuestreoRepository muestreoRepository, IResultado resultadoRepository, IEvidenciaMuestreoRepository evidenciaMuestreoRepository)
        {
            _muestreoRepository=muestreoRepository;
            _resultadoRepository=resultadoRepository;
            _evidenciaMuestreoRepository=evidenciaMuestreoRepository;
        }

        public async Task<Response<bool>> Handle(EliminarMuestreoCommand request, CancellationToken cancellationToken)
        {
            if (request.Muestreos.Count > 0)
            {
                foreach (var muestreo in request.Muestreos)
                {
                    var muestreoDb = await _muestreoRepository.ObtenerElementoPorIdAsync(muestreo);

                    if (muestreoDb is null)
                    {
                        throw new KeyNotFoundException($"No se encontró el identificador: {muestreo}");
                    }

                    _evidenciaMuestreoRepository.EliminarEvidenciasMuestreo(muestreoDb.Id);

                    var resultados = await _resultadoRepository.ObtenerElementosPorCriterioAsync(r => r.MuestreoId == muestreo);

                    if (resultados.Any())
                    {
                        resultados.ToList().ForEach(resultado =>
                        {
                            _resultadoRepository.Eliminar(resultado);
                        });
                    }

                    _muestreoRepository.Eliminar(muestreoDb);
                }
            }

            return new Response<bool> { Succeded=true };
        }
    }
}
