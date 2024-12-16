using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostaAPI.DTOs.Countries;
using PostaAPI.DTOs;
using PostaAPI.Interfaces;
using PostaAPI.DTOs.Cities;

namespace PostaAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesService _citiesService;
        public CitiesController(ICitiesService citiesService)
        {
            _citiesService = citiesService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity(CreateCityDTO createCity) => Ok(await _citiesService.CreateCity(createCity));
        [HttpGet]
        public async Task<IActionResult> GetCities() => Ok(await _citiesService.GetCities());
        [HttpPost]
        public async Task<IActionResult> EditCity(EditCityDTO editCity) => Ok(await _citiesService.EditCity(editCity));
        [HttpPost]
        public async Task<IActionResult> DeleteCity(DeleteDTO deleteDTO) => Ok(await _citiesService.DeleteCity(deleteDTO));
    }
}
