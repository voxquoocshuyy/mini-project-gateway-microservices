using Microsoft.EntityFrameworkCore;
using mini_project_data.Entities;

namespace mini_project_data.Repositories.RoleRepositories;

public class RoleRepository: BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(DbContext context) : base(context)
    {
        
    }

    public RoleRepository(DbContext context, DbSet<Role> dbsetExist) : base(context, dbsetExist)
    {
        
    }
}