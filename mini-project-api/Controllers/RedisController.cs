using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="distributedCache"></param>
    public RedisController(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
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
}