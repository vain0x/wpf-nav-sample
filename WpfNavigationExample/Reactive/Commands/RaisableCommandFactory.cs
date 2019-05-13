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
    public sealed class RaisableCommandFactory
    {
        private sealed class RaisableCommandImpl<TParameter>
            : IRaisableCommand<TParameter>
        {
            private readonly Func<TParameter, bool> _canExecute;
            private readonly Action<TParameter> _execute;
            private readonly Action<EventHandler> _onSubscribed;
            private readonly Action<EventHandler> _onUnsubscribed;

            private event EventHandler _canExecuteChanged;
            public event EventHandler CanExecuteChanged
            {
                add
                {
                    _canExecuteChanged += value;
                    _onSubscribed(value);
                }
                remove
                {
                    _canExecuteChanged -= value;
                    _onUnsubscribed(value);
                }
            }

            public void Refresh()
            {
                _canExecuteChanged?.Invoke(this, EventArgs.Empty);
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

            public RaisableCommandImpl(Func<TParameter, bool> canExecute, Action<TParameter> execute, Action<EventHandler> onSubscribed, Action<EventHandler> onUnsubscribed)
            {
                _canExecute = canExecute;
                _execute = execute;
                _onSubscribed = onSubscribed;
                _onUnsubscribed = onUnsubscribed;
            }
        }

        private readonly Action<EventHandler> _onSubscribed;
        private readonly Action<EventHandler> _onUnsubscribed;

        /// <summary>
        /// Creates a command.
        /// </summary>
        public IRaisableCommand<P> Create<P>(Action<P> execute, Func<P, bool> canExecute)
        {
            return new RaisableCommandImpl<P>(canExecute, execute, _onSubscribed, _onUnsubscribed);
        }

        /// <summary>
        /// Creates a command with no parameter.
        /// </summary>
        public IRaisableCommand<Unit> Create(Action execute, Func<bool> canExecute)
        {
            return Create<Unit>(_ => execute(), _ => canExecute());
        }

        public RaisableCommandFactory(Action<EventHandler> onSubscribed, Action<EventHandler> onUnsubscribed)
        {
            _onSubscribed = onSubscribed;
            _onUnsubscribed = onUnsubscribed;
        }
    }
}
