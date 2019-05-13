using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNavigationExample.Collections
{
    public static class ListExtension
    {
        public static X RemoveLast<X>(this IList<X> @this)
        {
            var count = @this.Count;

            if (count == 0)
            {
                throw new InvalidOperationException();
            }

            var x = @this[count - 1];
            @this.RemoveAt(count - 1);
            return x;
        }
    }
}
