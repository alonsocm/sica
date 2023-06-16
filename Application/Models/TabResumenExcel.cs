

namespace Application.Models
{
    public class TabResumenExcel
    {
        //Tabla resultados por parametro
        public string? Parametro { get; set; }
        //Tabla resultados totales
        
        public int? Resultados_Aprobados { get; set; }
        public int? Resultados_Rechazados { get; set; }
    }
}
