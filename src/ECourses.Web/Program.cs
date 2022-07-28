using ECourses.ApplicationCore.Extensions;
using ECourses.ApplicationCore.StartupTasks;
using ECourses.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services
    .ConfigureDatabase(defaultConnectionString)
    .ConfigureIdentity();

builder.Services.AddStartupTask<DatabaseInitializationStartupTask>();

var app = builder.Build();

await app.RunStartupTasks();

app.MapGet("/", () => "Hello World!");

app.Run();
