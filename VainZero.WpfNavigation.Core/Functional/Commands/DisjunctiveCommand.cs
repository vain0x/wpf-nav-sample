using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DotNetKit.Functional.Commands;

namespace VainZero.Functional.Commands
{
    public sealed class DisjunctiveCommand<TParameter>
        : MulticastCommand<TParameter>
    {
        List<ICommand<TParameter>> Commands { get; } =
            new List<ICommand<TParameter>>();

        public override event EventHandler CanExecuteChanged;

        void OnCanExecuteChanged(object sender, EventArgs e)
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public override bool CanExecute(TParameter parameter)
        {
            return Commands.Any(command => command.CanExecute(parameter));
        }

        public override void Execute(TParameter parameter)
        {
            foreach (var command in Commands.ToArray())
            {
                if (command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }
            }
        }

        public override void Add(ICommand<TParameter> command)
        {
            command.CanExecuteChanged += OnCanExecuteChanged;
            Commands.Add(command);
        }

        public override bool Remove(ICommand<TParameter> command)
        {
            if (Commands.Remove(command))
            {
                command.CanExecuteChanged -= OnCanExecuteChanged;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
