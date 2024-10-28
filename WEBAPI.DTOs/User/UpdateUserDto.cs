using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.DTOs.User
{
    public class UpdateUserDto
    {
        public string Names { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
