using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using PostaAPI.Classes;
using PostaAPI.DTOs.Users;
using PostaAPI.DTOs;
using PostaAPI.Enums;
using PostaAPI.GenericRepository;
using PostaAPI.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PostaAPI.Services
{

    [AllowAnonymous]
    public class LoginService : ILoginService
    {
        private readonly IGenericRepository<Users> _userRepository;
        private readonly IGenericRepository<UserJwtToken> _userJwtTokenRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginService(IGenericRepository<Users> userRepository, IGenericRepository<UserJwtToken> userJwtTokenRepository, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _userJwtTokenRepository = userJwtTokenRepository;
        }

        public async Task<ApiResponse<UserLoginDTO>> AuthenticateLogin(string usernameOrEmail, string password)
        {
            try
            {
                var user = (await _userRepository.FindByCriteriaAsync(_ => _.UserName.ToLower() == usernameOrEmail.ToLower() || _.Email.ToLower() == usernameOrEmail.ToLower())).FirstOrDefault();
                if (user == null)
                    return new ApiResponse<UserLoginDTO>((int)PublicResultStatusCodes.UserDoesNotExits);

                if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                    return new ApiResponse<UserLoginDTO>((int)PublicResultStatusCodes.PasswordNotCorrect);

                var token = await GenerateJwtToken(user);

                var userDto = new UserLoginDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = token
                };

                DeleteExistingTokens(user.Id);

                var userJWTToken = new UserJwtToken()
                {
                    IdUser = user.Id,
                    Token = token,
                    ExpiryDate = DateTime.Now.AddHours(3),
                };

                _userJwtTokenRepository.Add(userJWTToken);
                if (_httpContextAccessor?.HttpContext?.Session != null)
                    _httpContextAccessor.HttpContext.Session.SetString("UserToken", token);
                var token2 = _httpContextAccessor.HttpContext.Session.GetString("UserToken");
                return new ApiResponse<UserLoginDTO>((int)PublicResultStatusCodes.Done, userDto);

            }
            catch (Exception)
            {
                return new ApiResponse<UserLoginDTO>((int)PublicResultStatusCodes.InternalServerError);
            }
        }

        private void DeleteExistingTokens(int idUser)
        {
            try
            {
                var existingTokens = _userJwtTokenRepository.FindByCriteria(x => x.IdUser == idUser);
                foreach (var item in existingTokens)
                {
                    item.IsDeleted = true;
                    _userJwtTokenRepository.Update(item);
                }
            }
            catch (Exception)
            {

            }
        }
        public ApiResponse<bool> SignOut(string token)
        {
            try
            {
                var userJWT = _userJwtTokenRepository.FindByCriteria(x => x.Token == token).FirstOrDefault();
                if (userJWT == null)
                    return new ApiResponse<bool>((int)PublicResultStatusCodes.UserDoesNotExits);
                userJWT.IsDeleted = true;
                _userJwtTokenRepository.Update(userJWT);
                return new ApiResponse<bool>((int)PublicResultStatusCodes.Done);
            }
            catch (Exception)
            {

                return new ApiResponse<bool>((int)PublicResultStatusCodes.InternalServerError);

            }
        }

        private async Task<string> GenerateJwtToken(Users user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aqsJK5Sud1LQnt+8Rdw/DC4bQxvzjOLcnecqUJlydLs="));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Username", user.UserName),
                new Claim("Email", user.Email),
                new Claim("Id", user.Id.ToString()),
                new Claim("IdRole", user.IdRole.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
