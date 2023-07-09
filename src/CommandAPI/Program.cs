using CommandAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//var configuration = builder.Configuration;

//ConfigureServices(builder.Services, builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
//builder.Services.AddScoped<ICommandAPIRepo, MockCommandAPIRepo>();
builder.Services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();
builder.Services.AddDbContext<CommandContext>(opt => opt.UseNpgsql
    (builder.Configuration.GetConnectionString("PostgreSqlConnection")));

var app = builder.Build();

app.UseRouting();

app.MapGet("/", () => "Hello World!");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// void ConfigureServices(IServiceCollection services, IConfiguration configuration)
// {
//     services.AddControllers();
//     services.AddScoped<ICommandAPIRepo, MockCommandAPIRepo>();
//     services.AddDbContext<CommandContext>(opt => opt.UseNpgsql
//         (configuration.GetConnectionString("PostgreSqlConnection")));
// }