using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.DTOs.User
{
    internal class UpdateUserDTO
    {
        public string Nombres { get; set; }

        public string Usuario { get; set; }

        public string Contraseña { get; set; }
    }
}
