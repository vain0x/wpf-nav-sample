using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using MU = VainZero.WpfNavigationDemo.Models.Users;

namespace VainZero.WpfNavigationDemo.Views.Users.Editing
{
    public sealed class User
        : BindableBase
    {
        private long _id;

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private DateTime _birthday;
        public DateTime Birthday
        {
            get => _birthday;
            set => SetProperty(ref _birthday, value);
        }

        public MU.User MakeReadOnly()
        {
            return new MU.User(_id, Name, Email, Birthday);
        }

        public User(MU.User user)
        {
            _id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Birthday = user.Birthday;
        }
    }
}
