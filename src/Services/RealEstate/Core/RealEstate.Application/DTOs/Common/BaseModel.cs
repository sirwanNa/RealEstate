namespace RealEstate.Application.DTOs.Common
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
