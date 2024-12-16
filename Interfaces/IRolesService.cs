using PostaAPI.DTOs.Countries;
using PostaAPI.DTOs;
using PostaAPI.DTOs.Roles;

namespace PostaAPI.Interfaces
{
    public interface IRolesService
    {
        Task<ApiResponse<CreateRoleDTO>> CreateRole(CreateRoleDTO createRoleDTO);
        Task<ApiResponse<EditRoleDTO>> EditRole(EditRoleDTO editRoleDTO);
        Task<ApiResponse<IEnumerable<RolesListDTO>>> GetRoles();
        Task<ApiResponse<DeleteDTO>> DeleteRole(DeleteDTO deleteDTO);
    }
}
