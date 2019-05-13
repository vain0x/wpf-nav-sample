using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WpfNavigationExample.Views.LayoutFrames
{
    /// <summary>
    /// Converter to produce a <see cref="INavigation"/>.
    ///
    /// The parameter must be an array of two items:
    /// the first must be of <see cref="INavigateRequestFactory"/>,
    /// and the second is the argument of the factory.
    ///
    /// See <see cref="Users.Lists.UserListPageControl"/> for typical usage.
    /// </summary>
    public sealed class NavigateRequestCreateMultiValueConverter
        : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
                throw new InvalidOperationException();
            if (values.Contains(DependencyProperty.UnsetValue))
                return DependencyProperty.UnsetValue;

            var factory = (INavigateRequestFactory)values[0];
            var p = values[1];
            return factory.Create(p);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public static NavigateRequestCreateMultiValueConverter Instance { get; } =
            new NavigateRequestCreateMultiValueConverter();
    }

    /// <summary>
    /// Nongeneric <see cref="INavigateRequestFactory{TParameter}"/>.
    /// </summary>
    public interface INavigateRequestFactory
    {
        INavigateRequest Create(object parameter);
    }

    /// <summary>
    /// Function to create an <see cref="INavigateRequest"/>.
    /// </summary>
    /// <typeparam name="TParameter">
    /// The parameter of the function.
    /// </typeparam>
    public interface INavigateRequestFactory<in TParameter>
        : INavigateRequestFactory
    {
        INavigateRequest Create(TParameter parameter);
    }

    public sealed class NavigateRequestFactory<TParameter>
        : INavigateRequestFactory<TParameter>
    {
        private readonly Func<TParameter, INavigateRequest> _create;

        public INavigateRequest Create(TParameter parameter)
        {
            return _create(parameter);
        }

        INavigateRequest INavigateRequestFactory.Create(object parameter)
        {
            return Create((TParameter)parameter);
        }

        public NavigateRequestFactory(Func<TParameter, INavigateRequest> create)
        {
            _create = create;
        }
    }
}
