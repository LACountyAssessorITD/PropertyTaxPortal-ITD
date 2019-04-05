using PropertyTaxPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyTaxPortal.Services
{
    public class AdminUserService : IUserService
    {
        private IDictionary<string, (string PasswordHash, User User)> _users = new Dictionary<string, (string PasswordHash, User User)>();
        

        public AdminUserService(IDictionary<string, string> users)
        {

            using (var context = new PTPContext())
            {
                try
                {
                    IDictionary<string, string> user_account = new Dictionary<string, string>();
                    user_account = context.Users.Where(b => b.Active == 1).ToList().ToDictionary(x => x.Username, x => x.Password);
                    foreach (var user in user_account)
                    {
                        _users.Add(user.Key.ToLower(), (BCrypt.Net.BCrypt.HashPassword(user.Value), new User(user.Key)));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("EXCEPTION----------------------------------------------------------------------------EXCEPTION" + e);
                    foreach (var user in users)
                    {
                        _users.Add(user.Key.ToLower(), (BCrypt.Net.BCrypt.HashPassword(user.Value), new User(user.Key)));
                    }
                }

            }


            
        }

        public Task<bool> ValidateCredentials(string username, string password, out User user)
        {
            user = null;
            var key = username.ToLower();
            if (_users.ContainsKey(key))
            {
                var hash = _users[key].PasswordHash;
                if (BCrypt.Net.BCrypt.Verify(password, hash))
                {
                    user = _users[key].User;
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }
    }
}
