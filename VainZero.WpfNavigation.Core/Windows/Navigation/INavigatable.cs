using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetKit.Misc;
using VainZero.Functional.Commands;

namespace VainZero.Windows.Navigation
{
    /// <summary>
    /// ナビゲーションのコンテンツを表す。
    /// </summary>
    public interface INavigatable
    {
        MulticastCommand<INavigatable> NavigateCommand { get; }
        MulticastCommand<int> NavigateBackCommand { get; }
        MulticastCommand<int> NavigateForwardCommand { get; }
    }
}
