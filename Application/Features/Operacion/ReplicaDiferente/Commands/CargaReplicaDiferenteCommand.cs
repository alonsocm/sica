using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.ReplicaDiferente.Commands
{
    public class CargaReplicaDiferenteCommand : IRequest<Response<bool>>
    {
        public List<GeneralDescargaDiferente> Resultados { get; set; } = new List<GeneralDescargaDiferente>();
    }

    public class CargaReplicaDiferenteCommandHandler : IRequestHandler<CargaReplicaDiferenteCommand, Response<bool>>
    {
        private readonly IVwReplicaRevisionResultadoRepository _repository;
        private readonly IResultado _repositoryExcel;
        public CargaReplicaDiferenteCommandHandler(IVwReplicaRevisionResultadoRepository repositoryAsync, IResultado repositoryExcel)
        {
            _repository = repositoryAsync;
            _repositoryExcel = repositoryExcel;
        }

        public async Task<Response<bool>> Handle(CargaReplicaDiferenteCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.Resultados)
            {
                var resultadovista = _repository.ObtenerElementosPorCriterio(f => f.ClaveUnica == item.ClaveUnica).FirstOrDefault();
                ResultadoMuestreo resultadoexcel = await _repositoryExcel.ObtenerElementoPorIdAsync(resultadovista.ResultadoMuestreoId);
                resultadoexcel.ObservacionSrenameca = item.ObservacionSRENAMECA;
                resultadoexcel.FechaObservacionSrenameca = DateTime.Now;
                resultadoexcel.Comentarios = item.ComentariosAprobacionResultados;
                _repositoryExcel.Actualizar(resultadoexcel);
            }

            return new Response<bool>();
        }
    }
}
