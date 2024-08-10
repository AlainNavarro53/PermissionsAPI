using AutoMapper;
using PermissionsAPI.DTOs;
using PermissionsAPI.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Permission, PermissionDto>()
            .ForMember(dest => dest.TipoPermisoDescripcion, opt => opt.MapFrom(src => src.TipoPermiso.Description));

        CreateMap<TipoPermiso, PermissionTypeDto>();
        CreateMap<PermissionDto, Permission>();
        CreateMap<PermissionTypeDto, TipoPermiso>();
        CreateMap<ModifyPermissionDto, Permission>();
        CreateMap<RequestPermissionDto, Permission>();

        CreateMap<TipoPermiso, TipoPermisoDto>();
        CreateMap<CreateTipoPermisoCommand, TipoPermiso>();
        CreateMap<UpdateTipoPermisoDto, TipoPermiso>();
        CreateMap<RequestTipoPermisoDto, TipoPermiso>();
    }
}