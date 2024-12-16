using PostaAPI.DTOs;
using PostaAPI.DTOs.Cities;
using PostaAPI.DTOs.Shipments;
using PostaAPI.DTOs.Users;

namespace PostaAPI.Interfaces
{
    public interface IShipmentsService
    {
        Task<ApiResponse<IEnumerable<UsersListDTO>>> GetClients();
        Task<ApiResponse<IEnumerable<CitiesListDTO>>> GetCitiesByCountry(int idCountry);
        Task<ApiResponse<CreateShipmentDTO>> CreateShipment(CreateShipmentDTO createShipment);
        Task<ApiResponse<IEnumerable<ShipmentsListDTO>>> GetShipments();
    }
}
