using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI.Data
{
    public class PostgressqlConfiguration
    {
        public string Connection { get; set; }

        public PostgressqlConfiguration(string connection) => Connection = connection;
        
    }
}
