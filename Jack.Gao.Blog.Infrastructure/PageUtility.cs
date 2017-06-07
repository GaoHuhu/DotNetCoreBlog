using System;

namespace Jack.Gao.Blog.Infrastructure
{
    public class PageUtility
    {
        public static void BuildPageParameters(out int previous, out int next, out int currentPageIndex, out int first, out int last, int pageIndex, int count)
        {
            previous = 1;

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

            next = 1;

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

            currentPageIndex = 1;

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

            first = 1;
            last = count == 0 ? 1 : count;
        }
    }
}
