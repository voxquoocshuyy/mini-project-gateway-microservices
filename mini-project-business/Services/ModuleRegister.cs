using Microsoft.Extensions.DependencyInjection;
using mini_project_business.Services.UserServices;
using mini_project_business.Services.WeatherServices;
using mini_project_business.Utilities;

namespace mini_project_business.Services;

public static class ModuleRegister
{
    public static void RegisterService(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IJwtHelper, JwtHelper>();
        services.AddScoped<IWeatherService, WeatherService>();
    }
}