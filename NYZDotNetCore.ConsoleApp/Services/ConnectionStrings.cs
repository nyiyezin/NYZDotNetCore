using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYZDotNetCore.ConsoleApp.Services
{
    internal static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-U6LKCT1",
            InitialCatalog = "NYZDotNetCore",
            UserID = "nyiye",
            Password = "N0FCr%I4`0'm",
            TrustServerCertificate = true,
        };

    }
}
