using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DotNetKit.Misc;

namespace WpfNavigationExample.Reactive.Commands
{
    /// <summary>
    /// Basic implementation of <see cref="IRaisableCommand{TParameter}"/>.
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    public sealed class RaisableCommand<TParameter>
        : IRaisableCommand<TParameter>
    {
        private readonly Func<TParameter, bool> _canExecute;
        private readonly Action<TParameter> _execute;

        public event EventHandler CanExecuteChanged;

        public void Refresh()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(TParameter parameter)
        {
            return _canExecute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return CanExecute((TParameter)parameter);
        }

        public void Execute(TParameter parameter)
        {
            if (!CanExecute(parameter))
                throw new InvalidOperationException("Can't execute command.");

            _execute(parameter);
        }

        public void Execute(object parameter)
        {
            Execute((TParameter)parameter);
        }

        public RaisableCommand(Func<TParameter, bool> canExecute, Action<TParameter> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }
    }

    public static class RaisableCommand
    {
        /// <summary>
        /// Creates a raisable command.
        /// </summary>
        public static IRaisableCommand<P> Create<P>(Action<P> execute, Func<P, bool> canExecute)
        {
            return new RaisableCommand<P>(canExecute, execute);
        }

        /// <summary>
        /// Creates a raisable command that needs no parameter.
        /// </summary>
        public static IRaisableCommand<Unit> Create(Action execute, Func<bool> canExecute)
        {
            return Create<Unit>(_ => execute(), _ => canExecute());
        }
    }
}
