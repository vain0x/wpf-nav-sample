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
            new[]
            {
                new User("John Due", "john_due", new DateTime(1999, 12, 31)),
                new User("Vain Zero", "vain0", new DateTime(2000, 01, 01)),
            };

        public UserListPage()
            : this(SampleUsers.ToDictionary(user => user.UserId))
        {
        }
    }
}
