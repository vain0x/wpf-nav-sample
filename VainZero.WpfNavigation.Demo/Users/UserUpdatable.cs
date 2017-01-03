using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;

namespace VainZero.WpfNavigation.Demo.Users
{
    public sealed class UserUpdatable
    {
        public string UserId { get; }
        public ReactiveProperty<string> Name { get; }
        public ReactiveProperty<DateTime> Birthday { get; }

        public User MakeReadOnly()
        {
            return new User(Name.Value, UserId, Birthday.Value);
        }

        public UserUpdatable(User user)
        {
            UserId = user.UserId;
            Name = new ReactiveProperty<string>(user.Name);
            Birthday = new ReactiveProperty<DateTime>(user.Birthday);
        }
    }
}
