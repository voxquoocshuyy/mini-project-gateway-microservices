using System.ComponentModel;

namespace mini_project_business.ViewModels.Weathers;

public class SearchWeatherModel
{
    [DefaultValue("")]
    public string City { get; set; } = "";

    public float? Temperature { get; set; } = null;
}