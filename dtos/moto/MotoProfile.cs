namespace TrackingCodeApi.dtos.moto;
using AutoMapper;
using TrackingCodeApi.models;



public class MotoProfile : Profile
{
    public MotoProfile()
    {
        CreateMap<Moto, MotoDto>();
        CreateMap<MotoDto, Moto>();
    }
}
