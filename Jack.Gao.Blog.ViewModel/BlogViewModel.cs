using System;
using System.Collections.Generic;
using System.Text;

namespace Jack.Gao.Blog.ViewModel
{
    public class BlogViewModel : ViewModelBase
    {
        public Guid BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int CommentCount { get; set; }
    }
}
