using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using mini_project_business.Services.WeatherServices;
using mini_project_business.ViewModels.Weathers;
using mini_project_data.Entities;

namespace mini_project_api.Controllers;
/// <summary>
/// 
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/redis")]
public class RedisController : ControllerBase
{
    private readonly IDistributedCache _distributedCache;
    private readonly IWeatherService _weatherService;
    private readonly IMemoryCache _memoryCache;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="distributedCache"></param>
    /// <param name="weatherService"></param>
    /// <param name="memoryCache"></param>
    public RedisController(IDistributedCache distributedCache, IWeatherService weatherService, IMemoryCache memoryCache)
    {
        _distributedCache = distributedCache;
        _weatherService = weatherService;
        _memoryCache = memoryCache;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public string Get()
    {
        var cacheKey = "theTime";
        var currentTime = DateTime.Now.ToString();
        var cachedTime = _distributedCache.GetString(cacheKey);
        if (string.IsNullOrEmpty(cachedTime))
        {
            //cachedTime = "Expired"
            //cache expire in 10 seconds
            var options = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(10));
            //set new value to cache
            _distributedCache.SetString(cacheKey, currentTime, options);
            cachedTime = _distributedCache.GetString(cacheKey);
        }
        var result = $"Current Time: {currentTime} \nCached Time: {cachedTime}";
        return result;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="searchWeatherModel"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetWeatherWithCache")]
    public IList<GetWeatherDetailModel>? GetWeatherWithCache([FromQuery] SearchWeatherModel searchWeatherModel)
    {
        var weather = _weatherService.GetWeatherPage(searchWeatherModel);
        if(!_memoryCache.TryGetValue(searchWeatherModel, out weather))
        {
            weather = _weatherService.GetWeatherPage(searchWeatherModel);
            _memoryCache.Set(searchWeatherModel, weather);
        }    
        return weather;
    }
}