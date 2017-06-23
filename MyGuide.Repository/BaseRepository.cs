using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace MyGuide.Repository
{
    public class BaseRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public T Query<T>(string functionName, object parameters)
        {
            using (var conn = Connection)
            {
                conn.Open();

                return conn.Query<T>(functionName, parameters, null, true, null, CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public List<T> QueryMultiple<T>(string functionName, object parameters = null)
        {
            using (var conn = Connection)
            {
                conn.Open();

                return conn.Query<T>(functionName, parameters, null, true, null, CommandType.StoredProcedure).ToList();
            }
        }

        public void Execute(string functionName, object parameters)
        {
            using (var conn = Connection)
            {
                conn.Open();

                Connection.Execute(functionName, parameters, null, null, CommandType.StoredProcedure);
            }
        }

    }
}
