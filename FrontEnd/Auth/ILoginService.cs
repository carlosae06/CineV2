using FrontEnd.DTOs;

namespace FrontEnd.Auth
{
    public interface ILoginService
    {
        Task Login(UserToken userToken);
        Task Logout();
        Task<bool> VerificarLogin();
    }
}
