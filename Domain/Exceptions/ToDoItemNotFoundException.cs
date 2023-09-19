namespace Domain.Exceptions
{
    public class ToDoItemNotFoundException : Exception
    {
        public ToDoItemNotFoundException(Guid id)
            : base($"ToDoItem with the id '{id}' does not exist")
        {
        }
    }
}
