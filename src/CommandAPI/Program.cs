using CommandAPI.Data;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.MapGet("/", () => "Hello World!");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void ConfigureServices(IServiceCollection services)
{
    //SECTION 1. Add code below
    services.AddControllers();
    services.AddScoped<ICommandAPIRepo, MockCommandAPIRepo>();
}