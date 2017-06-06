using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

using Jack.Gao.Blog.DataAccess;

namespace Jack.Gao.Blog.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            int blogCount = 0;

            int blogTypeCount = 0;

            blogCount = BlogDataAccess.GetBlogCount();

            blogTypeCount = BlogTypeDataAccess.GetBlogTypeCount();

            ViewBag.BlogCount = blogCount;

            ViewBag.BlogTypeCount= blogTypeCount;

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
