using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using PostaAPI.Classes;
using PostaAPI.DTOs;
using PostaAPI.DTOs.Users;
using PostaAPI.Enums;
using PostaAPI.GenericRepository;
using PostaAPI.Helpers;
using PostaAPI.Interfaces;

namespace PostaAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<Users> _usersRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly LoggedUserHelper _loggedUserHelper;

        public UserService(IGenericRepository<Users> usersRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment, LoggedUserHelper loggedUserHelper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _loggedUserHelper = loggedUserHelper;
        }
        public async Task<ApiResponse<CreateUserDTO>> CreateUser(CreateUserDTO createUserDTO)
        {
            try
            {
                var user = new Users();
                _mapper.Map(createUserDTO, user);

                if (!string.IsNullOrEmpty(createUserDTO.Password))
                {
                    user.Password = HashPassword(createUserDTO.Password);
                    user.SaltedPassword = user.Password;
                }

                if (createUserDTO.Document != null)
                {
                    var documentHelper = new UploadDocumentHelper(_webHostEnvironment, string.Concat(createUserDTO.FirstName, createUserDTO.LastName));
                    user.ProfileImagePath = await documentHelper.UploadDocumentAsync(createUserDTO.Document);
                }
                //var loggedUser = _loggedUserHelper.GetUserLoggedIn();
                //EntityHelper.SetEntryData(user,loggedUser.Id, loggedUser.UserName);
                var userCreated = _usersRepository.AddAndGetEntity(user);
                return new ApiResponse<CreateUserDTO>((int)PublicResultStatusCodes.Done, _mapper.Map<CreateUserDTO>(_usersRepository.GetById(userCreated.Id)));
            }
            catch (Exception)
            {
                return new ApiResponse<CreateUserDTO>((int)PublicResultStatusCodes.InternalServerError);
            }
        }

        public async Task<ApiResponse<EditUserDTO>> EditUser(EditUserDTO editUserDTO)
        {
            try
            {
                var user = _usersRepository.GetById(editUserDTO.Id);
                if (user == null)
                    return new ApiResponse<EditUserDTO>((int)PublicResultStatusCodes.EntityNotFound);

                if (editUserDTO.Document != null)
                {
                    if(!string.IsNullOrEmpty(user.ProfileImagePath))
                    {
                        var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, user.ProfileImagePath);
                        if (File.Exists(fullPath))
                            File.Delete(fullPath);
                    } 
                
                    var documentHelper = new UploadDocumentHelper(_webHostEnvironment, string.Concat(editUserDTO.FirstName, editUserDTO.LastName));
                    user.ProfileImagePath = await documentHelper.UploadDocumentAsync(editUserDTO.Document);
                }
                user.FirstName = editUserDTO.FirstName;
                user.IdRole = editUserDTO.IdRole;
                user.LastName = editUserDTO.LastName;
                user.UserName = editUserDTO.UserName;
                user.Email = editUserDTO.Email;
                user.Prefix = editUserDTO.Prefix;
                user.PhoneNumber = editUserDTO.PhoneNumber;
                user.IdCountry = editUserDTO.IdCountry;
                if (!string.IsNullOrEmpty(editUserDTO.Password))
                {
                    user.Password = HashPassword(editUserDTO.Password);
                    user.SaltedPassword = user.Password;
                }

                var loggedUser = _loggedUserHelper.GetUserLoggedIn();
                EntityHelper.SetUpdateData(user, loggedUser.Id, loggedUser.UserName);

                _usersRepository.Update(user);

                return new ApiResponse<EditUserDTO>((int)PublicResultStatusCodes.Done);
            }
            catch (Exception)
            {
                return new ApiResponse<EditUserDTO>((int)PublicResultStatusCodes.InternalServerError);
            }
        }

        public async Task<ApiResponse<IEnumerable<UsersListDTO>>> GetUsers()
        {
            try
            {
                var users = await _usersRepository.FindByCriteriaAsync(_ => !_.IsDeleted, new string[] { "IdRoleNavigation", "IdCountryNavigation" });
                return new ApiResponse<IEnumerable<UsersListDTO>>((int)PublicResultStatusCodes.Done, _mapper.Map<IList<UsersListDTO>>(users.OrderByDescending(_ => _.Id)));
            }
            catch (Exception)
            {
                return new ApiResponse<IEnumerable<UsersListDTO>>((int)PublicResultStatusCodes.InternalServerError);
            }
        }

        public async Task<ApiResponse<DeleteDTO>> DeleteUser(DeleteDTO deleteDTO)
        {
            try
            {
                var user = _usersRepository.GetById(deleteDTO.Id);
                if (user == null)
                    return new ApiResponse<DeleteDTO>((int)PublicResultStatusCodes.EntityNotFound);

                user.IsDeleted = true;
                var loggedUser = _loggedUserHelper.GetUserLoggedIn();
                EntityHelper.SetDeleteData(user, loggedUser.Id, loggedUser.UserName);
                _usersRepository.Update(user);
                return new ApiResponse<DeleteDTO>((int)PublicResultStatusCodes.Done);
            }
            catch (Exception)
            {
                return new ApiResponse<DeleteDTO>((int)PublicResultStatusCodes.InternalServerError);
            }
        }


        #region private user methods
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        #endregion
    }
}
