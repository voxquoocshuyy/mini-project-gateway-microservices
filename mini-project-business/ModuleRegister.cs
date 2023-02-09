using Microsoft.Extensions.DependencyInjection;
using mini_project_business.Services;
using mini_project_business.ViewModels;

namespace mini_project_business;

public static class ModuleRegister
{
    public static void RegisterBusiness(this IServiceCollection services)
    {
        services.RegisterService();
        services.ConfigureAutoMapper();
    }
}