using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNavigationExample.Reactive.Commands
{
    /// <summary>
    /// Represents a command which provides a way to raise events.
    /// </summary>
    public interface IRaisableCommand<in TParameter>
        : ICommand<TParameter>
    {
        /// <summary>
        /// Refreshes can-execute values.
        /// </summary>
        void Refresh();
    }
}
