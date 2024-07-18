using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Users
{
    public class UserDto
    {
        public int Id { get; set; }    
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }
        public long? CuencaId { get; set; }
        public long? DireccionLocalId { get; set; }
        public long PerfilId { get; set; }
        public string NombrePerfil { get; set; }

        public string NombreCompleto { get;  set; }

        public UserDto()
        {
            NombreCompleto = this.Nombre + ' ' + this.ApellidoPaterno + ' ' + this.ApellidoMaterno;
        }
    }

    
}
