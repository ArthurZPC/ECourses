namespace ECourses.ApplicationCore.Common.Exceptions
{
    public class EntityExistsException : Exception
    {
        private const string DefaultMessage = "Entity already exists.";
        public EntityExistsException(string message) : base(message) { }
        public EntityExistsException(string message, Exception innerException) : base(message, innerException) { }
        public EntityExistsException(Exception innerException) : base(DefaultMessage, innerException) { }
        public EntityExistsException() : base(DefaultMessage) { }
    }
}
