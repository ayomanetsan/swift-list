namespace Domain.Exceptions
{
    public class ProjectNotFoundException : Exception
    {
        public ProjectNotFoundException(Guid id)
            : base($"Project with the id '{id}' does not exist")
        {
        }
    }
}
