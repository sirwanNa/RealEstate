using RealEstate.Domain.Common;
using RealEstate.Domain.Entities.Setting;
using Shared.Enums;

namespace RealEstate.Domain.Entities.Blog
{
    public class ArticleTag:BaseEntity
	{
		public Language Language { get; set; }
		public Guid ArticleId { get; set; }
		public virtual Article? Article { get; set; }		
		public Guid TagId { get; set; }
		public virtual Constant? Tag { get; set; } 
		

	}
}
