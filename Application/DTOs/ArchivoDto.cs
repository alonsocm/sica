namespace Application.DTOs
{
    public class ArchivoDto
    {
        public string NombreArchivo { get; set; }
        public byte[] Archivo { get; set; }
        public string ContentType { get; set; }
    }
}
