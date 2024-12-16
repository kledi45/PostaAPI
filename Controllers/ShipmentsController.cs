using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostaAPI.DTOs.Shipments;
using PostaAPI.Interfaces;

namespace PostaAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ShipmentsController : ControllerBase
    {
        private IShipmentsService _shipmentsService;
        public ShipmentsController(IShipmentsService shipmentsService)
        {
            _shipmentsService = shipmentsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients() 
            => Ok(await _shipmentsService.GetClients());

        [HttpGet]
        public async Task<IActionResult> GetCitiesByCountry(int idCountry) 
            => Ok(await _shipmentsService.GetCitiesByCountry(idCountry));

        [HttpPost]
        public async Task<IActionResult> CreateShipment(CreateShipmentDTO shipmentDTO)
            => Ok(await _shipmentsService.CreateShipment(shipmentDTO));

        [HttpGet]
        public async Task<IActionResult> GetShipments()
            => Ok(await _shipmentsService.GetShipments());

    }
}
