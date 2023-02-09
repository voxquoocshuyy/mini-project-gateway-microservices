using Microsoft.EntityFrameworkCore;
using mini_project_data.Entities;

namespace mini_project_data.Repositories.UserRepositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {
        
    }

    public UserRepository(DbContext context, DbSet<User> dbsetExist) : base(context, dbsetExist)
    {
        
    }
}