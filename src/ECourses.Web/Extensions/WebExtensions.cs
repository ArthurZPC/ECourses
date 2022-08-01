using ECourses.ApplicationCore.Common.Interfaces;
using ECourses.ApplicationCore.Extensions;
using ECourses.ApplicationCore.StartupTasks;

namespace ECourses.Web.Extensions
{
    public static class WebExtensions
    {
        public static async Task<WebApplication> RunStartupTasks(this WebApplication app)
        {
            var tasks = app.Services.GetServices<IStartupTask>();

            foreach (var task in tasks)
            {
                await task.Execute();
            }

            return app;
        }

        public static IServiceCollection AddStartupTasks(this IServiceCollection services)
        {
            services.AddStartupTask<DatabaseInitializationStartupTask>();

            return services;
        }
    }
}
