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

        public UserListItem[] Items { get; }

        private static L.INavigationRequest CreateEditRequest(MU.User user, M.Model model)
        {
            return new L.NavigationRequest(() => new Editing.UserEditPage(user, model));
        }

        public UserListPage(M.Model model)
        {
            Items =
                model.UserRepository.FindAll()
                .Select(user => new UserListItem(user, CreateEditRequest(user, model)))
                .ToArray();
        }
    }

    public sealed class UserListItem
    {
        public MU.User User { get; }
        public L.INavigationRequest EditRequest { get; }

        public UserListItem(MU.User user, L.INavigationRequest editRequest)
        {
            User = user;
            EditRequest = editRequest;
        }
    }
}
