namespace Domain.Exceptions
{
    public class TaskNotFoundException : Exception
    {
        public TaskNotFoundException(Guid id)
            : base($"Task with the id '{id}' does not exist")
        {
        }
    }
}
