namespace RealEstate.Domain.Common
{
	public class BaseEntity
	{
		public Guid Id { get; set; }		
		public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
