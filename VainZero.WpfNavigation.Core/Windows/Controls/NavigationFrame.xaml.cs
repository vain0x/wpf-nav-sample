using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VainZero.Windows.Controls
{
    /// <summary>
    /// NavigationFrame.xaml の相互作用ロジック
    /// </summary>
    public partial class NavigationFrame : ContentControl
    {
        #region OnNavigatedCommand
        static readonly DependencyProperty onNavigatedCommandProperty =
            DependencyProperty.Register(
                nameof(OnNavigatedCommand),
                typeof(ICommand),
                typeof(NavigationFrame)
            );

        public static DependencyProperty OnNavigatedCommandProperty
        {
            get { return onNavigatedCommandProperty; }
        }

        public ICommand OnNavigatedCommand
        {
            get { return (ICommand)GetValue(OnNavigatedCommandProperty); }
            set { SetValue(OnNavigatedCommandProperty, value); }
        }
        #endregion

        public NavigationFrame()
        {
            InitializeComponent();
        }

        void OnContentLoaded(object sender, RoutedEventArgs e)
        {
            var command = OnNavigatedCommand;
            if (command != null)
            {
                command.Execute(sender);
            }
        }
    }
}
