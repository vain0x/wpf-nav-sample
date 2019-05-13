using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNavigationExample.Views.LayoutFrames
{
    /// <summary>
    /// Page to be hosted in <see cref="LayoutFrame"/>.
    /// </summary>
    public interface ILayoutPage
    {
        /// <summary>
        /// Text shown on the header (or perhaps title bar).
        /// </summary>
        string PageTitle { get; }
    }
}
