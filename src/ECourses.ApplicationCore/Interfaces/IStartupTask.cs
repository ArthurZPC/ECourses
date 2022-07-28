namespace ECourses.ApplicationCore.Interfaces
{
    public interface IStartupTask
    {
        Task Execute(CancellationToken cancellationToken = default);
    }
}
