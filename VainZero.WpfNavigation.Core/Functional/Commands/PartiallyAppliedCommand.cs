using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetKit.Functional.Commands;
using DotNetKit.Misc;

namespace VainZero.Functional.Commands
{
    sealed class PartiallyAppliedCommand<TParameter>
        : BaseCommand<Unit>
    {
        readonly ICommand<TParameter> command;
        readonly TParameter parameter;

        public override event EventHandler CanExecuteChanged
        {
            add { command.CanExecuteChanged += value; }
            remove { command.CanExecuteChanged -= value; }
        }

        public override bool CanExecute(Unit _)
        {
            return command.CanExecute(parameter);
        }

        public override void Execute(Unit _)
        {
            command.Execute(parameter);
        }

        internal PartiallyAppliedCommand(ICommand<TParameter> command, TParameter parameter)
        {
            this.command = command;
            this.parameter = parameter;
        }
    }
}
