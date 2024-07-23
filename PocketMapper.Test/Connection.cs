using BlazorProject.Domain;
using Microsoft.Data.SqlClient;

namespace PocketMapper.Test
{
    public class PocketTest
    {        
        private static readonly SqlConnection con = new SqlConnection("change your connection string here");

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