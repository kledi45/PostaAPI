using PostaAPI.DTOs.Users;
using PostaAPI.DTOs;

namespace PostaAPI.Interfaces
{
    public interface ILoginService
    {
        Task<ApiResponse<UserLoginDTO>> AuthenticateLogin(string usernameOrEmail, string password);
        ApiResponse<bool> SignOut(string token);
    }
}
