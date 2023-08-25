namespace Domain.Exceptions
{
    public class UserAlreadyExists : Exception
    {
        public UserAlreadyExists(string email)
            : base($"Email '{email}' is already registered")
        {            
        }
    }
}
