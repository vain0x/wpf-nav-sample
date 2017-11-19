using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VainZero.Reactive.Commands;
using DotNetKit.Misc;
using VainZero.Collections;
using System.ComponentModel;

namespace VainZero.WpfNavigationDemo.Views.LayoutFrames
{
    public sealed class Navigator<TState>
    {
        public ObservableCollection<TState> BackEntries { get; } =
            new ObservableCollection<TState>();

        public ObservableCollection<TState> ForwardEntries { get; } =
            new ObservableCollection<TState>();

        private TState _current;
        public TState Current => _current;

        public void Navigate(TState state)
        {
            BackEntries.Add(Current);
            ForwardEntries.Clear();

            _current = state;
        }

        public bool CanNavigateBack(int count)
        {
            return BackEntries.Count >= count;
        }

        public void NavigateBack(int count)
        {
            if (!CanNavigateBack(count))
                throw new InvalidOperationException("Couldn't navigate back.");

            ForwardEntries.Add(Current);

            for (var i = 0; i < count - 1; i++)
            {
                var state = BackEntries.RemoveLast();
                ForwardEntries.Add(state);
            }

            _current = BackEntries.RemoveLast();
        }

        public bool CanNavigateForward(int count)
        {
            return ForwardEntries.Count >= count;
        }

        public void NavigateForward(int count)
        {
            if (!CanNavigateForward(count))
                throw new InvalidOperationException("Couldn't navigate forward.");

            BackEntries.Add(Current);

            for (var i = 0; i < count - 1; i++)
            {
                var state = ForwardEntries.RemoveLast();
                BackEntries.Add(state);
            }

            _current = ForwardEntries.RemoveLast();
        }

        public Navigator(TState initialState)
        {
            _current = initialState;
        }
    }
}
