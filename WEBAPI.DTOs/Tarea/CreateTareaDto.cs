﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.DTOs.Tarea
{
    public class CreateTareaDto
    {
        public string Tarea { get; set; }
        public string Descripcion { get; set; }
        public int IdUsuario { get; set; }
    }
}
