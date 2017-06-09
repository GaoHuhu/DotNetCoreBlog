using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Jack.Gao.Blog.ViewModel;
using Jack.Gao.Blog.DataAccess;
using Jack.Gao.Blog.Infrastructure;

namespace Jack.Gao.Blog.Mvc.Controllers
{
    public class ManagerController : Controller
    {      
        public IActionResult Index()
        {
            return View();
        }

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