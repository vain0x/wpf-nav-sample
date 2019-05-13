using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfNavigationExample.Views.LayoutFrames
{
    /// <summary>
    /// History of navigation.
    /// </summary>
    /// <typeparam name="TNavigation">
    /// Type of navigation events.
    /// You can define *what is navigation* using this type parameter.
    /// </typeparam>
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
                var navigation = ListPop(BackEntries);
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
                var navigation = ListPop(ForwardEntries);
                BackEntries.Add(navigation);
                navigation.GoForward();
            }
        }

        static T ListPop<T>(IList<T> list)
        {
            var count = list.Count;

            if (count == 0)
                throw new InvalidOperationException();

            var last = list[count - 1];
            list.RemoveAt(count - 1);
            return last;
        }
    }

    public interface INavigation
    {
        void GoForward();
        void GoBack();
    }
}
