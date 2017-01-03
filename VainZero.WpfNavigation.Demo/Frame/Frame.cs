using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetKit.Functional.Commands;
using DotNetKit.Misc;
using VainZero.Functional.Commands;
using VainZero.Windows.Navigation;
using VainZero.WpfNavigation.Navigation;
using Reactive.Bindings;

namespace VainZero.WpfNavigation.Demo
{
    public sealed class Frame
    {
        public Navigator Navigator { get; }

        public History History { get; }

        public IReadOnlyReactiveProperty<Navigatable> Content { get; }

        public ICommand<Unit> NavigateBackCommand { get; }
        public ICommand<Unit> NavigateForwardCommand { get; }

        public Frame()
        {
            var front = new LoginPage();
            Navigator = new Navigator(front);
            History = new History(Navigator);
            Content = Navigator.Current.Cast<Navigatable>().ToReadOnlyReactiveProperty();

            NavigateBackCommand = Navigator.NavigateBackCommand.PartiallyApply(1);
            NavigateForwardCommand = Navigator.NavigateForwardCommand.PartiallyApply(1);
        }
    }
}
