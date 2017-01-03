using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VainZero.Windows.Navigation;
using Reactive.Bindings;

namespace VainZero.WpfNavigation.Demo
{
    public sealed class History
    {
        Navigator Navigator { get; }

        public ReactiveCollection<INavigatable> BackEntries =>
            Navigator.BackEntries;

        public ReactiveCollection<INavigatable> ForwardEntries =>
            Navigator.ForwardEntries;

        public History(Navigator navigator)
        {
            Navigator = navigator;
        }
    }
}
