using AutoMapper;
using mini_project_business.ViewModels.Weathers;
using mini_project_data.Entities;

namespace mini_project_business.ViewModels.ConfigureMappers;

public static class WeatherMapper
{
    public static void ConfigWeather(this IMapperConfigurationExpression configuration)
    {
        configuration.CreateMap<Weather, GetWeatherDetailModel>().ReverseMap();
        configuration.CreateMap<Weather, CreateWeatherModel>().ReverseMap();
        configuration.CreateMap<Weather, SearchWeatherModel>().ReverseMap();
        configuration.CreateMap<Weather, UpdateWeatherModel>().ReverseMap();
    }

}