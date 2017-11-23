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
    public sealed class Navigator<TNavigation>
        where TNavigation : INavigation
    {
        public ObservableCollection<TNavigation> BackEntries { get; } =
            new ObservableCollection<TNavigation>();

        public ObservableCollection<TNavigation> ForwardEntries { get; } =
            new ObservableCollection<TNavigation>();

        public void Navigate(TNavigation navigation)
        {
            BackEntries.Add(navigation);
            ForwardEntries.Clear();

            navigation.GoForward();
        }

        public bool CanNavigateBack(int count)
        {
            return BackEntries.Count >= count;
        }

        public void NavigateBack(int count)
        {
            if (!CanNavigateBack(count))
                throw new InvalidOperationException("Couldn't navigate back.");

            for (var i = 0; i < count; i++)
            {
                var navigation = BackEntries.RemoveLast();
                ForwardEntries.Add(navigation);

                navigation.GoBack();
            }
        }

        public bool CanNavigateForward(int count)
        {
            return ForwardEntries.Count >= count;
        }

        public void NavigateForward(int count)
        {
            if (!CanNavigateForward(count))
                throw new InvalidOperationException("Couldn't navigate forward.");

            for (var i = 0; i < count; i++)
            {
                var navigation = ForwardEntries.RemoveLast();
                BackEntries.Add(navigation);
                navigation.GoForward();
            }
        }
    }

    public interface INavigation
    {
        void GoForward();
        void GoBack();
    }
}
