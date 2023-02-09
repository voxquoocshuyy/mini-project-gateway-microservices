using mini_project_business.ViewModels.Users;

namespace mini_project_business.Services.UserServices;

public interface IUserService
{
    public Task<string> Login(LoginModel loginModel);
}