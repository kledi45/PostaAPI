using PostaAPI.DTOs.Countries;
using PostaAPI.DTOs;
using PostaAPI.DTOs.Cities;

namespace PostaAPI.Interfaces
{
    public interface ICitiesService
    {
        Task<ApiResponse<CreateCityDTO>> CreateCity(CreateCityDTO createCityDTO);
        Task<ApiResponse<EditCityDTO>> EditCity(EditCityDTO editCityDTO);
        Task<ApiResponse<IEnumerable<CitiesListDTO>>> GetCities();
        Task<ApiResponse<DeleteDTO>> DeleteCity(DeleteDTO deleteDTO);
    }
}
