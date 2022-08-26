using AutoMapper;
using Udemy.Services.Catalog.Dtos;
using Udemy.Services.Catalog.Models;

namespace Udemy.Services.Catalog.Mapping.AutoMapper
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Course, CourseCreateDto>().ReverseMap();
            CreateMap<Course, CourseUpdateDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();

            CreateMap<Feature, FeatureCreateDto>().ReverseMap();
            CreateMap<Feature, FeatureUpdateDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();

            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
