using AutoMapper;
using MyBlog.Model;

namespace MyBlog.Common.Utility.AutoMapper
{

    public class CustomAutoMapperProfile : Profile
  {
    public CustomAutoMapperProfile()
    {
      base.CreateMap<WriterInfo, WriterInfoDTO>();
      base.CreateMap<BlogNews, BlogNewsDTO>()
        .ForMember(dest => dest.TypeInfoName, sourse => sourse.MapFrom(src => src.TypeInfo.Name))
        .ForMember(dest => dest.WriterInfoName, sourse => sourse.MapFrom(src => src.WriterInfo.Name));
    }
  }
}
