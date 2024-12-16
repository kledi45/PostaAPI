using AutoMapper;
using PostaAPI.Classes;
using PostaAPI.DTOs;
using PostaAPI.DTOs.Cities;
using PostaAPI.DTOs.Roles;
using PostaAPI.DTOs.Shipments;
using PostaAPI.DTOs.Users;
using PostaAPI.Enums;
using PostaAPI.GenericRepository;
using PostaAPI.Helpers;
using PostaAPI.Interfaces;

namespace PostaAPI.Services
{
    public class ShipmentsService : IShipmentsService
    {
        private readonly IGenericRepository<Users> _usersRepository;
        private readonly IGenericRepository<Cities> _citiesRepository;
        private readonly IGenericRepository<Shipments> _shipmentsRepository;

        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly LoggedUserHelper _loggedUserHelper;

        public ShipmentsService
        (
            IGenericRepository<Users> usersRepository,
            IGenericRepository<Cities> citiesRepository,
            IGenericRepository<Shipments> shipmentsRepository,
            IMapper mapper, 
            IWebHostEnvironment webHostEnvironment, 
            LoggedUserHelper loggedUserHelper
        )
        {
            _usersRepository = usersRepository;
            _citiesRepository = citiesRepository;   
            _shipmentsRepository = shipmentsRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _loggedUserHelper = loggedUserHelper;
        }

        public async Task<ApiResponse<IEnumerable<UsersListDTO>>> GetClients()
        {
            try
            {
                var data = await _usersRepository.FindByCriteriaAsync(_ => !_.IsDeleted && _.IdRole == (int)RolesEnum.Client);
                return new ApiResponse<IEnumerable<UsersListDTO>> ((int)PublicResultStatusCodes.Done, _mapper.Map<IList<UsersListDTO>>(data));
            }
            catch (Exception)
            {
                return new ApiResponse<IEnumerable<UsersListDTO>>((int)PublicResultStatusCodes.InternalServerError);
            }
        }

        public async Task<ApiResponse<IEnumerable<CitiesListDTO>>> GetCitiesByCountry(int idCountry)
        {
            try
            {
                var cities = await _citiesRepository.FindByCriteriaAsync(_ => !_.IsDeleted && _.IDCountry == idCountry);
                return new ApiResponse<IEnumerable<CitiesListDTO>>((int)PublicResultStatusCodes.Done, _mapper.Map<IList<CitiesListDTO>>(cities));
            }
            catch (Exception)
            {
                return new ApiResponse<IEnumerable<CitiesListDTO>>((int)PublicResultStatusCodes.InternalServerError);
            }
        }

        public async Task<ApiResponse<CreateShipmentDTO>> CreateShipment(CreateShipmentDTO createShipment)
        {
            try
            {
                var shipment = new Shipments();
                _mapper.Map(createShipment, shipment);
                shipment.UniqueIdentifier = GetUniqueIdentifier();
                shipment.IdStatus = (int)StatusesEnum.Registered;
                var loggedUser = _loggedUserHelper.GetUserLoggedIn();
                EntityHelper.SetEntryData(shipment, loggedUser.Id, loggedUser.UserName);
                var shipmentCreated = _shipmentsRepository.AddAndGetEntity(shipment);
                return new ApiResponse<CreateShipmentDTO>((int)PublicResultStatusCodes.Done, _mapper.Map<CreateShipmentDTO>(await _shipmentsRepository.GetByIdAsync(shipmentCreated.Id)));
            }
            catch (Exception)
            {
                return new ApiResponse<CreateShipmentDTO>((int)PublicResultStatusCodes.InternalServerError);
            }
        }

        public async Task<ApiResponse<IEnumerable<ShipmentsListDTO>>> GetShipments()
        {
            try
            {
                var data = await _shipmentsRepository.FindByCriteriaAsync(_ => !_.IsDeleted, new string[] { "IdCityNavigation", "IdCountryNavigation", "IdUserNavigation" });
                return new ApiResponse<IEnumerable<ShipmentsListDTO>>((int)PublicResultStatusCodes.Done, _mapper.Map<IList<ShipmentsListDTO>>(data));
            }
            catch (Exception)
            {
                return new ApiResponse<IEnumerable<ShipmentsListDTO>>((int)PublicResultStatusCodes.InternalServerError);
            }
        }

        private string GetUniqueIdentifier()
        {
            Random random = new Random();

            string letters = new string(Enumerable.Range(0, 5)
                .Select(_ => (char)('A' + random.Next(26)))
                .ToArray());

            string numbers = new string(Enumerable.Range(0, 4)
                .Select(_ => (char)('0' + random.Next(10)))
                .ToArray());

            return letters + numbers;
        }

    }
}
