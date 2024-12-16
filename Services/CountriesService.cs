using AutoMapper;
using Microsoft.Identity.Client;
using PostaAPI.Classes;
using PostaAPI.DTOs;
using PostaAPI.DTOs.Countries;
using PostaAPI.DTOs.Users;
using PostaAPI.Enums;
using PostaAPI.GenericRepository;
using PostaAPI.Helpers;
using PostaAPI.Interfaces;

namespace PostaAPI.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly IGenericRepository<Countries> _countriesRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly LoggedUserHelper _loggedUserHelper;

        public CountriesService
        (
            IGenericRepository<Countries> countriesrRepository,
            IMapper mapper, IWebHostEnvironment webHostEnvironment,
            LoggedUserHelper loggedUserHelper
        )
        {
            _countriesRepository = countriesrRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _loggedUserHelper = loggedUserHelper;
        }

        public async Task<ApiResponse<CreateCountryDTO>> CreateCountry(CreateCountryDTO createCountryDTO)
        {
            try
            {
                var country = new Countries();
                _mapper.Map(createCountryDTO, country);
                var loggedUser = _loggedUserHelper.GetUserLoggedIn();
                EntityHelper.SetEntryData(country, loggedUser.Id, loggedUser.UserName);
                var countryCreated = _countriesRepository.AddAndGetEntity(country);
                return new ApiResponse<CreateCountryDTO>((int)PublicResultStatusCodes.Done, _mapper.Map<CreateCountryDTO>(await _countriesRepository.GetByIdAsync(countryCreated.Id)));
            }
            catch (Exception)
            {
                return new ApiResponse<CreateCountryDTO>((int)PublicResultStatusCodes.InternalServerError);
            }
        }

        public async Task<ApiResponse<DeleteDTO>> DeleteCountry(DeleteDTO deleteDTO)
        {
            try
            {

                var country = await _countriesRepository.GetByIdAsync(deleteDTO.Id);
                if (country == null)
                    return new ApiResponse<DeleteDTO>((int)PublicResultStatusCodes.EntityNotFound);

                country.IsDeleted = true;
                var loggedUser = _loggedUserHelper.GetUserLoggedIn();
                EntityHelper.SetDeleteData(country, loggedUser.Id, loggedUser.UserName);
                _countriesRepository.Update(country);
                return new ApiResponse<DeleteDTO>((int)PublicResultStatusCodes.Done);

            }
            catch (Exception)
            {
                return new ApiResponse<DeleteDTO>((int)PublicResultStatusCodes.InternalServerError);
            }
        }

        public async Task<ApiResponse<EditCountryDTO>> EditCountry(EditCountryDTO editCountryDTO)
        {
            try
            {
                var country = await _countriesRepository.GetByIdAsync(editCountryDTO.Id);
                if (country == null)
                    return new ApiResponse<EditCountryDTO>((int)PublicResultStatusCodes.EntityNotFound);

                country.Name = editCountryDTO.Name;
                country.ShippingPrice = editCountryDTO.ShippingPrice;
                var loggedUser = _loggedUserHelper.GetUserLoggedIn();
                EntityHelper.SetUpdateData(country, loggedUser.Id, loggedUser.UserName);

                _countriesRepository.Update(country);

                return new ApiResponse<EditCountryDTO>((int)PublicResultStatusCodes.Done);
            }
            catch (Exception)
            {
                return new ApiResponse<EditCountryDTO>((int)PublicResultStatusCodes.InternalServerError);
            }
        }

        public async Task<ApiResponse<IEnumerable<CountryListDTO>>> GetCountries()
        {
            try
            {
                var countries =await _countriesRepository.FindByCriteriaAsync(_ => !_.IsDeleted);
                return new ApiResponse<IEnumerable<CountryListDTO>>((int)PublicResultStatusCodes.Done, _mapper.Map<IList<CountryListDTO>>(countries));
            }
            catch (Exception)
            {
                return new ApiResponse<IEnumerable<CountryListDTO>>((int)PublicResultStatusCodes.InternalServerError);
            }
        }
    }
}
