using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetKit.Functional.Commands;
using VainZero.WpfNavigation.Navigation;
using Reactive.Bindings;

namespace VainZero.WpfNavigation.Demo.Users
{
    public sealed class UserListPage
        : Navigatable
    {
        public override string Title => "Users";

        public ICommand<User> GoDetailCommand { get; }

        public ICommand<User> SaveCommand { get; }

        public IEnumerable<User> Items =>
            Users.Values;

        Dictionary<string, User> Users { get; }

        void GoDetail(User user)
        {
            NavigateCommand.Execute(new UserDetailPage(Users, user));
        }

        public UserListPage(Dictionary<string, User> users)
        {
            Users = users;
            GoDetailCommand = CommandModule.Create<User>(GoDetail);
        }

        public static User[] SampleUsers { get; } =
            Enumerable.Range(0, 200).Select(i => new User($"User {i}", i.ToString(), DateTime.Now)).ToArray();

        public UserListPage()
            : this(SampleUsers.ToDictionary(user => user.UserId))
        {
        }
    }
}
