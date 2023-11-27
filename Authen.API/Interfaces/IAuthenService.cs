using Core.ViewModel;

namespace Authen.API.Interfaces
{
    public interface IAuthenService
    {
        Task<string> Authen(LoginViewModel user, string type);
    }
}
