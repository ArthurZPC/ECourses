using ECourses.Data;
using ECourses.Data.Identity;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSqlServer<ECoursesDbContext>(defaultConnectionString, 
    o => o.MigrationsAssembly(typeof(ECoursesDbContext).Assembly.FullName));

builder.Services.AddScoped<ECoursesDbContextInitializer>();

builder.Services.AddIdentity<User, Role>(o =>
{
    o.Password.RequireDigit = false;
    o.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<ECoursesDbContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContextInitializer = scope.ServiceProvider.GetRequiredService<ECoursesDbContextInitializer>();
    await dbContextInitializer.Initialize();
    await dbContextInitializer.Seed();
}

app.MapGet("/", () => "Hello World!");

app.Run();
