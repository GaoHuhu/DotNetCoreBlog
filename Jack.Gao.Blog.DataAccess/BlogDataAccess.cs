using Jack.Gao.Blog.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Jack.Gao.Blog.DataAccess
{
    public class BlogDataAccess
    {
        public static int GetBlogCount()
        {
            using (SqlConnection conn = new SqlConnection(DataAccessBase.CONNECTION_STRING))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand("select count(*) from blog", conn))
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

        public static List<BlogViewModel> GetBlogs(out int pageCount, out int total,int pageIndex = 1, int pageRows = 5)
        {
            pageCount = 0;
            total = 0;

            List<BlogViewModel> blogs = new List<BlogViewModel>();

            using (SqlConnection conn = new SqlConnection(DataAccessBase.CONNECTION_STRING))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand("Proc_Blog_Page", conn))
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
                        BlogViewModel blog = new BlogViewModel();

                        blog.BlogId = reader.GetGuid(0);
                        blog.Title = reader.GetString(1);
                        blog.Content = reader.GetString(2);
                        blog.CreatedTime = reader.GetDateTime(3);
                        blog.UpdatedTime = reader.GetDateTime(4);

                        blogs.Add(blog);
                    }

                    if (!reader.IsClosed)
                    {
                        reader.Dispose();
                    }

                    if (command.Parameters["@pageCount"].Value != null)
                        pageCount = int.Parse(command.Parameters["@pageCount"].Value.ToString());

                    if (command.Parameters["@total"].Value != null)
                        total = int.Parse(command.Parameters["@total"].Value.ToString());
                }
            }

            return blogs;
        }
    }
}
