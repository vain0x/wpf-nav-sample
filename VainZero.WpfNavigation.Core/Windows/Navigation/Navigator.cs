using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetKit.Functional.Commands;
using DotNetKit.Misc;
using Reactive.Bindings;
using VainZero.Collections;

namespace VainZero.Windows.Navigation
{
    public sealed class Navigator
    {
        static readonly EmptyNavigatable emptyNavigatable =
            new EmptyNavigatable();

        public ReactiveCollection<INavigatable> BackEntries { get; } =
            new ReactiveCollection<INavigatable>();

        public ReactiveCollection<INavigatable> ForwardEntries { get; } =
            new ReactiveCollection<INavigatable>();

        readonly ReactiveProperty<INavigatable> current =
            new ReactiveProperty<INavigatable>(emptyNavigatable);

        public IReadOnlyReactiveProperty<INavigatable> Current => current;

        public Command<INavigatable> NavigateCommand { get; }
        public Command<int> NavigateBackCommand { get; }
        public Command<int> NavigateForwardCommand { get; }

        void OnCanNavigateChanged()
        {
            NavigateBackCommand.RaiseCanExecuteChanged();
            NavigateForwardCommand.RaiseCanExecuteChanged();
        }

        INavigatable NavigateCore(INavigatable navigatable)
        {
            var previous = Current.Value;

            // Detach.
            previous.NavigateCommand.Remove(NavigateCommand);
            previous.NavigateBackCommand.Remove(NavigateBackCommand);
            previous.NavigateForwardCommand.Remove(NavigateForwardCommand);

            // Attach.
            navigatable.NavigateCommand.Add(NavigateCommand);
            navigatable.NavigateBackCommand.Add(NavigateBackCommand);
            navigatable.NavigateForwardCommand.Add(NavigateForwardCommand);

            current.Value = navigatable;

            return previous;
        }

        void Navigate(INavigatable navigatable)
        {
            var previous = NavigateCore(navigatable);

            if (previous != emptyNavigatable)
            {
                BackEntries.Add(previous);
            }

            ForwardEntries.Clear();

            OnCanNavigateChanged();
        }

        void NavigateBack(int count)
        {
            ForwardEntries.Add(Current.Value);

            for (var i = 0; i < count - 1; i++)
            {
                var x = BackEntries.RemoveLast();
                ForwardEntries.Add(x);
            }

            var navigatable = BackEntries.RemoveLast();

            NavigateCore(navigatable);

            OnCanNavigateChanged();
        }

        void NavigateForward(int count)
        {
            BackEntries.Add(Current.Value);

            for (var i = 0; i < count - 1; i++)
            {
                var x = ForwardEntries.RemoveLast();
                BackEntries.Add(x);
            }

            var navigatable = ForwardEntries.RemoveLast();

            NavigateCore(navigatable);

            OnCanNavigateChanged();
        }

        public Navigator(INavigatable initial)
        {
            NavigateBackCommand =
                CommandModule.Create<int>(NavigateBack, k => BackEntries.Count >= k);

            NavigateForwardCommand =
                CommandModule.Create<int>(NavigateForward, k => ForwardEntries.Count >= k);

            NavigateCommand =
                CommandModule.Create<INavigatable>(Navigate);

            Navigate(initial);
        }
    }
}
