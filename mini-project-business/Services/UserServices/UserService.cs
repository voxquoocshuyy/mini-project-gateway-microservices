using mini_project_business.Utilities;
using mini_project_business.ViewModels.Users;
using mini_project_data.Repositories.RoleRepositories;
using mini_project_data.Repositories.UserRepositories;

namespace mini_project_business.Services.UserServices;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IJwtHelper _jwtHelper;

    public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IJwtHelper jwtHelper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _jwtHelper = jwtHelper;
    }

    public async Task<string> Login(LoginModel loginModel)
    {
        var user = await _userRepository.GetFirstOrDefaultAsync(u => u.Phone == loginModel.Phone);
        if (user == null || user.Password != loginModel.Password)
        {
            return "Phone or password not correct";
        }
        var role = await _roleRepository.GetFirstOrDefaultAsync(r => r.Id == user.RoleId);
        return _jwtHelper.generateJwtToken(user, role, user.Id);
    }
}