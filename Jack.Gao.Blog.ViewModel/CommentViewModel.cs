using System;
using System.Collections.Generic;
using System.Text;

namespace Jack.Gao.Blog.ViewModel
{
    public class CommentViewModel : ViewModelBase
    {
        public Guid CommentId { get; set; }
        public Guid BlogId { get; set; }
        public string Comment { get; set; }
    }
}
