using ECourses.Data;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSqlServer<ECoursesDbContext>(defaultConnectionString, 
    o => o.MigrationsAssembly(typeof(ECoursesDbContext).Assembly.FullName));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
