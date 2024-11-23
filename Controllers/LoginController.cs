using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostaAPI.GenericRepository;
using PostaAPI.Interfaces;

namespace PostaAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string usernameOrEmail, string password) => Ok(await _loginService.AuthenticateLogin(usernameOrEmail,password));

        [HttpPost]
        public IActionResult SignOut(string token) => Ok(_loginService.SignOut(token));


    }
}
