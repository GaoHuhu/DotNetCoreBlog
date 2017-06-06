using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Jack.Gao.Blog.ViewModel;
using Jack.Gao.Blog.DataAccess;

namespace Jack.Gao.Blog.Mvc.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        #region Blog

        public IActionResult Blog(int pageIndex = 1, int pageRows = 5)
        {
            List<BlogViewModel> blogs = new List<BlogViewModel>();

            int count = 0;
            int total = 0;

            blogs = BlogDataAccess.GetBlogs(out count, out total, pageIndex, pageRows);

            ViewBag.PageIndex = pageIndex;
            ViewBag.Count = count;
            ViewBag.Total = total;
            ViewBag.Blogs = blogs;

            return View();
        }

        public PartialViewResult Page(PageModel pageModel)
        {
            int previous = 1;

            previous = pageModel.PageIndex - 1;

            if (previous <= 0)
            {
                previous = 1;
            }
            else
            {
                if (previous > pageModel.Count && pageModel.Count > 0)
                {
                    previous = pageModel.Count;
                }
            }

            int next = 1;

            next = pageModel.PageIndex + 1;

            if (next <= 0)
            {
                next = 1;
            }
            else
            {
                if (next > pageModel.Count && pageModel.Count > 0)
                {
                    next = pageModel.Count;
                }
            }

            int currentPageIndex = 1;

            if (currentPageIndex <= 0)
            {
                currentPageIndex = 1;
            }
            else if (currentPageIndex > pageModel.Count)
            {
                if (pageModel.Count > 0)
                    currentPageIndex = pageModel.Count;
                else
                    currentPageIndex = 1;
            }
            else
            {
                currentPageIndex = pageModel.PageIndex;
            }

            int firstPage = 1;
            int lastPage = pageModel.Count == 0 ? 1 : pageModel.Count;

            ViewBag.PageCounts = pageModel.Count;
            ViewBag.Previous = previous;
            ViewBag.Next = next;
            ViewBag.CurrentIndex = currentPageIndex;
            ViewBag.Total = pageModel.Total;
            ViewBag.FirstPage = firstPage;
            ViewBag.LastPage = lastPage;

            return PartialView();
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

        #endregion

        #region BlogType
        public IActionResult BlogType(int pageIndex = 1, int pageRows = 5)
        {
            int count = 0;
            int total = 0;

            List<BlogTypeViewModel> blogTypes = new List<BlogTypeViewModel>();

            blogTypes = BlogTypeDataAccess.GetBlogTypes(out count, out total, pageIndex, pageRows);

            int previous = 1;

            previous = pageIndex - 1;

            if (previous <= 0)
            {
                previous = 1;
            }
            else
            {
                if (previous > count && count > 0)
                {
                    previous = count;
                }
            }

            int next = 1;

            next = pageIndex + 1;

            if (next <= 0)
            {
                next = 1;
            }
            else
            {
                if (next > count && count > 0)
                {
                    next = count;
                }
            }

            int currentPageIndex = 1;

            if (currentPageIndex <= 0)
            {
                currentPageIndex = 1;
            }
            else if (currentPageIndex > count)
            {
                if (count > 0)
                    currentPageIndex = count;
                else
                    currentPageIndex = 1;
            }
            else
            {
                currentPageIndex = pageIndex;
            }

            ViewBag.PageCounts = count;

            ViewBag.Previous = previous;
            ViewBag.Next = next;
            ViewBag.CurrentIndex = currentPageIndex;
            ViewBag.Total = total;

            ViewBag.BlogTypes = blogTypes;

            return View();
        }

        public bool SaveBlogType(string id, string name)
        {
            return BlogTypeDataAccess.SaveBlogType(id, name);
        }

        public bool DeleteBlogType(string id)
        {
            return BlogTypeDataAccess.DeleteBlogType(id);
        }
        #endregion
    }
}