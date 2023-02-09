using mini_project_business.ViewModels.Weathers;

namespace mini_project_business.Services.WeatherServices;

public interface IWeatherService
{
    IList<GetWeatherDetailModel> GetWeatherPage(SearchWeatherModel searchWeatherModel);

    public Task<GetWeatherDetailModel> GetWeatherById(int id);

    public Task<GetWeatherDetailModel> CreateWeatherAsync(CreateWeatherModel requestBody);

    public Task<GetWeatherDetailModel> UpdateWeatherAsync(int id, UpdateWeatherModel requestBody);

    public Task DeleteWeatherAsync(int id);
}