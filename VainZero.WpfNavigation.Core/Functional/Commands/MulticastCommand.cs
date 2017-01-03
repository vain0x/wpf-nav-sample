using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DotNetKit.Functional.Commands;

namespace VainZero.Functional.Commands
{
    public abstract class MulticastCommand<TParameter>
        : BaseCommand<TParameter>
    {
        public abstract void Add(ICommand<TParameter> command);
        public abstract bool Remove(ICommand<TParameter> command);
    }

    public sealed class NeverMulticastCommand<TParameter>
        : MulticastCommand<TParameter>
    {
        public override event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public override bool CanExecute(TParameter parameter)
        {
            return false;
        }

        public override void Execute(TParameter parameter)
        {
            throw new InvalidOperationException();
        }

        public override void Add(ICommand<TParameter> command)
        {
        }

        public override bool Remove(ICommand<TParameter> command)
        {
            return false;
        }

        public static NeverMulticastCommand<TParameter> Instance { get; } =
            new NeverMulticastCommand<TParameter>();
    }
}
