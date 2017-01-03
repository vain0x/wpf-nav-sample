using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetKit.Functional.Commands;
using VainZero.WpfNavigation.Navigation;
using Reactive.Bindings;

namespace VainZero.WpfNavigation.Demo
{
    public sealed class LoginPage
        : Navigatable
    {
        public override string Title => "Login";

        public ReactiveProperty<string> UserId { get; } =
            new ReactiveProperty<string>("john_due");

        public UnitCommand LoginCommand { get; }

        public LoginPage()
        {
            LoginCommand =
                new UnitCommand(() =>
                {
                    NavigateCommand.Execute(new MenuPage());
                });
        }
    }
}
