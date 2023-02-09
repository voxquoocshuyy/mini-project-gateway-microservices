namespace mini_project_business.ViewModels.Weathers;

public class GetWeatherDetailModel
{
    public int Id { get; set; }

    public string City { get; set; } = null!;

    public float? Temperature { get; set; }
}