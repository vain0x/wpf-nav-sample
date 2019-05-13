using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using M = WpfNavigationExample.Models;
using L = WpfNavigationExample.Views.LayoutFrames;

namespace WpfNavigationExample.Views.FrontPages
{
    /// <summary>
    /// The first page shown after sign in.
    /// </summary>
    public sealed class FrontPage
        : BindableBase
        , L.ILayoutPage
    {
        public string PageTitle => "Front";

        public L.INavigateRequest UserListRequest { get; }

        public FrontPage(M.Model model)
        {
            UserListRequest =
                new L.NavigateRequest(() =>
                    new Users.Lists.UserListPage(model)
                );
        }
    }
}
