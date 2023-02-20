using ECourses.ApplicationCore.Common.Configuration;
using ECourses.ApplicationCore.Extensions;
using ECourses.Web.Extensions;
using ECourses.Web.Filters;
using ECourses.Web.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Text;
using MediatR;
using ECourses.ApplicationCore.RabbitMQ.Configuration;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var defaultConnectionString = configuration.GetConnectionString("DockerConnection");

var jwtKey = configuration.GetValue<string>("JWT:Key");
var jwtKeyBytes = Encoding.ASCII.GetBytes(jwtKey);

builder.Services
    .ConfigureDatabase(defaultConnectionString)
    .AddIdentityServices();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    //options.SuppressModelStateInvalidFilter = true;
})
    .Configure<WebRootOptions>(x => x.WebRootLocation = builder.Configuration.GetValue<string>("WebRootLocation"))
    .Configure<JwtOptions>(builder.Configuration.GetSection("JWT"))
    .Configure<RabbitMQOptions>(builder.Configuration.GetSection("RabbitMQ"))
    .Configure<PasswordSettingsOptions>(builder.Configuration.GetSection("PasswordSettings"));

builder.Services
    .AddStartupTasks()
    .AddGenericEntityValidator()
    .AddRepositories()
    .AddApplicationServices()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(o =>
    {
        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "JWT Authentication",
            Description = "Enter JWT token without 'Bearer'",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme,
            }
        };

        o.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
        o.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { securityScheme, Array.Empty<string>() }
        });
        o.OperationFilter<CamelCasePropertiesFilter>();
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

builder.Services.AddHttpContextAccessor();

builder.Services.AddJwtAuthentication(jwtKeyBytes);
builder.Services.AddAuthorizationCore();

builder.Services.AddMediatR(typeof(JwtOptions).Assembly);

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
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<JwtMiddleware>();


app.UseEndpoints(c => c.MapControllers());

app.Run();
