namespace mini_project_business.ViewModels;

public class BaseResponse<T>
{

    public int Code { get; set; }
    public string Msg { get; set; }
    public T Data { get; set; }
}

public class ModelsResponse<T>
{
    public int Code { get; set; }
    public string Msg { get; set; }
    public List<T> Data { get; set; }
}
public class ModelResponseLogin
{
    public int Code { get; set; }
    public string Msg { get; set; }
    public string? Data { get; set; }
}