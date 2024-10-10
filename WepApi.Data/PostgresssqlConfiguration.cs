using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Data
{
    public class PostgresssqlConfiguration
    {
        public string Connection { get; set; }

        public PostgresssqlConfiguration(string connection) => Connection = connection;
    }
}
