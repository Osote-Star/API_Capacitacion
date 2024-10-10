using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.DTOs.User
{
    public class CreateUserDTO
    {
        public string Nombres { get; set; }
        public string Usuario { get; set; }
        public string contraseña { get; set; }
    }
}
