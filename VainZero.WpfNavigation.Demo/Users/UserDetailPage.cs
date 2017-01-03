using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetKit.Functional.Commands;
using DotNetKit.Misc;
using VainZero.WpfNavigation.Navigation;
using VainZero.Functional.Commands;

namespace VainZero.WpfNavigation.Demo.Users
{
    public sealed class UserDetailPage
        : Navigatable
    {
        public override string Title => "User";

        public UserUpdatable User { get; }

        Dictionary<string, User> Users { get; }

        public ICommand<Unit> SaveCommand { get; }

        void Save()
        {
            Users[User.UserId] = User.MakeReadOnly();
            NavigateCommand.Execute(new UserListPage(Users));
        }

        public UserDetailPage(Dictionary<string, User> users, User user)
        {
            Users = users;
            User = new UserUpdatable(user);
            SaveCommand = new UnitCommand(Save);
        }
    }
}
