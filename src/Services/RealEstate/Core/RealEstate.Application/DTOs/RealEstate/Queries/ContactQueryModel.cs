using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.DTOs.RealEstate.Queries
{
	public class ContactQueryDTO
	{
		[Range(1, int.MaxValue, ErrorMessage = "Value must be greater than or equal to 1.")]
		public int PageNumber { get; set; }
		[Range(1, int.MaxValue, ErrorMessage = "Value must be greater than or equal to 1.")]
		public int PageSize { get; set; }
		public  string? Name { get; set; }
		public  string? Message { get; set; }
		public  string? Email { get; set; }
		public  string? Phone { get; set; }
	}
}
