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
        sealed class PageChangeNavigation
            : INavigation
        {
            private readonly LayoutFrame _parent;
            private readonly ILayoutPage _page;
            private readonly ILayoutPage _previousPage;

            public string PageTitle => _page.PageTitle;

            public void GoBack()
            {
                _parent.Content = _previousPage;
            }

            public void GoForward()
            {
                _parent.Content = _page;
            }

            public PageChangeNavigation(LayoutFrame parent, ILayoutPage page, ILayoutPage previousPage)
            {
                _parent = parent;
                _page = page;
                _previousPage = previousPage;
            }
        }

        public Navigator<INavigation> Navigator { get; }

        private ILayoutPage _content;
        public ILayoutPage Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        private void RefreshCommands()
        {
            NavigateCommand.Refresh();
            NavigateBackCommand.Refresh();
            NavigateForwardCommand.Refresh();
        }

        private void Navigate(INavigateRequest request)
        {
            var page = request.CreatePage();
            if (page == null) return;

            var previousPage = _content;

            Navigator.Navigate(new PageChangeNavigation(this, page, previousPage));
            RefreshCommands();
        }

        public IRaisableCommand<INavigateRequest> NavigateCommand { get; }
        public IRaisableCommand<Unit> NavigateBackCommand { get; }
        public IRaisableCommand<Unit> NavigateForwardCommand { get; }

        public LayoutFrame(ILayoutPage initialPage, Models.Model model)
        {
            var commandFactory =
                new RaisableCommandFactory(
                    h => CommandManager.RequerySuggested += h,
                    h => CommandManager.RequerySuggested -= h
                );

            _content = initialPage;

            Navigator = new Navigator<INavigation>();

            NavigateCommand =
                commandFactory.Create<INavigateRequest>(
                    Navigate,
                    _ => true
                );

            NavigateBackCommand =
                commandFactory.Create(
                    () => Navigator.NavigateBack(1),
                    () => Navigator.CanNavigateBack(1)
                );

            NavigateForwardCommand =
                commandFactory.Create(
                    () => Navigator.NavigateForward(1),
                    () => Navigator.CanNavigateForward(1)
                );
        }
    }
}
