using AutoMapper;
using PostaAPI.Classes;
using PostaAPI.DTOs;
using PostaAPI.DTOs.Roles;
using PostaAPI.DTOs.Statuses;
using PostaAPI.Enums;
using PostaAPI.GenericRepository;
using PostaAPI.Helpers;
using PostaAPI.Interfaces;

namespace PostaAPI.Services
{
    public class StatusesService : IStatusesService
    {
        private readonly IGenericRepository<Statuses> _statusesRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly LoggedUserHelper _loggedUserHelper;

        public StatusesService
        (
            IGenericRepository<Statuses> statusesRepository,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            LoggedUserHelper loggedUserHelper
        )
        {
            _statusesRepository = statusesRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _loggedUserHelper = loggedUserHelper;
        }
        public async Task<ApiResponse<CreateStatusDTO>> CreateStatus(CreateStatusDTO createStatusDTO)
        {
            try
            {
                var status = new Statuses();
                _mapper.Map(createStatusDTO, status);
                var loggedUser = _loggedUserHelper.GetUserLoggedIn();
                EntityHelper.SetEntryData(status, loggedUser.Id, loggedUser.UserName);
                var roleCreated = _statusesRepository.AddAndGetEntity(status);
                return new ApiResponse<CreateStatusDTO>((int)PublicResultStatusCodes.Done, _mapper.Map<CreateStatusDTO>(await _statusesRepository.GetByIdAsync(roleCreated.Id)));
            }
            catch (Exception)
            {
                return new ApiResponse<CreateStatusDTO>((int)PublicResultStatusCodes.InternalServerError);
            }
        }

        public async Task<ApiResponse<DeleteDTO>> DeleteStatus(DeleteDTO deleteDTO)
        {
            try
            {
                var status = await _statusesRepository.GetByIdAsync(deleteDTO.Id);
                if (status == null)
                    return new ApiResponse<DeleteDTO>((int)PublicResultStatusCodes.EntityNotFound);

                status.IsDeleted = true;
                var loggedUser = _loggedUserHelper.GetUserLoggedIn();
                EntityHelper.SetDeleteData(status, loggedUser.Id, loggedUser.UserName);
                _statusesRepository.Update(status);
                return new ApiResponse<DeleteDTO>((int)PublicResultStatusCodes.Done);
            }
            catch (Exception)
            {
                return new ApiResponse<DeleteDTO>((int)PublicResultStatusCodes.InternalServerError);
            }
        }

        public async Task<ApiResponse<EditStatusDTO>> EditStatus(EditStatusDTO editStatusDTO)
        {
            try
            {
                var status = await _statusesRepository.GetByIdAsync(editStatusDTO.Id);
                if (status == null)
                    return new ApiResponse<EditStatusDTO>((int)PublicResultStatusCodes.EntityNotFound);

                status.Name = status.Name;
                var loggedUser = _loggedUserHelper.GetUserLoggedIn();
                EntityHelper.SetUpdateData(status, loggedUser.Id, loggedUser.UserName);

                _statusesRepository.Update(status);

                return new ApiResponse<EditStatusDTO>((int)PublicResultStatusCodes.Done);
            }
            catch (Exception)
            {
                return new ApiResponse<EditStatusDTO>((int)PublicResultStatusCodes.InternalServerError);
            }
        }

        public async Task<ApiResponse<IEnumerable<StatusesListDTO>>> GetStatuses()
        {
            try
            {
                var statuses = await _statusesRepository.FindByCriteriaAsync(_ => !_.IsDeleted);
                return new ApiResponse<IEnumerable<StatusesListDTO>>((int)PublicResultStatusCodes.Done, _mapper.Map<IList<StatusesListDTO>>(statuses));
            }
            catch (Exception)
            {

                return new ApiResponse<IEnumerable<StatusesListDTO>>((int)PublicResultStatusCodes.InternalServerError);
            }
        }
    }
}
