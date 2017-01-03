using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VainZero.WpfNavigation.Demo.Users
{
    public sealed class User
    {
        public string Name { get; }
        public string UserId { get; }
        public DateTime Birthday { get; }

        public User(string name, string userId, DateTime birthday)
        {
            Name = name;
            UserId = userId;
            Birthday = birthday;
        }
    }
}
