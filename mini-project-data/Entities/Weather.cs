using System;
using System.Collections.Generic;

namespace mini_project_data.Entities;

public partial class Weather
{
    public int Id { get; set; }

    public string City { get; set; } = null!;

    public float? Temperature { get; set; }
}
