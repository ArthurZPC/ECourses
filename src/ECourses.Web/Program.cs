using ECourses.ApplicationCore.Extensions;
using ECourses.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services
    .ConfigureDatabase(defaultConnectionString)
    .ConfigureIdentity();

builder.Services
    .AddStartupTasks()
    .AddRepositories()
    .AddApplicationServices()
    .AddControllers();

var app = builder.Build();

await app.RunStartupTasks();

app.MapControllers();

app.Run();
