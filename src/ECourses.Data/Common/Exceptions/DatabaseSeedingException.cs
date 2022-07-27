namespace ECourses.Data.Common.Exceptions
{
    public class DatabaseSeedingException : Exception
    {
        private const string DefaultMessage = "Error occured during database seeding.";
        public DatabaseSeedingException(string message):base(message) { }
        public DatabaseSeedingException(string message, Exception innerException) : base(message, innerException) { }
        public DatabaseSeedingException(Exception innerException):base(DefaultMessage, innerException) { }
        public DatabaseSeedingException():base(DefaultMessage) { }
    }
}
