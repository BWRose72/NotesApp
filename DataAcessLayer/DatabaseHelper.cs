using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer
{
    public static class DatabaseHelper
    {
        private const string connString = "Server=.\\SQLEXPRESS; Database = NotesDB; Integrated Security=true; TrustServerCertificate=true";
        private const string connString2 = "Server=(localdb)\\MSSQLLocalDB; Database = NotesDB; Integrated Security=true; TrustServerCertificate=true";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connString);
        }
    }
}
