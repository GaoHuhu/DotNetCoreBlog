using Jack.Gao.Blog.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jack.Gao.Blog.Specification
{
    public class BlogTypeSpecification:SpecificationBase<BlogTypeViewModel>
    {
        public override bool IsValidation(BlogTypeViewModel t)
        {
            if (t == null)
                //throw new ArgumentNullException(nameof(t));
                return false;

            if (!string.IsNullOrEmpty(t.Name))
                return true;

            return false;
        }
    }
}
