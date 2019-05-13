using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using WpfNavigationExample.Reactive.Commands;
using DotNetKit.Misc;
using System.Windows.Input;

namespace WpfNavigationExample.Views.LayoutFrames
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
            _navigateCommand.Refresh();
            _navigateBackCommand.Refresh();
            _navigateForwardCommand.Refresh();
        }

        private void Navigate(INavigateRequest request)
        {
            var page = request.CreatePage();
            if (page == null) return;

            var previousPage = _content;

            Navigator.Navigate(new PageChangeNavigation(this, page, previousPage));
            RefreshCommands();
        }

        readonly IRaisableCommand<INavigateRequest> _navigateCommand;
        public ICommand<INavigateRequest> NavigateCommand => _navigateCommand;

        readonly IRaisableCommand<Unit> _navigateBackCommand;
        public ICommand<Unit> NavigateBackCommand => _navigateBackCommand;

        readonly IRaisableCommand<Unit> _navigateForwardCommand;
        public ICommand<Unit> NavigateForwardCommand => _navigateForwardCommand;

        public LayoutFrame(ILayoutPage initialPage, Models.Model model)
        {
            _content = initialPage;

            Navigator = new Navigator<INavigation>();

            _navigateCommand =
                RaisableCommand.Create<INavigateRequest>(
                    Navigate,
                    _ => true
                );

            _navigateBackCommand =
                RaisableCommand.Create(
                    () => Navigator.NavigateBack(1),
                    () => Navigator.CanNavigateBack(1)
                );

            _navigateForwardCommand =
                RaisableCommand.Create(
                    () => Navigator.NavigateForward(1),
                    () => Navigator.CanNavigateForward(1)
                );
        }
    }
}
