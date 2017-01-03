using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetKit.Functional.Commands;
using VainZero.WpfNavigation.Navigation;

namespace VainZero.WpfNavigation.Demo
{
    public sealed class MenuPage
        : Navigatable
    {
        public override string Title => "Menu";

        public UnitCommand UsersCommand { get; }

        public MenuPage()
        {
            UsersCommand =
                new UnitCommand(() =>
                {
                    NavigateCommand.Execute(new Users.UserListPage());
                });
        }
    }
}
