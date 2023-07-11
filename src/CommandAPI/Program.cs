using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using CommandAPI.Data;
using Npgsql;
using AutoMapper;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionStringBuilder = new NpgsqlConnectionStringBuilder();

connectionStringBuilder.ConnectionString =
    builder.Configuration.GetConnectionString("PostgreSqlConnection");
 connectionStringBuilder.Username = builder.Configuration["UserID"];
 connectionStringBuilder.Password = builder.Configuration["Password"];

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers().AddNewtonsoftJson(s =>
{
    s.SerializerSettings.ContractResolver = new
    CamelCasePropertyNamesContractResolver();
});

//builder.Services.AddScoped<ICommandAPIRepo, MockCommandAPIRepo>();
builder.Services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();
builder.Services.AddDbContext<CommandContext>(opt => opt.UseNpgsql(connectionStringBuilder.ConnectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

// app.MapGet("/", () => "Hello World!");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
