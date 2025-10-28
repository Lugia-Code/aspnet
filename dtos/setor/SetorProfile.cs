namespace TrackingCodeApi.dtos.setor;

using AutoMapper;
using TrackingCodeApi.models;



public class SetorProfile : Profile
{
    public SetorProfile()
    {
        CreateMap<Setor, SetorDto>();
        CreateMap<SetorDto, Setor>();
    }
}
