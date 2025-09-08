using RealEstate.Application.DTOs.Common;

namespace RealEstate.Application.DTOs.RealEstate
{
	public class ContactListDTO : BaseModel
	{
		public int PagesCount { get; set; }
		public required List<ContactListItemDTO> ItemsList { get; set; }
	}
	public class ContactListItemDTO : BaseModel
	{
		public required string Name { get; set; }
		public required string Message { get; set; }
		public string? Email { get; set; }
		public required string Phone { get; set; }
	}
	public class ContactDTO:BaseModel
	{
		public required string Name { get; set; }
		public required string Message { get; set; }
		public string? Email { get; set; }
		public required string Phone { get; set; }
	}

}
