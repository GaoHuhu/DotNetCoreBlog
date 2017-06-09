using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Jack.Gao.Blog.ViewModel;
using Jack.Gao.Blog.DataAccess;
using Jack.Gao.Blog.Infrastructure;

namespace Jack.Gao.Blog.Mvc.Controllers
{
    public class BlogTypeController : Controller
    {
        public IActionResult Index(int pageIndex = 1, int pageRows = 5)
        {
            int count = 0;
            int total = 0;
            int currentPageIndex = 0;
            int firstPage = 0;
            int lastPage = 0;
            int previous = 0;
            int next = 0;

            List<BlogTypeViewModel> blogTypes = new List<BlogTypeViewModel>();

            blogTypes = BlogTypeDataAccess.GetBlogTypes(out count, out total, pageIndex, pageRows);
            PageUtility.BuildPageParameters(out previous, out next, out currentPageIndex, out firstPage, out lastPage, pageIndex, count);

            string pageUrl = @"/BlogType/Index?pageIndex={0}&pageRows={1}";

            string firstPageUrl = string.Format(pageUrl, firstPage, pageRows);
            string lastPageUrl = string.Format(pageUrl, lastPage, pageRows);
            string previousPageUrl = string.Format(pageUrl, previous, pageRows);
            string nextPageUrl = string.Format(pageUrl, next, pageRows);

            ViewBag.PageCounts = count;
            ViewBag.Total = total;
            ViewBag.PageIndex = currentPageIndex;
            ViewBag.FirstPageUrl = firstPageUrl;
            ViewBag.LastPageUrl = lastPageUrl;
            ViewBag.PreviousPageUrl = previousPageUrl;
            ViewBag.NextPageUrl = nextPageUrl;

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
    }
}