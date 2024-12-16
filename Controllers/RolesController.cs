using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostaAPI.DTOs.Users;
using PostaAPI.DTOs;
using PostaAPI.Interfaces;
using PostaAPI.DTOs.Countries;
using Microsoft.AspNetCore.Authorization;
using PostaAPI.DTOs.Roles;

namespace PostaAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;
        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDTO createRoleDTO) => Ok(await _rolesService.CreateRole(createRoleDTO));
        [HttpGet]
        public async Task<IActionResult> GetRoles() => Ok(await _rolesService.GetRoles());
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleDTO editRole) => Ok(await _rolesService.EditRole(editRole));
        [HttpPost]
        public async Task<IActionResult> DeleteRole(DeleteDTO deleteDTO) => Ok(await _rolesService.DeleteRole(deleteDTO));

    }
}
