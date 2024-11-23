using AutoMapper;
using PostaAPI.Classes;
using PostaAPI.DTOs.Users;

namespace PostaAPI.Profiles
{
    public class AutoMapping : Profile
    {
        private readonly string _apiUrl = "https://localhost:7005/";
        public AutoMapping()
        {
            #region users profile mappers
            CreateMap<CreateUserDTO, Users>().ReverseMap();
            CreateMap<EditUserDTO, Users>().ReverseMap();
            CreateMap<UsersListDTO, Users>().ReverseMap()
               .ForMember(x => x.ProfileImagePath, opt => opt.MapFrom(src => _apiUrl + src.ProfileImagePath))
               .ForMember(x => x.Role, opt => opt.MapFrom(src => src.IdRoleNavigation.Title));

            #endregion

            //#region roles profile mappers
            //CreateMap<CreateRolesDTO, Roles>().ReverseMap();
            //CreateMap<EditRolesDTO, Roles>().ReverseMap();
            //CreateMap<RolesListDTO, Roles>().ReverseMap();
            //#endregion

    //        #region client requests
    //        CreateMap<CreateClientRequestDTO, ClientRequests>().ReverseMap();
    //        CreateMap<EditClientRequestDTO, ClientRequests>().ReverseMap();
    //        CreateMap<ClientRequestsListDTO, ClientRequests>().ReverseMap()
    //        .ForMember(dest => dest.UserDelegatedTo, opt => opt.MapFrom(src => src.ClientRequestDelegations != null
    //                ? string.Join(", ", src.ClientRequestDelegations.Select(d => d.IdUserDelegatedToNavigation.FirstName + " " + d.IdUserDelegatedToNavigation.LastName)) 
    //                : string.Empty))
    //         .ForMember(dest => dest.IdUserDelegatedTo, opt => opt.MapFrom(src => src.ClientRequestDelegations != null ? 
    //         src.ClientRequestDelegations.Select(d => d.UserId).ToList() : new List<int>()))
    //        .ForMember(dest => dest.DateOfReceipt, opt => opt.MapFrom(src => src.EntryDate))
    //        .ForMember(x => x.HasDamageRecordings, opt => opt.MapFrom(src => src.DamageRecordings.Any(x => !x.IsDeleted)));
    //        #endregion

    //        #region isolation drying 
    //        CreateMap<CreateIsolationDryingDTO, IsolationDrying>().ReverseMap();
    //        CreateMap<EditIsolationDryingDTO, IsolationDrying>().ReverseMap();
    //        CreateMap<IsolationDryingListDTO, IsolationDrying>().ReverseMap();
    //        #endregion
    //        #region damage recordings
    //        CreateMap<CreateDamageRecordingDTO, DamageRecordings>().ReverseMap();
    //        CreateMap<EditDamageRecordingDTO, DamageRecordings>().ReverseMap();
    //        CreateMap<DamageRecordingsListDTO, DamageRecordings>().ReverseMap()
    //            .ForMember(x => x.User, opt => opt.MapFrom(x => x.IdUserNavigation.FirstName + " " + x.IdUserNavigation.LastName))
    //            .ForMember(x => x.LGKey, opt => opt.MapFrom(x => x.IdLGKeyNavigation.Name))
    //       .ForMember(x => x.Devices, opt => opt.MapFrom(x =>
    //string.Join(", ", x.DamageRecordingsDevices
    //    .Where(d => d.IdDeviceNavigation != null) // Ensure IdDeviceNavigation is not null
    //    .Select(d => d.IdDeviceNavigation.Name)))) 
    //            .ForMember(x => x.IdDevices, opt => opt.MapFrom(x => x.DamageRecordingsDevices.Select(x => x.IDDevice)));
    //        #endregion
        }
    }
}
