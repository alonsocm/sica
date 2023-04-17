﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PerfilDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estatus { get; set; }

        public PerfilDto()
        {
            this.Nombre = "";
            this.Estatus = false;
        }
    }

   
}
