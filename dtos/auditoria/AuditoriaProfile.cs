using AutoMapper;
using TrackingCodeApi.models;

namespace TrackingCodeApi.dtos.auditoria;


public class AuditoriaProfile : Profile
{
    public AuditoriaProfile()
    {
        CreateMap<Auditoria, AuditoriaDto>();
        CreateMap<AuditoriaDto, Auditoria>();
    }
}
