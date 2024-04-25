using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYZDotNetCore.ConsoleApp
{
    internal static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-IQ53SCH",
            InitialCatalog = "NYZDotNetCore",
            UserID = "sa",
            Password = "sa@123",
            TrustServerCertificate = true,
        };
        
    }
}
