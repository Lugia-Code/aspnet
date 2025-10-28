namespace TrackingCodeApi.dtos.usuario;

using AutoMapper;
using TrackingCodeApi.models;



public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        // Map model → DTO
        CreateMap<Usuario, UsuarioReadDto>();

        // Map DTO → model
        CreateMap<UsuarioCreateDto, Usuario>();
        CreateMap<UsuarioUpdateDto, Usuario>();
    }
}
