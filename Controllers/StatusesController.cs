using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostaAPI.DTOs.Roles;
using PostaAPI.DTOs;
using PostaAPI.Interfaces;
using PostaAPI.DTOs.Statuses;

namespace PostaAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class StatusesController : ControllerBase
    {
        private readonly IStatusesService _statusesService;
        public StatusesController(IStatusesService statusesService)
        {
            _statusesService = statusesService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatus(CreateStatusDTO createStatusDTO) => Ok(await _statusesService.CreateStatus(createStatusDTO));
        [HttpGet]
        public async Task<IActionResult> GetStatuses() => Ok(await _statusesService.GetStatuses());
        [HttpPost]
        public async Task<IActionResult> EditStatus(EditStatusDTO editStatusDTO) => Ok(await _statusesService.EditStatus(editStatusDTO));
        [HttpPost]
        public async Task<IActionResult> DeleteStatus(DeleteDTO deleteDTO) => Ok(await _statusesService.DeleteStatus(deleteDTO));
    }
}
