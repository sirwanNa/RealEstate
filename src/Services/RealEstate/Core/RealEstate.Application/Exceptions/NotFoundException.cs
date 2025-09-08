namespace RealEstate.Application.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string entityName)
            : base($"Entity \"{entityName}\" was not found.")
        {
        }
    }
}
