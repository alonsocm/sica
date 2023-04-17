using Application.DTOs;
using Application.Wrappers;
using MediatR;
using AutoMapper;
using Application.Interfaces.IRepositories;


namespace Application.Features.Validados.Queries
{
    public class UpdateResultadosCommand : IRequest<Response<bool>>
    {
        //public List<int> MuestreosId { get; set; }
        public long MuestreosId { get; set; }
        public int UsuarioId { get; set; }
        public int TipoAprobId { get; set; }
        public int EstatusId { get; set; }
        public List<ParametrosDto> lstparametros { get; set; }
        public bool isOCDL { get; set; }

        public UpdateResultadosCommand()
        {
            this.isOCDL = false;
            this.TipoAprobId = 0;
        }



    }

    public class UpdateResultadosHandler : IRequestHandler<UpdateResultadosCommand, Response<bool>>
    {
        private readonly IResumenResRepository _repository;
        private readonly IMuestreoRepository _repositorMuetreoyAsync;
        public UpdateResultadosHandler(IResumenResRepository repository, IMapper mapper, IMuestreoRepository muetreorepositorio)
        {
            _repository = repository;
            _repositorMuetreoyAsync = muetreorepositorio;


        }
        public async Task<Response<bool>> Handle(UpdateResultadosCommand request, CancellationToken cancellationToken)
        {
            try
            {
                int idEstatusVencido = 24;
                int idEstatusOtro = 11;
                string ObservacionVencido = "OK";
                long MuestreoId = 0;
                ParametrosDto param = new ParametrosDto();
                var Resultmuestro = await _repository.ObtenerElementosPorCriterioAsync(x => x.MuestreoId == request.MuestreosId);

                if (Resultmuestro != null)
                {
                    Resultmuestro = Resultmuestro.Select(m =>
                    {
                        param = request.lstparametros.Where(x => x.Id == m.ParametroId).ToList()[0];
                        MuestreoId = m.MuestreoId;

                        if (request.isOCDL)
                        {
                            m.EsCorrectoOcdl = (request.EstatusId == idEstatusVencido) ? true : ((param.ObservacionesOCDLId != null) ? false : true);
                            m.ObservacionesOcdl = (request.EstatusId == idEstatusVencido) ? ObservacionVencido : ((param.ObservacionesOCDLId == idEstatusOtro) ? param.ObservacionesOCDL : string.Empty);
                            m.ObservacionesOcdlid = (request.EstatusId == idEstatusVencido) ? idEstatusOtro : param.ObservacionesOCDLId;
                        }
                        else
                        {                           
                            m.EsCorrectoSecaia = (request.EstatusId == idEstatusVencido) ? true : ((param.ObservacionesSECAIAId != null) ? false : true);
                            m.ObservacionesSecaia = (request.EstatusId == idEstatusVencido) ? ObservacionVencido : ((param.ObservacionesSECAIAId == idEstatusOtro) ? param.ObservacionesSECAIA : string.Empty);
                            m.ObservacionesSecaiaid = (request.EstatusId == idEstatusVencido) ? idEstatusOtro : param.ObservacionesSECAIAId;
                            
                        }
                        return m;
                    });

                    if (Resultmuestro.ToList().Count > 0)
                    {
                        foreach (var resultmuestreo in Resultmuestro.ToList())
                        { _repository.Actualizar(resultmuestreo); }
                    }

                }

                var muestreos = await _repositorMuetreoyAsync.ObtenerElementosPorCriterioAsync(x => x.Id == MuestreoId);

                if (request.isOCDL)
                {
                    muestreos.ToList()[0].EstatusOcdl = request.EstatusId;
                    muestreos.ToList()[0].UsuarioRevisionOcdlid = request.UsuarioId;
                    muestreos.ToList()[0].FechaRevisionOcdl = DateTime.Now;
                    muestreos.ToList()[0].TipoAprobacionId = request.TipoAprobId;
                }
                else
                {
                    muestreos.ToList()[0].EstatusSecaia = request.EstatusId;
                    muestreos.ToList()[0].UsuarioRevisionSecaiaid = request.UsuarioId;
                    muestreos.ToList()[0].FechaRevisionSecaia = DateTime.Now;
                }

                _repositorMuetreoyAsync.Actualizar(muestreos.ToList()[0]);
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
