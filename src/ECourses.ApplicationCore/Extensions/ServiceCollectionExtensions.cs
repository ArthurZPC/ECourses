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

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICourseService, CourseService>();

            return services;
        }

        public static IServiceCollection AddGenericEntityValidator(this IServiceCollection services)
        {
            services.AddTransient(typeof(IEntityValidator<>), typeof(EntityValidator<>));

            return services;
        }
    }
}
