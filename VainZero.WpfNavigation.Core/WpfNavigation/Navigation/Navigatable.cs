using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VainZero.Functional.Commands;
using VainZero.Windows.Navigation;

namespace VainZero.WpfNavigation.Navigation
{
    public abstract class Navigatable
        : INavigatable
    {
        /// <summary>
        /// ページのタイトルを取得する。
        /// </summary>
        public abstract string Title { get; }

        public MulticastCommand<INavigatable> NavigateCommand { get; } =
            new DisjunctiveCommand<INavigatable>();

        public MulticastCommand<int> NavigateBackCommand { get; } =
            new DisjunctiveCommand<int>();

        public MulticastCommand<int> NavigateForwardCommand { get; } =
            new DisjunctiveCommand<int>();
    }
}
