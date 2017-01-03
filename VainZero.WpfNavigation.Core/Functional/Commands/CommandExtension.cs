using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetKit.Functional.Commands;
using DotNetKit.Misc;

namespace VainZero.Functional.Commands
{
    public static class CommandExtension
    {
        public static ICommand<Unit> PartiallyApply<X>(this ICommand<X> @this, X x)
        {
            return new PartiallyAppliedCommand<X>(@this, x);
        }
    }
}
