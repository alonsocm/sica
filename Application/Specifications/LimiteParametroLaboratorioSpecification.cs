using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    public class LimiteParametroLaboratorioSpecification: Specification<LimiteParametroLaboratorio>
    {
        public LimiteParametroLaboratorioSpecification() {
            Query.Include(x => x.Parametro).Include(y => y.Laboratorio).Include(z => z.RealizaLaboratorioMuestreo).Include(a => a.LaboratorioMuestreo)
                    .Include(b => b.LaboratorioSubroga).Include(c => c.LoSubroga).Include(d => d.Anio);
        }
    }
}
