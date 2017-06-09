using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Jack.Gao.Blog.DataAccess
{
    public class DataAccessBase
    {
        public static readonly string CONNECTION_STRING = @"Server=tcp:myblogdatabaseserver.database.windows.net,1433;Initial Catalog=MyBlog;
                                                Persist Security Info=False;User ID=usersql;Password=Jack_12345678;MultipleActiveResultSets=False;
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
