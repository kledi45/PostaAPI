using AutoMapper;
using PostaAPI.Classes;
using PostaAPI.DTOs.Cities;
using PostaAPI.DTOs.Countries;
using PostaAPI.DTOs.Roles;
using PostaAPI.DTOs.Shipments;
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
               .ForMember(x => x.Role, opt => opt.MapFrom(src => src.IdRoleNavigation.Title))
               .ForMember(x => x.Country, opt => opt.MapFrom(src => src.IdCountryNavigation.Name));

            #endregion

            #region countries profile mappers
            CreateMap<CreateCountryDTO, Countries>().ReverseMap();
            CreateMap<EditCountryDTO, Countries>().ReverseMap();
            CreateMap<CountryListDTO, Countries>().ReverseMap();
            #endregion
            #region roles profile mappers
            CreateMap<CreateRoleDTO, Roles>().ReverseMap();
            CreateMap<EditRoleDTO, Roles>().ReverseMap();
            CreateMap<RolesListDTO, Roles>().ReverseMap();
            #endregion

            #region cities profile mappers 
            CreateMap<CreateCityDTO, Cities>().ReverseMap();
            CreateMap<EditCityDTO, Cities>().ReverseMap();
            CreateMap<CitiesListDTO, Cities>().ReverseMap()
                .ForMember(x => x.Country, opt => opt.MapFrom(x => x.IdCountryNavigation.Name));

            #endregion

            #region shipments profile mapper
            CreateMap<CreateShipmentDTO, Shipments>().ReverseMap();
            CreateMap<EditShipmentDTO, Shipments>().ReverseMap();
            CreateMap<ShipmentsListDTO, Shipments>().ReverseMap()
                .ForMember(x => x.City, opt => opt.MapFrom(x => x.IdCityNavigation.Name))
                .ForMember(x => x.Country, opt => opt.MapFrom(x => x.IdCountryNavigation.Name))
                .ForMember(x => x.Client, opt => opt.MapFrom(x => x.IdUserNavigation.FirstName + " " + x.IdUserNavigation.LastName));
            #endregion
        }
    }
}
