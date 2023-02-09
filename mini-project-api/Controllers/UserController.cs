using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mini_project_business.Services.UserServices;
using mini_project_business.ViewModels;
using mini_project_business.ViewModels.Users;

namespace mini_project_api.Controllers;
/// <summary>
/// 
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userService"></param>
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// [USER] Endpoint for user login.
    /// </summary>
    /// <param name="loginModel"></param>
    /// <returns>A user within status 200 or 204 status.</returns>
    /// <response code="200">Returns 200 status</response>
    /// <response code="204">Returns NoContent status</response>
    [Route("login")]
    [HttpPost]
    [AllowAnonymous]
    public ActionResult Login(LoginModel loginModel)
    {
        try
        {
            var response = _userService.Login(loginModel);
            if (response.Result == "Phone or password not correct")
            {
                return BadRequest(new ModelResponseLogin
                {
                    Code = StatusCodes.Status400BadRequest,
                    Data = response.Result,
                    Msg = "Use API login fail!"
                });
            }
            return Ok(new ModelResponseLogin
            {
                Code = StatusCodes.Status200OK,
                Data = response.Result,
                Msg = "Use API login success!"
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
        
    }
}