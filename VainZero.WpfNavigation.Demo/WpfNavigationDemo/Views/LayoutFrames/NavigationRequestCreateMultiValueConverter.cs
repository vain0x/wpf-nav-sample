using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace VainZero.WpfNavigationDemo.Views.LayoutFrames
{
    public sealed class NavigationRequestCreateMultiValueConverter
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

        public static NavigationRequestCreateMultiValueConverter Instance { get; } =
            new NavigationRequestCreateMultiValueConverter();
    }

    public interface INavigateRequestFactory
    {
        INavigationRequest Create(object parameter);
    }

    public interface INavigateRequestFactory<in TParameter>
        : INavigateRequestFactory
    {
        INavigationRequest Create(TParameter parameter);
    }

    public sealed class AnonymousNavigateFactory<TParameter>
        : INavigateRequestFactory<TParameter>
    {
        private readonly Func<TParameter, INavigationRequest> _create;

        public INavigationRequest Create(TParameter parameter)
        {
            return _create(parameter);
        }

        INavigationRequest INavigateRequestFactory.Create(object parameter)
        {
            return Create((TParameter)parameter);
        }

        public AnonymousNavigateFactory(Func<TParameter, INavigationRequest> create)
        {
            _create = create;
        }
    }
}
