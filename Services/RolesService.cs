using AutoMapper;
using PostaAPI.Classes;
using PostaAPI.DTOs;
using PostaAPI.DTOs.Countries;
using PostaAPI.DTOs.Roles;
using PostaAPI.Enums;
using PostaAPI.GenericRepository;
using PostaAPI.Helpers;
using PostaAPI.Interfaces;

namespace PostaAPI.Services
{
    public class RolesService : IRolesService
    {
        private readonly IGenericRepository<Roles> _rolesRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly LoggedUserHelper _loggedUserHelper;

        public RolesService
        (
            IGenericRepository<Roles> rolesRepository, 
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment, 
            LoggedUserHelper loggedUserHelper
        )
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _loggedUserHelper = loggedUserHelper;
        }

        public async Task<ApiResponse<CreateRoleDTO>> CreateRole(CreateRoleDTO createRoleDTO)
        {
            try
            {
                var role = new Roles();
                _mapper.Map(createRoleDTO, role);
                var loggedUser = _loggedUserHelper.GetUserLoggedIn();
                EntityHelper.SetEntryData(role, loggedUser.Id, loggedUser.UserName);
                var roleCreated = _rolesRepository.AddAndGetEntity(role);
                return new ApiResponse<CreateRoleDTO>((int)PublicResultStatusCodes.Done, _mapper.Map<CreateRoleDTO>(await _rolesRepository.GetByIdAsync(roleCreated.Id)));
            }
            catch (Exception)
            {
                return new ApiResponse<CreateRoleDTO>((int)PublicResultStatusCodes.InternalServerError);
            }
        }

        public async Task<ApiResponse<DeleteDTO>> DeleteRole(DeleteDTO deleteDTO)
        {
            try
            {
                var role = await _rolesRepository.GetByIdAsync(deleteDTO.Id);
                if (role == null)
                    return new ApiResponse<DeleteDTO>((int)PublicResultStatusCodes.EntityNotFound);

                role.IsDeleted = true;
                var loggedUser = _loggedUserHelper.GetUserLoggedIn();
                EntityHelper.SetDeleteData(role, loggedUser.Id, loggedUser.UserName);
                _rolesRepository.Update(role);
                return new ApiResponse<DeleteDTO>((int)PublicResultStatusCodes.Done);
            }
            catch (Exception)
            {
                return new ApiResponse<DeleteDTO>((int)PublicResultStatusCodes.InternalServerError);
            }
        }

        public async Task<ApiResponse<EditRoleDTO>> EditRole(EditRoleDTO editRoleDTO)
        {
            try
            {
                var role = await _rolesRepository.GetByIdAsync(editRoleDTO.Id);
                if (role == null)
                    return new ApiResponse<EditRoleDTO>((int)PublicResultStatusCodes.EntityNotFound);

                role.Title = editRoleDTO.Title;
                var loggedUser = _loggedUserHelper.GetUserLoggedIn();
                EntityHelper.SetUpdateData(role, loggedUser.Id, loggedUser.UserName);

                _rolesRepository.Update(role);

                return new ApiResponse<EditRoleDTO>((int)PublicResultStatusCodes.Done);
            }
            catch (Exception)
            {
                return new ApiResponse<EditRoleDTO>((int)PublicResultStatusCodes.InternalServerError);
            }
        }

        public async Task<ApiResponse<IEnumerable<RolesListDTO>>> GetRoles()
        {
            try
            {
                var roles = await _rolesRepository.FindByCriteriaAsync(_ => !_.IsDeleted);
                return new ApiResponse<IEnumerable<RolesListDTO>>((int)PublicResultStatusCodes.Done, _mapper.Map<IList<RolesListDTO>>(roles));
            }
            catch (Exception)
            {

                return new ApiResponse<IEnumerable<RolesListDTO>>((int)PublicResultStatusCodes.InternalServerError);
            }
        }
    }
}
