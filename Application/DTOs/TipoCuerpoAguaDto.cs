namespace Application.DTOs
{
    public class TipoCuerpoAguaDto
    {
        public long Id { get; set; }
        public string Descripcion { get; set; }
        public int? TipoHomologadoId { get; set; }
        public string TipoHomologadoDescripcion { get; set; }
        public string Activo { get; set; }
        public string Frecuencia { get; set; }
        public int EvidenciasEsperadas { get; set; }
        public int TiempoMinimoMuestreo { get; set; }
    }
}
