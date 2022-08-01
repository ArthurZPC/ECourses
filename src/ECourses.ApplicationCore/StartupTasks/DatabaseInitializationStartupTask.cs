using ECourses.ApplicationCore.Common.Interfaces;
using ECourses.Data;
using Microsoft.Extensions.DependencyInjection;

namespace ECourses.ApplicationCore.StartupTasks
{
    public class DatabaseInitializationStartupTask : IStartupTask
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseInitializationStartupTask(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Execute(CancellationToken cancellationToken = default)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var databaseInitializer = scope.ServiceProvider.GetRequiredService<ECoursesDbContextInitializer>();

                await databaseInitializer.Initialize();
                await databaseInitializer.Seed();
            }
        }
    }
}
