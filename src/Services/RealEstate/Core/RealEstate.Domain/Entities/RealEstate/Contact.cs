using RealEstate.Domain.Common;

namespace RealEstate.Domain.Entities.RealEstate
{
	public class Contact:BaseEntity
	{
		public required string Name { get; set; }
		public required string Message { get; set; }
		public string? Email { get; set; }
		public required string Phone { get; set; }
	}
}
