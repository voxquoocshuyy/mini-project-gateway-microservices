using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using mini_project_business.Services.WeatherServices;
using mini_project_business.ViewModels;
using mini_project_business.ViewModels.Weathers;

namespace mini_project_api.Controllers;
/// <summary>
/// 
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/weathers")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;
    private readonly IMemoryCache _memoryCache;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="weatherService"></param>
    /// <param name="memoryCache"></param>
    public WeatherController(IWeatherService weatherService, IMemoryCache memoryCache)
    {
        _weatherService = weatherService;
        _memoryCache = memoryCache;
    }
    /// <summary>
    /// [USER] Endpoint for get all weather with condition
    /// </summary>
    /// <param name="searchWeatherModel"></param>
    /// <returns>List of weather</returns>
    /// <response code="200">Returns the list of weather</response>
    /// <response code="204">Returns if list of weather is empty</response>
    /// <response code="403">Return if token is access denied</response>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ModelsResponse<GetWeatherDetailModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllWeather([FromQuery]SearchWeatherModel searchWeatherModel)
    {
        IList<GetWeatherDetailModel> result = _weatherService.GetWeatherPage(searchWeatherModel);
        if (!result.Any())
        {
            return NoContent();
        }
        if(!_memoryCache.TryGetValue(searchWeatherModel, out result!))
        {
            result = _weatherService.GetWeatherPage(searchWeatherModel);
            _memoryCache.Set(searchWeatherModel, result);
        }    
        return Ok(new ModelsResponse<GetWeatherDetailModel>()
        {
            Code = StatusCodes.Status200OK,
            Data = result.ToList(),
            Msg = "Use API get weather success!"
        });
    }
    
    /// <summary>
    /// [USER] Endpoint for get weather by ID
    /// </summary>
    /// <param name="id">An id of weather</param>
    /// <returns>Info of the weather</returns>
    /// <response code="200">Returns the weather</response>
    /// <response code="204">Returns if the weather is not exist</response>
    /// <response code="403">Return if token is access denied</response>
    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(BaseResponse<GetWeatherDetailModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWeatherById(int id)
    {
        GetWeatherDetailModel result = await _weatherService.GetWeatherById(id);
        return Ok(new BaseResponse<GetWeatherDetailModel>()
        {
            Code = StatusCodes.Status200OK,
            Msg = "Use API get weather by id success!",
            Data = result
        });
    }
    
    /// <summary>
    /// [ADMIN] Endpoint for create weather
    /// </summary>
    /// <param name="requestBody">An obj contains input info of a weather.</param>
    /// <returns>A weather within status 201 or error status.</returns>
    /// <response code="201">Returns the weather</response>
    /// <response code="403">Return if token is access denied</response>
    [HttpPost]
    [Authorize(Roles ="ADMIN")]
    [ProducesResponseType(typeof(BaseResponse<GetWeatherDetailModel>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateWeather([FromBody] CreateWeatherModel requestBody)
    {
        var result = await _weatherService.CreateWeatherAsync(requestBody);

        return Created(string.Empty, new BaseResponse<GetWeatherDetailModel>()
        {
            Code = StatusCodes.Status201Created,
            Data = result,
            Msg = "Send Request Successful"
        });
    }

    /// <summary>
    /// [ADMIN] Endpoint for edit weather.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="requestBody">An obj contains update info of a weather.</param>
    /// <returns>A weather within status 200 or error status.</returns>
    /// <response code="200">Returns weather after update</response>
    /// <response code="403">Return if token is access denied</response>
    [HttpPut("{id}")]
    [Authorize(Roles ="ADMIN")]
    [ProducesResponseType(typeof(BaseResponse<GetWeatherDetailModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateWeatherAsync(int id, [FromBody] UpdateWeatherModel requestBody)
    {
        try
        {
            GetWeatherDetailModel updateWeather = await _weatherService.UpdateWeatherAsync(id,requestBody);

            return Ok(new BaseResponse<GetWeatherDetailModel>()
            {
                Code = StatusCodes.Status200OK,
                Data = updateWeather,
                Msg = "Update Successful"
            });
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
            
    }
    
    /// <summary>
    /// [ADMIN] Endpoint for Admin Delete a weather.
    /// </summary>
    /// <param name="id">ID of weather</param>
    /// <returns>A weather within status 200 or 204 status.</returns>
    /// <response code="200">Returns 200 status</response>
    /// <response code="204">Returns NoContent status</response>
    [HttpDelete("{id}")]
    [Authorize(Roles ="ADMIN")]
    public async Task<IActionResult> DeleteWeatherAsync(int id)
    {
        try
        {
            await _weatherService.DeleteWeatherAsync(id);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
        return NoContent();
    }
}