namespace Domain.Exceptions;

public class FriendshipNotFoundException : Exception
{
    public FriendshipNotFoundException(string userEmail, string requesterEmail)
        : base($"Friendship between {userEmail} and {requesterEmail} does not exist")
    {
    }
}