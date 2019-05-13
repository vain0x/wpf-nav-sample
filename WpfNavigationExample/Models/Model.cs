using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfNavigationExample.Models.Users;

namespace WpfNavigationExample.Models
{
    /// <summary>
    /// Application data and logic, irrelevant to views.
    /// </summary>
    public sealed class Model
    {
        public UserRepository UserRepository { get; }

        public Model()
        {
            UserRepository = new UserRepository();
        }
    }
}
