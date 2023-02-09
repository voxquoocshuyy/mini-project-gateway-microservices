using Microsoft.EntityFrameworkCore;
using mini_project_data.Entities;

namespace mini_project_data.Repositories.WeatherRepositories;

public class WeatherRepository : BaseRepository<Weather>, IWeatherRepository
{
    public WeatherRepository(DbContext context) : base(context)
    {
    }

    public WeatherRepository(DbContext context, DbSet<Weather> dbsetExist) : base(context, dbsetExist)
    {
    }
}