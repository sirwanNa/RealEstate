using RealEstate.Domain.Common;
using Shared.Enums;

namespace RealEstate.Domain.Entities.Blog
{
    public class Article:BaseEntity
	{
        public required string Title { get; set; }
        public required string UrlTitle { get; set; } 
        public required string Description { get; set; }
        public Language Language { get; set; }   
        public virtual ICollection<ArticleTag>? ArticleTags { get; set; }
    }
}
