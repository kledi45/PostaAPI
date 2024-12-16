using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostaAPI.DTOs.Users;
using PostaAPI.DTOs;
using PostaAPI.Interfaces;
using PostaAPI.DTOs.Countries;
using Microsoft.AspNetCore.Authorization;

namespace PostaAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesService _countriesService;
        public CountriesController(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry(CreateCountryDTO createCountry) => Ok(await _countriesService.CreateCountry(createCountry));
        [HttpGet]
        public async Task<IActionResult> GetCountries() => Ok(await _countriesService.GetCountries());
        [HttpPost]
        public async Task<IActionResult> EditCountry(EditCountryDTO editCountry) => Ok(await _countriesService.EditCountry(editCountry));
        [HttpPost]
        public async Task<IActionResult> DeleteCountry(DeleteDTO deleteDTO) => Ok(await _countriesService.DeleteCountry(deleteDTO));

    }
}
