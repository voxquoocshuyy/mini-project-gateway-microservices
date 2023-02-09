using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using mini_project_business.ViewModels.ConfigureMappers;

namespace mini_project_business.ViewModels;

public static class AutoMapperConfig
{
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.ConfigWeather();
        });
        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}