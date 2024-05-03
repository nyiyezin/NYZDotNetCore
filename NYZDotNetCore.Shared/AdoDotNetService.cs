using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;


namespace NYZDotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly String _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T>Query<T>(string query, params AdoDotNetParameter[]? parameters)
        {

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            if(parameters is not null && parameters.Length> 0)
            {
                sqlCommand.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());
            }

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            sqlConnection.Close();

            string json = JsonConvert.SerializeObject(dataTable);
            List<T> list = JsonConvert.DeserializeObject<List<T>>(json)!;
            return list;
        }

        public T QueryFirstOrDefault<T>(string query, params AdoDotNetParameter[]? parameters)
        {

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            if (parameters is not null && parameters.Length > 0)
            {
                sqlCommand.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());
            }

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            sqlConnection.Close();

            string json = JsonConvert.SerializeObject(dataTable);
            List<T> list = JsonConvert.DeserializeObject<List<T>>(json)!;
            return list[0];
        }

        public int Execute(string query, params AdoDotNetParameter[]? parameters)
        {

            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            if (parameters is not null && parameters.Length > 0)
            {
                sqlCommand.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());
            }
            var result = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return result;
        }

    }

    public class AdoDotNetParameter
    {
        public AdoDotNetParameter(string name, object value) 
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }

        public object Value { get; set; }
    }


}
