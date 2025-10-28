using TrackingCodeApi.models;
using AutoMapper;

namespace TrackingCodeApi.dtos.common
{
  public class PagedResponseProfile : Profile
  {
     public PagedResponseProfile()
     {
         CreateMap(typeof(PagedResponse<>), typeof(PagedResponseDto<>));
     }
  }
}
