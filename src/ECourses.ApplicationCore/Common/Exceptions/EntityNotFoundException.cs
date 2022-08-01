namespace ECourses.ApplicationCore.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        private const string DefaultMessage = "Entity not found.";
        public EntityNotFoundException(string message) : base(message) { }
        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        public EntityNotFoundException(Exception innerException) : base(DefaultMessage, innerException) { }
        public EntityNotFoundException() : base(DefaultMessage) { }
    }
}
