﻿using System.Data.SqlClient;

namespace NYZDotNetCore.WinFormsAppSqlInjection;

internal class ConnectionStrings
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
