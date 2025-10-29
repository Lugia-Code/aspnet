using AutoMapper;
using TrackingCodeApi.models;

namespace TrackingCodeApi.dtos.usuario;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<Usuario, UsuarioDto>();
        CreateMap<UsuarioDto, Usuario>();
    }
}