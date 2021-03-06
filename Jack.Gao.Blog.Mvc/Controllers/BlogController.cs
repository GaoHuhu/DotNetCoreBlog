using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Jack.Gao.Blog.ViewModel;
using Jack.Gao.Blog.DataAccess;
using Jack.Gao.Blog.Infrastructure;
using System.Data.SqlClient;

namespace Jack.Gao.Blog.Mvc.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index(int pageIndex = 1, int pageRows = 5)
        {
            List<BlogViewModel> blogs = new List<BlogViewModel>();

            int count = 0;
            int total = 0;
            int currentPageIndex = 0;
            int firstPage = 0;
            int lastPage = 0;
            int previous = 0;
            int next = 0;

            blogs = BlogDataAccess.GetBlogs(out count, out total, pageIndex, pageRows);

            PageUtility.BuildPageParameters(out previous, out next, out currentPageIndex, out firstPage, out lastPage, pageIndex, count);

            string pageUrl = @"/Blog/Index?pageIndex={0}&pageRows={1}";

            string firstPageUrl = string.Format(pageUrl, firstPage, pageRows);
            string lastPageUrl = string.Format(pageUrl, lastPage, pageRows);
            string previousPageUrl = string.Format(pageUrl, previous, pageRows);
            string nextPageUrl = string.Format(pageUrl, next, pageRows);

            ViewBag.Blogs = blogs;

            ViewBag.Count = count;
            ViewBag.Total = total;
            ViewBag.PageIndex = currentPageIndex;
            ViewBag.FirstPageUrl = firstPageUrl;
            ViewBag.LastPageUrl = lastPageUrl;
            ViewBag.PreviousPageUrl = previousPageUrl;
            ViewBag.NextPageUrl = nextPageUrl;

            return View();
        }

        public bool SaveBlog(string id, string title, string content)
        {
            bool success = false;

            string sql = string.Empty;

            if (string.IsNullOrEmpty(id))
            {
                sql = "insert into blog(Id,Title,Content,CreatedTime,UpdatedTime) values (@Id,@Title,@Content,@CreatedTime,@UpdatedTime)";
            }
            else
            {
                sql = "Update blog set Title=@Title, Content=@Content, UpdatedTime=@UpdatedTime where Id=@Id";
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
            p2.ParameterName = "@Title";
            p2.Value = title;
            parameters.Add(p2);

            SqlParameter p3 = new SqlParameter();
            p3.ParameterName = "@Content";
            p3.Value = content;
            parameters.Add(p3);

            if (string.IsNullOrEmpty(id))
            {
                SqlParameter p4 = new SqlParameter();

                p4.ParameterName = "@CreatedTime";
                p4.Value = DateTime.Now;

                parameters.Add(p4);
            }

            SqlParameter p5 = new SqlParameter();
            p5.ParameterName = "@UpdatedTime";
            p5.Value = DateTime.Now;
            parameters.Add(p5);

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

        public bool DeleteBlog(string id)
        {
            string sql = "delete from blog Where Id=@Id;delete from comment where blogid=@blogid";

            List<SqlParameter> parameters = new List<SqlParameter>();

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@Id";

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@blogid";

            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            else
            {
                Guid guid = new Guid(id);
                p1.Value = guid;
                p2.Value = guid;
            }
            parameters.Add(p1);
            parameters.Add(p2);

            return DataAccessBase.ExecuteNonQuery(sql, DataAccessBase.CONNECTION_STRING, parameters.ToArray());
        }
    }
}