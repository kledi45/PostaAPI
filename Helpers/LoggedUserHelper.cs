using Microsoft.IdentityModel.Tokens;
using PostaAPI.DTOs.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PostaAPI.Helpers
{
    public class LoggedUserHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public LoggedUserHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public UserLoginDTO? GetUserLoggedIn()
        {
            var httpContext = _contextAccessor?.HttpContext;
            if (httpContext == null || httpContext.Session == null)
                return null;
            var authorizationHeader = httpContext.Request.Headers["Authorization"].ToString();
            var token = "";
            if (authorizationHeader.StartsWith("Bearer "))
            {
                 token = authorizationHeader.Substring("Bearer ".Length).Trim();

              
            }
           
            if (string.IsNullOrEmpty(token))
                return null;

            var key = Encoding.UTF8.GetBytes("aqsJK5Sud1LQnt+8Rdw/DC4bQxvzjOLcnecqUJlydLs=");

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                var claims = principal.Claims.ToList();
                var userId = claims.FirstOrDefault(c => c.Type == "Id")?.Value;
                var userName = claims.FirstOrDefault(c => c.Type == "Username")?.Value;
                var email = claims.FirstOrDefault(c => c.Type == "Email")?.Value;
                var idRole = claims.FirstOrDefault(c => c.Type == "IdRole")?.Value;
                var userLoginDto = new UserLoginDTO
                {
                    Id = int.Parse(userId),
                    UserName = userName,
                    Email = email,
                    Token = token,
                    IdRole = int.Parse(idRole),
                };

                return userLoginDto;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

}
