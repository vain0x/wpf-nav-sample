using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNavigationExample.Views.LayoutFrames
{
    /// <summary>
    /// Represents a page to be hosted in <see cref="LayoutFrame"/>.
    /// </summary>
    public interface ILayoutPage
    {
        string PageTitle { get; }
    }
}
