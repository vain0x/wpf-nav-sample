using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNavigationExample.Views.LayoutFrames
{
    public interface INavigateRequest
    {
        ILayoutPage CreatePage();
    }

    public sealed class NavigateRequest
        : INavigateRequest
    {
        private Func<ILayoutPage> _pageCreator;
        public ILayoutPage CreatePage()
        {
            return _pageCreator();
        }

        public NavigateRequest(Func<ILayoutPage> pageCreator)
        {
            _pageCreator = pageCreator;
        }
    }
}
