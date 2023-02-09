using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using mini_project_data.Entities;
using mini_project_data.Repositories;

namespace mini_project_data;

public static class ModuleRegister
{
    public static IServiceCollection RegisterData(this IServiceCollection services)
    {
        services.AddScoped<DbContext, ManageContext>();
        services.RegisterRepository();
        return services;
    }
}