using ECourses.ApplicationCore.Extensions;
using ECourses.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services
    .ConfigureDatabase(defaultConnectionString)
    .ConfigureIdentity();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services
    .AddStartupTasks()
    .AddGenericEntityValidator()
    .AddRepositories()
    .AddApplicationServices()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(o =>
    {
        o.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "ECourses",
            Version = "v1",
            Description = "This DEMO app created for selling courses using ASP.NET Core",
            Contact = new OpenApiContact
            {
                Email = "guseynoff.artur@yandex.ru",
                Name = "Artur Huseinau",
            }
        });
    })
    .AddControllers();

var app = builder.Build();

await app.RunStartupTasks();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(o =>
{
    o.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    o.RoutePrefix = String.Empty;
});

app.UseStaticFiles();

app.MapControllers();

app.Run();
