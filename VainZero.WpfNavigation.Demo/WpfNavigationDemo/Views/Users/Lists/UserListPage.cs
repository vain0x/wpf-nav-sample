using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using M = VainZero.WpfNavigationDemo.Models;
using MU = VainZero.WpfNavigationDemo.Models.Users;
using L = VainZero.WpfNavigationDemo.Views.LayoutFrames;

namespace VainZero.WpfNavigationDemo.Views.Users.Lists
{
    public sealed class UserListPage
        : BindableBase
        , L.ILayoutPage
    {
        public string PageTitle => "Users";

        public UserListItem[] Users { get; }

        public L.INavigateRequestFactory<MU.User> EditRequestFactory { get; }

        public UserListPage(M.Model model)
        {
            Users =
                model.UserRepository.FindAll()
                .Select(user => new UserListItem(user))
                .ToArray();

            EditRequestFactory =
                new L.AnonymousNavigateFactory<MU.User>(user =>
                    new L.NavigationRequest(() =>
                        new Editing.UserEditPage(user, model)
                    ));
        }
    }

    public sealed class UserListItem
    {
        public MU.User User { get; }

        public UserListItem(MU.User user)
        {
            User = user;
        }
    }
}
