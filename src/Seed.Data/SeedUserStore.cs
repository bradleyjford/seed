using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Seed.Common;
using Seed.Security;

namespace Seed.Data
{
    public class SeedUserStore : 
        IUserLoginStore<User, int>,
        IUserClaimStore<User, int>,
        IUserRoleStore<User, int>,
        IUserPasswordStore<User, int>,
        IUserSecurityStampStore<User, int>
    {
        private readonly IUserRepository _userRepository;

        public SeedUserStore(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Dispose()
        {
        }

        public Task CreateAsync(User user)
        {
            return _userRepository.Add(user);
        }

        public Task UpdateAsync(User user)
        {
            return Task.FromResult(0);
        }

        public Task DeleteAsync(User user)
        {
            user.Deactivate();

            return TaskHelpers.ForVoidResult();
        }

        public Task<User> FindByIdAsync(int userId)
        {
            return _userRepository.Get(userId);
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return _userRepository.GetByUserName(userName);
        }

        public Task AddLoginAsync(User user, UserLoginInfo login)
        {
            user.AddLoginProvider(login.LoginProvider, login.ProviderKey);

            return TaskHelpers.ForVoidResult();
        }

        public Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            user.RemoveLoginProvider(login.LoginProvider, login.ProviderKey);

            return TaskHelpers.ForVoidResult();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            var loginProviders = (IList<UserLoginInfo>)user.LoginProviders
                .Select(l => new UserLoginInfo(l.Name, l.UserKey))
                .ToList();

            return Task.FromResult(loginProviders);
        }

        public Task<User> FindAsync(UserLoginInfo login)
        {
            return _userRepository.GetByLoginProvider(login.LoginProvider, login.ProviderKey);
        }

        public Task<IList<Claim>> GetClaimsAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task AddClaimAsync(User user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(User user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task SetSecurityStampAsync(User user, string stamp)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetSecurityStampAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
