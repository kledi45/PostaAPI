using PostaAPI.DTOs.Roles;
using PostaAPI.DTOs;
using PostaAPI.DTOs.Statuses;

namespace PostaAPI.Interfaces
{
    public interface IStatusesService
    {
        Task<ApiResponse<CreateStatusDTO>> CreateStatus(CreateStatusDTO createStatusDTO);
        Task<ApiResponse<EditStatusDTO>> EditStatus(EditStatusDTO editStatusDTO);
        Task<ApiResponse<IEnumerable<StatusesListDTO>>> GetStatuses();
        Task<ApiResponse<DeleteDTO>> DeleteStatus(DeleteDTO deleteDTO);
    }
}
