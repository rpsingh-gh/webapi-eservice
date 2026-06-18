using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Eshopping_WebAPI.DBservices
{
    public class dbConnector
    {
        private SqlConnection SqlConn = null;
        public SqlConnection GetConnection
        {
            get { return SqlConn; }
            set { SqlConn = value; }
        }

        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();

        }

        public dbConnector()
        {
            var configuation = GetConfiguration();
            SqlConn = new SqlConnection(configuation.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        }
    }
}
