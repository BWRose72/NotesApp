using Microsoft.Data.SqlClient;

namespace DataAcessLayer
{
    public static class DatabaseHelper
    {
        private const string connString = "Server=.\\SQLEXPRESS01; Database = NotesDB; Integrated Security=true; TrustServerCertificate=true";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connString);
        }
    }
}
