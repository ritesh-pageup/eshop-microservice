using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.API.Provider.Database
{
    public class ConnectionHelper
    {
        private readonly IConfiguration config;

        public ConnectionHelper(IConfiguration config)
        {
            this.config = config;
        }
        public string GetConnectionString()
        {
            return (config.GetSection("MongoDB:ConnectionString").Value ?? "");
        }
        public string GetDatabaseName()
        {
            return (config.GetSection("MongoDB:DatabaseName").Value ?? "");
        }
    }
}
