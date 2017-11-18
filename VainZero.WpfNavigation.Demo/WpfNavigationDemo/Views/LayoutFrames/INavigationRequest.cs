using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VainZero.WpfNavigationDemo.Views.LayoutFrames
{
    public interface INavigationRequest
    {
        ILayoutPage CreatePage();
    }

    public sealed class NavigationRequest
        : INavigationRequest
    {
        private Func<ILayoutPage> _pageCreator;
        public ILayoutPage CreatePage()
        {
            return _pageCreator();
        }

        public NavigationRequest(Func<ILayoutPage> pageCreator)
        {
            _pageCreator = pageCreator;
        }
    }
}
