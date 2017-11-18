using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using M = VainZero.WpfNavigationDemo.Models;
using L = VainZero.WpfNavigationDemo.Views.LayoutFrames;

namespace VainZero.WpfNavigationDemo.Views.FrontPages
{
    public sealed class FrontPage
        : BindableBase
        , L.ILayoutPage
    {
        public string PageTitle => "Front";

        public L.INavigationRequest UserListRequest { get; }

        public FrontPage(M.Model model)
        {
            UserListRequest =
                new L.NavigationRequest(() =>
                    new Users.Lists.UserListPage(model)
                );
        }
    }
}
