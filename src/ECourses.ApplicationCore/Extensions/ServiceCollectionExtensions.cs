using ECourses.ApplicationCore.Interfaces;
using ECourses.Data;
using ECourses.Data.Identity;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
