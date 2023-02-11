using AutoMapper;
using mini_project_business.ViewModels.Weathers;
using mini_project_data.Entities;
using mini_project_data.Repositories.WeatherRepositories;

namespace mini_project_business.Services.WeatherServices;

public class WeatherService : IWeatherService
{
    private readonly IWeatherRepository _weatherRepository;
    private readonly IMapper _mapper;

    public WeatherService(IWeatherRepository weatherRepository, IMapper mapper)
    {
        _weatherRepository = weatherRepository;
        _mapper = mapper;
    }

    public IList<GetWeatherDetailModel> GetWeatherPage(SearchWeatherModel searchWeatherModel)
    {
        IQueryable<Weather> queryWeather = _weatherRepository.GetAll();
        if (searchWeatherModel.City != "")
        {
            queryWeather = queryWeather.Where(x => x.City.Contains(searchWeatherModel.City));
        }
        if(searchWeatherModel.Temperature != null)
        {
            queryWeather = queryWeather.Where(x => x.Temperature >= searchWeatherModel.Temperature + 1 
                                                   || x.Temperature <= searchWeatherModel.Temperature);
        }
        var result = _mapper.ProjectTo<GetWeatherDetailModel>(queryWeather);
        return result.ToList();
    }

    public async Task<GetWeatherDetailModel> GetWeatherById(int id)
    {
        Weather weather = await _weatherRepository.GetFirstOrDefaultAsync(w => w.Id == id);
        if (weather == null)
        {
            throw new Exception("Please enter the correct information!!! ");
        }
        var result = _mapper.Map<GetWeatherDetailModel>(weather);
        return result;
    }

    public async Task<GetWeatherDetailModel> CreateWeatherAsync(CreateWeatherModel requestBody)
    {
        Weather weather = _mapper.Map<Weather>(requestBody);
        if (weather == null)
        {
            throw new Exception("Please enter the correct information!!! ");
        }
        await _weatherRepository.InsertAsync(weather);
        await _weatherRepository.SaveChangesAsync();
        GetWeatherDetailModel weatherDetail = _mapper.Map<GetWeatherDetailModel>(weather);
        return weatherDetail;
    }

    public async Task<GetWeatherDetailModel> UpdateWeatherAsync(int id, UpdateWeatherModel requestBody)
    {
        if (id != requestBody.Id)
        {
            throw new Exception("Please enter the correct information!!! ");
        }
        Weather weather = await _weatherRepository.GetFirstOrDefaultAsync(w => w.Id == requestBody.Id);
        if (weather == null)
        {
            throw new Exception("Please enter the correct information!!! ");
        }
        weather = _mapper.Map(requestBody, weather);
        _weatherRepository.Update(weather);
        await _weatherRepository.SaveChangesAsync();
        GetWeatherDetailModel weatherDetail = _mapper.Map<GetWeatherDetailModel>(weather);
        return weatherDetail;
    }

    public async Task DeleteWeatherAsync(int id)
    {
        Weather block = await _weatherRepository.GetFirstOrDefaultAsync(w => w.Id == id);
        if (block == null)
        {
            throw new Exception("Please enter the correct information!!! ");
        }
        _weatherRepository.Delete(block);
        await _weatherRepository.SaveChangesAsync();
    }
}