using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNavigationExample.Models.Users
{
    public sealed class User
    {
        public long Id { get; }
        public string Name { get; }
        public string Email { get; }
        public DateTime Birthday { get; }

        public User(long id, string name, string email, DateTime birthday)
        {
            Id = id;
            Name = name;
            Email = email;
            Birthday = birthday;
        }
    }

    public sealed class AuthenticatedUser
    {
        public User User { get; }
        public DateTime AuthenticatedAt { get; }

        public AuthenticatedUser(User user, DateTime authenticatedAt)
        {
            User = user;
            AuthenticatedAt = authenticatedAt;
        }
    }

    public sealed class UserAuthenticator
    {
        private readonly UserRepository _repository;

        public bool Authenticate(string email, string password, out AuthenticatedUser authenticatedUser)
        {
            if (password != "password")
            {
                authenticatedUser = null;
                return false;
            }

            var user = _repository.FindByEmail(email);
            authenticatedUser = new AuthenticatedUser(user, DateTime.Now);
            return true;
        }

        public UserAuthenticator(UserRepository repository)
        {
            _repository = repository;
        }
    }

    public sealed class UserRepository
    {
        private readonly Dictionary<long, User> _items;

        private static Dictionary<long, User> GenerateSampleUsers()
        {
            return
                Enumerable.Range(1, 200)
                .Select(id =>
                    new User(
                        id,
                        name: $"User {id}",
                        email: $"u{id}@example.com",
                        birthday: new DateTime(2007, 8, 31).AddDays(id)
                    ))
                .ToDictionary(user => user.Id);
        }

        public IEnumerable<User> FindAll()
        {
            return _items.Values.OrderBy(user => user.Id);
        }

        public User FindById(long id)
        {
            return _items[id];
        }

        public User FindByEmail(string email)
        {
            return _items.Values.First(user => user.Email == email);
        }

        public void Save(User user)
        {
            _items[user.Id] = user;
        }

        public UserRepository()
        {
            _items = GenerateSampleUsers();
        }
    }
}
