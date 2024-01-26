﻿using Application.DTOs;
using Application.DTOs.EvidenciasMuestreo;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IVwValidacionEvienciasRepository : IRepository<VwValidacionEviencias>
    {
        List<VwValidacionEviencias> ObtenerDatosGenerales();
    }
}
