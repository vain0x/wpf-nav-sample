using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M = WpfNavigationExample.Models;
using MU = WpfNavigationExample.Models.Users;
using L = WpfNavigationExample.Views.LayoutFrames;
using Prism.Mvvm;

namespace WpfNavigationExample.Views.Users.Editing
{
    public sealed class UserEditPage
        : BindableBase
        , L.ILayoutPage
    {
        public string PageTitle => "User";

        public User User { get; }

        public L.INavigateRequest SaveRequest { get; }

        public UserEditPage(MU.User user, M.Model model)
        {
            User = new User(user);

            SaveRequest = new L.NavigateRequest(() =>
            {
                model.UserRepository.Save(User.MakeReadOnly());
                return new Lists.UserListPage(model);
            });
        }
    }
}
