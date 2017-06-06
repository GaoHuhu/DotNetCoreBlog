using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Jack.Gao.Blog.DataAccess;
using System.Data.SqlClient;
using System.Data;
using Jack.Gao.Blog.ViewModel;

namespace Jack.Gao.Blog.Mvc.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index(string blogId,int pageIndex = 1, int pageRows = 5)
        {
            List<CommentViewModel> comments = new List<CommentViewModel>();

            //int count = 0;
            //int total = 0;

            using (SqlConnection conn = new SqlConnection(DataAccessBase.CONNECTION_STRING))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand("select blogid, commentid, comment, createdtime, updatedtime from comment where blogId=@blogId", conn))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@blogId", SqlDbType.UniqueIdentifier).Value = new Guid(blogId);
                    //command.Parameters.Add("@pageIndex", System.Data.SqlDbType.Int).Value = pageIndex;

                    //command.Parameters.Add("@pageRows", System.Data.SqlDbType.Int).Value = pageRows;

                    //command.Parameters.Add("@pageCount", System.Data.SqlDbType.Int);
                    //command.Parameters["@pageCount"].Direction = System.Data.ParameterDirection.Output;

                    //command.Parameters.Add("@total", System.Data.SqlDbType.Int);
                    //command.Parameters["@total"].Direction = System.Data.ParameterDirection.Output;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        CommentViewModel comment = new CommentViewModel();

                        comment.BlogId = reader.GetGuid(0);
                        comment.CommentId = reader.GetGuid(1);
                        comment.Comment = reader.GetString(2);
                        comment.CreatedTime = reader.GetDateTime(3);
                        comment.UpdatedTime = reader.GetDateTime(4);

                        comments.Add(comment);
                    }

                    if (!reader.IsClosed)
                    {
                        reader.Dispose();
                    }

                    //if (command.Parameters["@pageCount"].Value != null)
                    //    count = int.Parse(command.Parameters["@pageCount"].Value.ToString());

                    //if (command.Parameters["@total"].Value != null)
                    //    total = int.Parse(command.Parameters["@total"].Value.ToString());
                }
            }

            //int previous = 1;

            //previous = pageIndex - 1;

            //if (previous <= 0)
            //{
            //    previous = 1;
            //}
            //else
            //{
            //    if (previous > count && count > 0)
            //    {
            //        previous = count;
            //    }
            //}

            //int next = 1;

            //next = pageIndex + 1;

            //if (next <= 0)
            //{
            //    next = 1;
            //}
            //else
            //{
            //    if (next > count && count > 0)
            //    {
            //        next = count;
            //    }
            //}

            //int currentPageIndex = 1;

            //if (currentPageIndex <= 0)
            //{
            //    currentPageIndex = 1;
            //}
            //else if (currentPageIndex > count)
            //{
            //    if (count > 0)
            //        currentPageIndex = count;
            //    else
            //        currentPageIndex = 1;
            //}
            //else
            //{
            //    currentPageIndex = pageIndex;
            //}

            //int firstPage = 1;
            //int lastPage = count == 0 ? 1 : count;

            //ViewBag.PageCounts = count;

            //ViewBag.Previous = previous;
            //ViewBag.Next = next;
            //ViewBag.CurrentIndex = currentPageIndex;
            //ViewBag.Total = total;
            //ViewBag.FirstPage = firstPage;
            //ViewBag.LastPage = lastPage;
            ViewBag.Comments =comments;
            ViewBag.BlogId = blogId;

            return View();
        }

        public bool SaveComment(string comment,string blogId)
        {
            bool success = false;

            string sql = string.Empty;

            //if (string.IsNullOrEmpty(id))
            //{
                sql = "insert into Comment(CommentId, BlogId, Comment, CreatedTime, UpdatedTime) values (newid(), @BlogId, @Commnet, getdate(), getdate())";
            //}
            //else
            //{
            //    sql = "Update blog set Title=@Title, Content=@Content, UpdatedTime=@UpdatedTime where Id=@Id";
            //}

            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@BlogId";
            //if (string.IsNullOrEmpty(id))
            //{
            //    p1.Value = Guid.NewGuid();
            //}
            //else
            //{
                Guid guid = new Guid(blogId);
                p1.Value = guid;
            //}
            parameters.Add(p1);

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@Commnet";
            p2.Value = comment;
            parameters.Add(p2);

            try
            {
                success = DataAccessBase.ExecuteNonQuery(sql, DataAccessBase.CONNECTION_STRING, parameters.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return success;
        }
    }
}