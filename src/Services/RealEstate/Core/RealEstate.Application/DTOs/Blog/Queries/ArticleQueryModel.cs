using System.ComponentModel.DataAnnotations;
using Shared.Enums;

namespace RealEstate.Application.DTOs.Blog.Queries
{
    public class ArticleQueryDTO
	{
		[Range(1, int.MaxValue, ErrorMessage = "Value must be greater than or equal to 1.")]
		public int PageNumber { get; set; }
		[Range(1, int.MaxValue, ErrorMessage = "Value must be greater than or equal to 1.")]
		public int PageSize { get; set; }
		public Language Language { get; set; }
		public string? Title { get; set; }
	}
}
