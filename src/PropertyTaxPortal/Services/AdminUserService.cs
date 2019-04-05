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
        

      

        public Task<bool> ValidateCredentials(string username, string password, out User user)
        {
            user = null;
            var key = username.ToLower();
            IDictionary<string, (string PasswordHash, User User)> _users_1 = new Dictionary<string, (string PasswordHash, User User)>();
            using (var context = new PTPContext())
            {
               
                
                    IDictionary<string, string> user_account = new Dictionary<string, string>();
                    user_account = context.Users.Where(b => b.Active == 1).ToList().ToDictionary(x => x.Username, x => x.Password);
                    foreach (var user_1 in user_account)
                    {
                    _users_1.Add(user_1.Key.ToLower(), (BCrypt.Net.BCrypt.HashPassword(user_1.Value), new User(user_1.Key)));
                    }
               
             }

            if (_users_1.ContainsKey(key))
            {

                var hash = _users_1[key].PasswordHash;
                if (BCrypt.Net.BCrypt.Verify(password, hash))
                {
                    user = _users_1[key].User;
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }
    }
}
