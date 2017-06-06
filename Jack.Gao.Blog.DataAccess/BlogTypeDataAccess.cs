using Jack.Gao.Blog.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Jack.Gao.Blog.DataAccess
{
    public class BlogTypeDataAccess
    {
        public static int GetBlogTypeCount()
        {
            using (SqlConnection conn = new SqlConnection(DataAccessBase.CONNECTION_STRING))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand("select count(*) from blogtype", conn))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        return  reader.GetInt32(0);
                    }
                }
            }

            return 0;
        }

        public static List<BlogTypeViewModel> GetBlogTypes(out int pageCount, out int total, int pageIndex = 1, int pageRows = 5)
        {
            pageCount = 0;
            total = 0;

            List<BlogTypeViewModel> blogTypes = new List<BlogTypeViewModel>();

            using (SqlConnection conn = new SqlConnection(DataAccessBase.CONNECTION_STRING))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand("Proc_BlogType_Page", conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@pageIndex", System.Data.SqlDbType.Int).Value = pageIndex;

                    command.Parameters.Add("@pageRows", System.Data.SqlDbType.Int).Value = pageRows;

                    command.Parameters.Add("@pageCount", System.Data.SqlDbType.Int);
                    command.Parameters["@pageCount"].Direction = System.Data.ParameterDirection.Output;

                    command.Parameters.Add("@total", System.Data.SqlDbType.Int);
                    command.Parameters["@total"].Direction = System.Data.ParameterDirection.Output;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        BlogTypeViewModel blog = new BlogTypeViewModel();

                        blog.Id = reader.GetGuid(0);
                        blog.Name = reader.GetString(1);
                        blog.CreatedTime = reader.GetDateTime(2);
                        blog.UpdatedTime = reader.GetDateTime(3);

                        blogTypes.Add(blog);
                    }

                    reader.Dispose();

                    if (command.Parameters["@pageCount"].Value != null)
                        pageCount = int.Parse(command.Parameters["@pageCount"].Value.ToString());

                    if (command.Parameters["@total"].Value != null)
                        total = int.Parse(command.Parameters["@total"].Value.ToString());
                }
            }

            return blogTypes;
        }

        public static bool DeleteBlogType(string id)
        {
            string sql = "delete from blogtype Where Id=@Id";

            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@Id";

            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            else
            {
                Guid guid = new Guid(id);
                p1.Value = guid;
            }
            parameters.Add(p1);

            return DataAccessBase.ExecuteNonQuery(sql, DataAccessBase.CONNECTION_STRING, parameters.ToArray());
        }

        public static bool SaveBlogType(string id, string name)
        {
            string sql = string.Empty;

            if (string.IsNullOrEmpty(id))
            {
                sql = "insert into blogtype(Id,Name,CreatedTime,UpdatedTime) values (@Id,@Name,@CreatedTime,@UpdatedTime)";
            }
            else
            {
                sql = "Update blogtype set Name=@Name, CreatedTime=@CreatedTime, UpdatedTime=@UpdatedTime where Id=@Id";
            }

            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@Id";

            if (string.IsNullOrEmpty(id))
            {
                p1.Value = Guid.NewGuid();
            }
            else
            {
                Guid guid = new Guid(id);
                p1.Value = guid;
            }
            parameters.Add(p1);

            SqlParameter p2 = new SqlParameter();

            p2.ParameterName = "@Name";
            p2.Value = name;

            parameters.Add(p2);

            SqlParameter p3 = new SqlParameter();

            p3.ParameterName = "@CreatedTime";
            p3.Value = DateTime.Now;

            parameters.Add(p3);

            SqlParameter p4 = new SqlParameter();

            p4.ParameterName = "@UpdatedTime";
            p4.Value = DateTime.Now;

            parameters.Add(p4);

            return DataAccessBase.ExecuteNonQuery(sql, DataAccessBase.CONNECTION_STRING, parameters.ToArray());
        }
    }
}
