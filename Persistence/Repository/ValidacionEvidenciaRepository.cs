using Application.DTOs;
using Application.DTOs.EvidenciasMuestreo;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class ValidacionEvidenciaRepository : Repository<ValidacionEvidencia>, IValidacionEvidenciaRepository
    {
        public ValidacionEvidenciaRepository(SicaContext context) : base(context)
        {
        }

        public ValidacionEvidencia ConvertirValidacionEvidencia(vwValidacionEvienciasDto validacionMuestreo)
        {

            ValidacionEvidencia validacionEvidencia = new ValidacionEvidencia();
            validacionEvidencia.MuestreoId = 0;
            validacionEvidencia.CumpleEvidenciasEsperadas = (validacionMuestreo.CumpleEvidencias == "SI") ? true:false;
            




            return validacionEvidencia;
        }

    }
}
