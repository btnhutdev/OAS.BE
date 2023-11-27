using Authen.API.Interfaces;
using AutoMapper;
using Core.ViewModel;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Authen.API.Repositories
{
    public class AuthenRepository : IAuthenRepository
    {
        #region constructor
        SQLServerDbContext _context;

        public AuthenRepository(SQLServerDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Login
        public async Task<bool> Login(LoginViewModel user, string type)
        {
            var result = await _context.Users.FromSqlRaw("EXEC sp_Login @username, @password, @type",
                new SqlParameter("@username", user.Username),
                new SqlParameter("@password", user.Password),
                new SqlParameter("@type", type))
                .ToListAsync().ConfigureAwait(false);

            return result.FirstOrDefault() != null;
        }
        #endregion

        #region GetUserByUsername
        public async Task<UserViewModel> GetUserByUsername(string username)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username).ConfigureAwait(false);

            if (user != null)
            {
                var entry = _context.Entry(user);
                entry.Reference(c => c.IdPermissionNavigation).Load();
            }

            UserViewModel userViewModel = new UserViewModel();
            userViewModel.PermissionName = user.IdPermissionNavigation.PermissionName;
            userViewModel.Username = user.Username;
            userViewModel.IdUser = user.IdUser;
            userViewModel.Password = user.Password;
            userViewModel.PhoneNumber = user.PhoneNumber;
            userViewModel.Email = user.Email;
            userViewModel.FirstName = user.FirstName;
            userViewModel.LastName = user.LastName;
            userViewModel.Gender = user.Gender;

            return userViewModel;
        }
        #endregion
    }
}
