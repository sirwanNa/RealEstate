using RealEstate.Domain.Common;
using RealEstate.Domain.Entities.Blog;
using RealEstate.Domain.Entities.RealEstate;
using Shared.Enums;

namespace RealEstate.Domain.Entities.Setting
{
    public class Constant : BaseEntity
    {
        public ConstantType Type { get; set; }
        public virtual ICollection<ConstantTitle>? ConstantTitles { get; set; } 
        public virtual ICollection<ArticleTag>? ArticleTags { get; set; }
        public virtual ICollection<PropertyInventory>? PropertyInventory_Regions { get; set; }
        public virtual ICollection<PropertyInventory>? PropertyInventory_Builders { get; set; }
		public virtual ICollection<PropertyTag>? PropertyTags { get; set; }
	}
}
