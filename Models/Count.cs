using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CRUD_with_sql.Models
{
    public class Count
    {
        private readonly IConfiguration _configuration;

        public Count(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Dictionary<string, int> GetEntityCounts()
        {
            var counts = new Dictionary<string, int>();

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("my_cstring")))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("PR_Counts", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            counts.Add(reader["EntityName"].ToString(), (int)reader["EntityCount"]);
                        }
                    }
                }
            }

            return counts;
        }
    }
}