using Microsoft.AspNetCore.Mvc;
using PostaAPI.DTOs;
using PostaAPI.DTOs.Users;
using PostaAPI.Interfaces;

namespace PostaAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO) => Ok(await _userService.CreateUser(createUserDTO));
        [HttpGet]
        public async Task<IActionResult> GetUsers() => Ok(await _userService.GetUsers());
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserDTO editUserDTO) => Ok(await _userService.EditUser(editUserDTO));
        [HttpPost]
        public async Task<IActionResult> DeleteUser(DeleteDTO deleteDTO) => Ok(await _userService.DeleteUser(deleteDTO));

    }
}