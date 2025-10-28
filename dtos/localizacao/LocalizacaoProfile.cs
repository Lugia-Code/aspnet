using TrackingCodeApi.models;
using AutoMapper;

namespace TrackingCodeApi.dtos.localizacao;

public class LocalizacaoProfile: Profile
{
    public LocalizacaoProfile()
    {
        CreateMap<Localizacao, LocalizacaoDto>();
        CreateMap<LocalizacaoDto, Localizacao>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null));
    }
    
}