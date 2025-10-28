namespace TrackingCodeApi.dtos.tag;

using AutoMapper;
using TrackingCodeApi.models;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagDto>();
        CreateMap<TagDto, Tag>();
    }
}
