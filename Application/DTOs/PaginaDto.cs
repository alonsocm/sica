
namespace Application.DTOs
{
    public class PaginaDto
    {
        public long Id { get; set; }
        public long? IdPaginaPadre { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Url { get; set; } = null!;
        public long Orden { get; set; }
        public bool Activo { get; set; }


    }
}
