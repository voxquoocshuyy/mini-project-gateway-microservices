using Google.Apis.Json;
using mini_project_api.Configurations;
using mini_project_business;
using mini_project_data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    o.SerializerSettings.ContractResolver = new NewtonsoftJsonContractResolver()
    {
        NamingStrategy = new SnakeCaseNamingStrategy()
    };
    o.SerializerSettings.Converters.Add(new StringEnumConverter()
    {
        AllowIntegerValues = true
    });

});
//add redis
//This method gets called by the runtime. Use this method to add services to the container.
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost";
    options.InstanceName = "SampleInstance";
});   
//add authentication in register
builder.Services.RegisterSecurityModule(builder.Configuration);
//add swagger configuration
builder.Services.RegisterSwaggerModule();
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterData();
builder.Services.RegisterBusiness();
builder.Services.AddMemoryCache();

var app = builder.Build();

//add authentication in use
app.UseApplicationSecurity();
app.UseApplicationSwagger();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();