using ECourses.ApplicationCore.Interfaces;

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
    }
}
