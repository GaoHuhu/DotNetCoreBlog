using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Jack.Gao.Blog.DataAccess
{
    public class DataAccessBase
    {
        public static readonly string CONNECTION_STRING = @"Server=***;Initial Catalog=***;
                                                Persist Security Info=False;User ID=***;Password=***;MultipleActiveResultSets=False;
                                                Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static bool ExecuteNonQuery(string sql, string connectionString, SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();

                        command.Parameters.AddRange(parameters);

                        int count = command.ExecuteNonQuery();

                        if (count > 0)
                            return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }
    }
}
