using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetKit.Misc;
using VainZero.Functional.Commands;

namespace VainZero.Windows.Navigation
{
    public sealed class EmptyNavigatable
        : INavigatable
    {
        public MulticastCommand<INavigatable> NavigateCommand =>
            NeverMulticastCommand<INavigatable>.Instance;

        public MulticastCommand<int> NavigateBackCommand =>
            NeverMulticastCommand<int>.Instance;

        public MulticastCommand<int> NavigateForwardCommand =>
            NeverMulticastCommand<int>.Instance;
    }
}
