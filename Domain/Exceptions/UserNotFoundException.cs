namespace Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string email)
            : base($"User by the email '{email}' does not exist.")
        {          
        }
    }
}
