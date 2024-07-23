using BlazorProject.Domain;
using Microsoft.Data.SqlClient;

namespace PocketMapper.Test
{
    public class PocketTest
    {
        private static readonly SqlConnection con = new SqlConnection("Server=tcp:sgarcez.database.windows.net,1433;Initial Catalog=Blazor;Persist Security Info=False;User ID=sgarcez;Password=P@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public PocketTest()
        {
            
        }

        public static List<T> GetList<T>(string tableName) where T : new()
        {
            SqlCommand command = new SqlCommand($"select * from {tableName}", con);

            con.Open();
            var reader = command.ExecuteReader().ConvertMap<T>();
            con.Close();

            return reader;
        }
    }
}