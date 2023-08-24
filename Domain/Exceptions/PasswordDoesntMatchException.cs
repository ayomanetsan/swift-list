namespace Domain.Exceptions
{
    public class PasswordDoesntMatchException : Exception
    {
        public PasswordDoesntMatchException(string email)
            : base($"Password for '{email}' does not match.")
        {
        }
    }
}
