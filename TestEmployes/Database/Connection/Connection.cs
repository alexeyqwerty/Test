using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestEmployes.Database.Connection
{
    public class Connection
    {
        private static SqlConnection sqlConnection { get; set; }

        private static SqlCommand sqlCommand { get; set; }  

        private static SqlDataReader dataReader { get; set; }

        static Connection()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"database.mdf");

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; AttachDbFilename=" +
                filePath + ";Integrated Security = True; Connect Timeout = 30; " +
                "Encrypt=False;TrustServerCertificate=True;" +
                "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            sqlConnection = new SqlConnection(connectionString);
        }

        public static void Open()
        {
            sqlConnection.Open();
        }

        public static void Close() 
        {
            if(sqlConnection != null) 
            {
                sqlConnection.Close();
            }
        }

        public static List<string>GetData(string query)
        {
            sqlCommand = new SqlCommand(query, sqlConnection);
            dataReader = sqlCommand.ExecuteReader();

            List<string> result = new List<string>();

            while(dataReader.Read()) 
            {
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    result.Add(dataReader[i].ToString());
                }                
            }

            dataReader.Close();

            return result;
        }

        public static void Save(Dictionary<string, object> data, string query)
        {
            sqlCommand = new SqlCommand(query, sqlConnection);

            foreach(var dictItem in data)
            {
                sqlCommand.Parameters.AddWithValue(dictItem.Key, dictItem.Value.ToString());
            }

            sqlCommand.ExecuteNonQuery();
        }

        public static void Remove(string query)
        {
            sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.ExecuteNonQuery();
        }

    }
}
