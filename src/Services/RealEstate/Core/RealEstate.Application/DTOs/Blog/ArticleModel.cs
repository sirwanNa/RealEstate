using System.ComponentModel.DataAnnotations;
using RealEstate.Application.DTOs.Common;
using Shared.Enums;

namespace RealEstate.Application.DTOs.Blog
{
    public class ArticleListDTO : BaseModel
	{
		public int PagesCount { get; set; }
		public required List<ArticleListItemDTO> ItemsList { get; set; }
	}
	public class ArticleListItemDTO : BaseModel
	{
		public required string Title { get; set; }
        public required string UrlTitle { get; set; }
    }
	public class ArticleDTO:BaseModel
	{
		[Required]
		public  string? Title { get; set; }
        [Required]
        public  string? Description { get; set; }
		public Language Language { get; set; }
		public Guid ArticleId { get; set; }
		public List<Guid>? SelectedTagIdsList { get; set; }
		public List<FileUploadViewModelPost>? FilesList { get; set; }
    }
    public class ArticleDetailsDTO : BaseModel
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        public List<string>? SelectedTagsList { get; set; }   
    }
    public class ArticleListItemEndUserDTO : BaseModel
    {
        public string? Title { get; set; }
        public string? UrlTitle { get; set; }
        public string? Description { get; set; }
        public string? MainImagePath { get; set; }
        public string? FileName { get; set; }
    }
}
