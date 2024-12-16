using PostaAPI.DTOs.Users;
using PostaAPI.DTOs;
using PostaAPI.DTOs.Countries;

namespace PostaAPI.Interfaces
{
    public interface ICountriesService
    {
        Task<ApiResponse<CreateCountryDTO>> CreateCountry(CreateCountryDTO createUserDTO);
        Task<ApiResponse<EditCountryDTO>> EditCountry(EditCountryDTO editUserDTO);
        Task<ApiResponse<IEnumerable<CountryListDTO>>> GetCountries();
        Task<ApiResponse<DeleteDTO>> DeleteCountry(DeleteDTO deleteDTO);
    }
}
