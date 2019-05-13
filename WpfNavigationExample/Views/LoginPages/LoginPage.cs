using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetKit.Misc;
using Prism.Mvvm;
using WpfNavigationExample.Reactive.Commands;
using M = WpfNavigationExample.Models;
using MU = WpfNavigationExample.Models.Users;
using L = WpfNavigationExample.Views.LayoutFrames;

namespace WpfNavigationExample.Views.LoginPages
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
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password = "password";
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public L.INavigateRequest LoginRequest { get; }

        public LoginPage(M.Model model)
        {
            _authenticator = new MU.UserAuthenticator(model.UserRepository);

            LoginRequest = new L.NavigateRequest(() =>
            {
                if (!_authenticator.Authenticate(Email, Password, out var authenticatedUser))
                {
                    return null;
                }

                return new FrontPages.FrontPage(model);
            });
        }
    }
}
