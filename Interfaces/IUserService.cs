using PostaAPI.DTOs;
using PostaAPI.DTOs.Users;

namespace PostaAPI.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<CreateUserDTO>> CreateUser(CreateUserDTO createUserDTO);
        Task<ApiResponse<EditUserDTO>> EditUser(EditUserDTO editUserDTO);
        Task<ApiResponse<IEnumerable<UsersListDTO>>> GetUsers();
        Task<ApiResponse<DeleteDTO>> DeleteUser(DeleteDTO deleteDTO);
    }
}
