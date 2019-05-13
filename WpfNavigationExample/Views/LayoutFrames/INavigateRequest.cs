using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNavigationExample.Views.LayoutFrames
{
    /// <summary>
    /// Request of navigation to go to new page.
    /// 
    /// Bind this to the command parameter of navigate command.
    /// </summary>
    public interface INavigateRequest
    {
        ILayoutPage CreatePage();
    }

    public sealed class NavigateRequest
        : INavigateRequest
    {
        private readonly Func<ILayoutPage> _pageCreator;

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
