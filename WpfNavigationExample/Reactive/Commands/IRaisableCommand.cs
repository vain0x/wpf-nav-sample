using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNavigationExample.Reactive.Commands
{
    /// <summary>
    /// Command that provides a way to raise events.
    /// </summary>
    public interface IRaisableCommand<in TParameter>
        : ICommand<TParameter>
    {
        /// <summary>
        /// Force to recalculate can-execute value.
        /// </summary>
        void Refresh();
    }
}
