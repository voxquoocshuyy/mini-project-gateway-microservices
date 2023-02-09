using Microsoft.Extensions.DependencyInjection;
using mini_project_data.Repositories.RoleRepositories;
using mini_project_data.Repositories.UserRepositories;
using mini_project_data.Repositories.WeatherRepositories;

namespace mini_project_data.Repositories;

public static class ModuleRegister
{
    public static void RegisterRepository(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IWeatherRepository, WeatherRepository>();
    }
}