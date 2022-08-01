namespace ECourses.ApplicationCore.Common.Interfaces
{
    public interface IStartupTask
    {
        Task Execute(CancellationToken cancellationToken = default);
    }
}
