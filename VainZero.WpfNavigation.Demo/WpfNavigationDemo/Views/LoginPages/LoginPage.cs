using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetKit.Misc;
using Prism.Mvvm;
using VainZero.Reactive.Commands;
using M = VainZero.WpfNavigationDemo.Models;
using MU = VainZero.WpfNavigationDemo.Models.Users;
using L = VainZero.WpfNavigationDemo.Views.LayoutFrames;

namespace VainZero.WpfNavigationDemo.Views.LoginPages
{
    public sealed class LoginPage
        : BindableBase
        , L.ILayoutPage
    {
        private readonly MU.UserAuthenticator _authenticator;

        public string PageTitle => "Login";

        private string _email = "u1@example.com";
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                SetProperty(ref _email, value);
            }
        }

        private string _password = "password";
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                SetProperty(ref _password, value);
            }
        }

        public L.INavigateRequest LoginRequest { get; }

        public LoginPage(M.Model model)
        {
            _authenticator = new MU.UserAuthenticator(model.UserRepository);

            LoginRequest = new L.NavigateRequest(() =>
            {
                MU.AuthenticatedUser authenticatedUser;
                if (_authenticator.Authenticate(Email, Password, out authenticatedUser))
                {
                    return new FrontPages.FrontPage(model);
                }
                else
                {
                    return null;
                }
            });
        }
    }
}
