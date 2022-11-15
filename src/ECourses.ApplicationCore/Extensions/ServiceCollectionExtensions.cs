using ECourses.ApplicationCore.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Common.Interfaces.Services;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.Services;
using ECourses.ApplicationCore.Validators;
using ECourses.Data;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Identity;
using ECourses.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using ECourses.ApplicationCore.Common.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ECourses.ApplicationCore.RabbitMQ.Interfaces;
using ECourses.ApplicationCore.RabbitMQ.Services;

namespace ECourses.ApplicationCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddSqlServer<ECoursesDbContext>(connectionString,
                o => o.MigrationsAssembly(typeof(ECoursesDbContext).Assembly.FullName));

            services.AddScoped<ECoursesDbContextInitializer>();

            return services;                      
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<ECoursesDbContext>();

            return services;
        }

        public static IServiceCollection AddStartupTask<T>(this IServiceCollection services) where T :class, IStartupTask
        {
            services.AddTransient<IStartupTask, T>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICategoryConverter, CategoryConverter>();
            services.AddTransient<ICategoryValidator, CategoryValidator>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddTransient<ITagConverter, TagConverter>();
            services.AddTransient<ITagValidator, TagValidator>();
            services.AddScoped<ITagRepository, TagRepository>();

            services.AddTransient<IAuthorConverter, AuthorConverter>();
            services.AddTransient<IAuthorValidator, AuthorValidator>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();

            services.AddTransient<ICourseConverter, CourseConverter>();
            services.AddTransient<ICourseValidator, CourseValidator>();
            services.AddScoped<ICourseRepository, CourseRepository>();

            services.AddTransient<IRatingConverter, RatingConverter>();
            services.AddTransient<IRatingValidator, RatingValidator>();
            services.AddScoped<IRatingRepository, RatingRepository>();

            services.AddTransient<IVideoConverter, VideoConverter>();
            services.AddTransient<IVideoValidator, VideoValidator>();
            services.AddScoped<IVideoRepository, VideoRepository>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFileService, FileService>();

            services.AddScoped<IRabbitMQService, RabbitMQService>();

            return services;
        }

        public static IServiceCollection AddGenericEntityValidator(this IServiceCollection services)
        {
            services.AddTransient(typeof(IEntityValidator<>), typeof(EntityValidator<>));

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, byte[] jwtKey)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}
