using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VainZero.Reactive.Commands
{
    /// <summary>
    /// Represents a command.
    /// </summary>
    public interface ICommand<in TParameter>
        : ICommand
    {
        /// <summary>
        /// Determines if it can execute with the specified parameter.
        /// </summary>
        bool CanExecute(TParameter parameter);

        /// <summary>
        /// Executes.
        /// </summary>
        void Execute(TParameter parameter);
    }
}
