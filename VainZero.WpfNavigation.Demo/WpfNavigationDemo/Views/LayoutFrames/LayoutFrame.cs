using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using VainZero.Reactive.Commands;
using DotNetKit.Misc;
using System.Windows.Input;

namespace VainZero.WpfNavigationDemo.Views.LayoutFrames
{
    public sealed class LayoutFrame
        : BindableBase
    {
        public Navigator<ILayoutPage> Navigator { get; }

        public ILayoutPage Content => Navigator.Current;

        private void Navigate(INavigationRequest request)
        {
            var page = request.CreatePage();
            if (page == null) return;

            Navigator.Navigate(page);
            RaisePropertyChanged(nameof(Content));

            NavigateCommand.Refresh();
            NavigateBackCommand.Refresh();
            NavigateForwardCommand.Refresh();
        }

        public IRaisableCommand<INavigationRequest> NavigateCommand { get; }
        public IRaisableCommand<Unit> NavigateBackCommand { get; }
        public IRaisableCommand<Unit> NavigateForwardCommand { get; }

        public LayoutFrame(ILayoutPage initialPage, Models.Model model)
        {
            var commandFactory =
                new RaisableCommandFactory(
                    h => CommandManager.RequerySuggested += h,
                    h => CommandManager.RequerySuggested -= h
                );

            Navigator = new Navigator<ILayoutPage>(initialPage);

            NavigateCommand =
                commandFactory.Create<INavigationRequest>(
                    Navigate,
                    _ => true
                );

            NavigateBackCommand =
                commandFactory.Create(
                    () =>
                    {
                        Navigator.NavigateBack(1);
                        RaisePropertyChanged(nameof(Content));
                    },
                    () => Navigator.CanNavigateBack(1)
                );

            NavigateForwardCommand =
                commandFactory.Create(
                    () =>
                    {
                        Navigator.NavigateForward(1);
                        RaisePropertyChanged(nameof(Content));
                    },
                    () => Navigator.CanNavigateForward(1)
                );
        }
    }
}
