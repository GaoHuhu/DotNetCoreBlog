using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Jack.Gao.Blog.Mvc.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(string message)
        {
            if (string.IsNullOrEmpty(message))
                ViewBag.Message = "Please enter valid data!";

            ViewBag.Message = message;

            return View();
        }
    }
}