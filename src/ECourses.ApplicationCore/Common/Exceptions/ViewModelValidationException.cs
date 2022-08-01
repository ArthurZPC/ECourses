namespace ECourses.ApplicationCore.Common.Exceptions
{
    public class ViewModelValidationException : Exception
    {
        private const string DefaultMessage = "View Model validation failed.";
        public ViewModelValidationException(string message) : base(message) { }
        public ViewModelValidationException(string message, Exception innerException) : base(message, innerException) { }
        public ViewModelValidationException(Exception innerException) : base(DefaultMessage, innerException) { }
        public ViewModelValidationException() : base(DefaultMessage) { }
    }
}
