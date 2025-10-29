using AutoMapper;
using TrackingCodeApi.models;
using TrackingCodeApi.dtos.moto;

public class MotoProfile : Profile
{
    public MotoProfile()
    {

        CreateMap<Moto, MotoDto>()
            .ForMember(dest => dest.Setor, opt => opt.MapFrom(src => src.Setor.Nome))
            .ForMember(dest => dest.IdSetor, opt => opt.MapFrom(src => src.IdSetor))
            .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => src.DataCadastro))
            .ForMember(dest => dest.IdAudit, opt => opt.MapFrom(src => src.IdAudit));

      
        CreateMap<MotoDto, Moto>()
            .ForMember(dest => dest.Setor, opt => opt.Ignore());
    }
}
