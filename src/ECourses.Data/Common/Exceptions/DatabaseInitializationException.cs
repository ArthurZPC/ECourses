namespace ECourses.Data.Common.Exceptions
{
    public class DatabaseInitializationException : Exception
    {
        private const string DefaultMessage = "Error occured during database initialization.";
        public DatabaseInitializationException(string message) : base(message) { }
        public DatabaseInitializationException(string message, Exception innerException) : base(message, innerException) { }
        public DatabaseInitializationException(Exception innerException) : base(DefaultMessage, innerException) { }
        public DatabaseInitializationException():base(DefaultMessage) { }
    }
}
