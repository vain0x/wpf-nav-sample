using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VainZero.WpfNavigationDemo.Models.Users;

namespace VainZero.WpfNavigationDemo.Models
{
    public sealed class Model
    {
        public UserRepository UserRepository { get; }

        public Model()
        {
            UserRepository = new UserRepository();
        }
    }
}
