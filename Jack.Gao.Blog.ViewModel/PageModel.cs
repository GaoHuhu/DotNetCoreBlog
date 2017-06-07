using System;
using System.Collections.Generic;
using System.Text;

namespace Jack.Gao.Blog.ViewModel
{
    public class PageModel
    {
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
        //public int First { get; set; }
        //public int Previous { get; set; }
        //public int Next { get; set; }
        //public int Last { get; set; }
        //public string PageUrl { get; set; }
        public string FirstPageUrl { get; set; }
        public string LastPageUrl { get; set; }
        public string PreviousPageUrl { get; set; }
        public string NextPageUrl { get; set; }
    }
}
