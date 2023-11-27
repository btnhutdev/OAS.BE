using Core.ViewModel;

namespace Authen.API.Interfaces
{
    public interface IAuthenRepository
    {
        Task<bool> Login(LoginViewModel user, string type);
        Task<UserViewModel> GetUserByUsername(string username);
    }
}
