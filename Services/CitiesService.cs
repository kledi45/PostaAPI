using AutoMapper;
using Microsoft.Identity.Client;
using PostaAPI.Classes;
using PostaAPI.DTOs;
using PostaAPI.DTOs.Cities;
using PostaAPI.DTOs.Roles;
using PostaAPI.Enums;
using PostaAPI.GenericRepository;
using PostaAPI.Helpers;
using PostaAPI.Interfaces;

namespace PostaAPI.Services
{
    public class CitiesService : ICitiesService
    {
        private readonly IGenericRepository<Cities> _citiesRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly LoggedUserHelper _loggedUserHelper;
        public CitiesService
        (
            IGenericRepository<Cities> citiesRepository,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            LoggedUserHelper loggedUserHelper
        )
        {
            _citiesRepository = citiesRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _loggedUserHelper = loggedUserHelper;
        }
        public async Task<ApiResponse<CreateCityDTO>> CreateCity(CreateCityDTO createCityDTO)
        {
            try
            {
                var city = new Cities()
                {
                    IDCountry = createCityDTO.IDCountry,
                    Name = createCityDTO.Name
                };
                var loggedUser = _loggedUserHelper.GetUserLoggedIn();
                EntityHelper.SetEntryData(city, loggedUser.Id, loggedUser.UserName);
                var cityCreated = _citiesRepository.AddAndGetEntity(city);
                return new ApiResponse<CreateCityDTO>((int)PublicResultStatusCodes.Done, _mapper.Map<CreateCityDTO>(await _citiesRepository.GetByIdAsync(cityCreated.Id)));
            }
            catch (Exception)
            {
                return new ApiResponse<CreateCityDTO>((int)PublicResultStatusCodes.InternalServerError);
            }
        }

        public async Task<ApiResponse<DeleteDTO>> DeleteCity(DeleteDTO deleteDTO)
        {
            try
            {
                var city = await _citiesRepository.GetByIdAsync(deleteDTO.Id);
                if (city == null)
                    return new ApiResponse<DeleteDTO>((int)PublicResultStatusCodes.EntityNotFound);

                city.IsDeleted = true;
                var loggedUser = _loggedUserHelper.GetUserLoggedIn();
                EntityHelper.SetDeleteData(city, loggedUser.Id, loggedUser.UserName);
                _citiesRepository.Update(city);
                return new ApiResponse<DeleteDTO>((int)PublicResultStatusCodes.Done);
            }
            catch (Exception)
            {
                return new ApiResponse<DeleteDTO>((int)PublicResultStatusCodes.InternalServerError);
            }
        }

        public async Task<ApiResponse<EditCityDTO>> EditCity(EditCityDTO editCityDTO)
        {
            try
            {
                var city = await _citiesRepository.GetByIdAsync(editCityDTO.Id);
                if (city == null)
                    return new ApiResponse<EditCityDTO>((int)PublicResultStatusCodes.EntityNotFound);

                city.Name = editCityDTO.Name;
                city.IDCountry = editCityDTO.IDCountry;
                var loggedUser = _loggedUserHelper.GetUserLoggedIn();
                EntityHelper.SetUpdateData(city, loggedUser.Id, loggedUser.UserName);

                _citiesRepository.Update(city);

                return new ApiResponse<EditCityDTO>((int)PublicResultStatusCodes.Done);
            }
            catch (Exception)
            {
                return new ApiResponse<EditCityDTO>((int)PublicResultStatusCodes.InternalServerError);
            }
        }

        public async Task<ApiResponse<IEnumerable<CitiesListDTO>>> GetCities()
        {
            try
            {
                var roles = await _citiesRepository.FindByCriteriaAsync(_ => !_.IsDeleted, new string[] {"IdCountryNavigation"});
                return new ApiResponse<IEnumerable<CitiesListDTO>>((int)PublicResultStatusCodes.Done, _mapper.Map<IList<CitiesListDTO>>(roles.OrderByDescending(x => x.Id)));
            }
            catch (Exception)
            {
                return new ApiResponse<IEnumerable<CitiesListDTO>>((int)PublicResultStatusCodes.InternalServerError);
            }
        }
    }
}
