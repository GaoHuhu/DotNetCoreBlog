using System;

namespace Jack.Gao.Blog.Specification
{
    public abstract class SpecificationBase<T> where T : class
    {
        public virtual bool IsValidation(T t)
        {
            if (t != null)
                return true;

            return false;
        }
    }
}
