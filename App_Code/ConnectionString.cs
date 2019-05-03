using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ConnectionString
/// </summary>
public class ConnectionString
{
    public static string connectionString = "Data Source=ABHRAJYOTI;Initial Catalog=scheduleit;Integrated Security=True";

    public static string getConnectionString()
    {
        return connectionString;
    }
}