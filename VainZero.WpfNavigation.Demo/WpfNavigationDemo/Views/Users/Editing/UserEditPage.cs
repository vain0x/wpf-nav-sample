using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M = VainZero.WpfNavigationDemo.Models;
using MU = VainZero.WpfNavigationDemo.Models.Users;
using L = VainZero.WpfNavigationDemo.Views.LayoutFrames;
using Prism.Mvvm;

namespace VainZero.WpfNavigationDemo.Views.Users.Editing
{
    public sealed class UserEditPage
        : BindableBase
        , L.ILayoutPage
    {
        public string PageTitle => "User";

        public User User { get; }

        public L.INavigationRequest SaveRequest { get; }

        public UserEditPage(MU.User user, M.Model model)
        {
            User = new User(user);

            SaveRequest = new L.NavigationRequest(() =>
            {
                model.UserRepository.Save(User.MakeReadOnly());
                return new Lists.UserListPage(model);
            });
        }
    }
}
