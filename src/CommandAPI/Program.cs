using CommandAPI.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

var connectionStringBuilder = new NpgsqlConnectionStringBuilder();

connectionStringBuilder.ConnectionString =
    builder.Configuration.GetConnectionString("PostgreSqlConnection");
 connectionStringBuilder.Username = builder.Configuration["UserID"];
 connectionStringBuilder.Password = builder.Configuration["Password"];

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddScoped<ICommandAPIRepo, MockCommandAPIRepo>();
builder.Services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();
builder.Services.AddDbContext<CommandContext>(opt => opt.UseNpgsql(connectionStringBuilder.ConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseRouting();

app.MapGet("/", () => "Hello World!");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
