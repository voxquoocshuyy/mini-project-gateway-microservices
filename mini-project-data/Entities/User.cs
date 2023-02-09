using System;
using System.Collections.Generic;

namespace mini_project_data.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Password { get; set; }

    public string Address { get; set; } = null!;

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }
}
