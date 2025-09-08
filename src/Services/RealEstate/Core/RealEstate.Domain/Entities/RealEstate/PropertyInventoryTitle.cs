using RealEstate.Domain.Common;
using Shared.Enums;

namespace RealEstate.Domain.Entities.RealEstate
{
    public class PropertyInventoryTitle : BaseEntity
	{
		public Language Language { get; set; }
		public required string Title { get; set; }
		public required string UrlTitle { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
		public Guid PropertyInventoryId { get; set; } 
		public string? PaymentConditions { get; set; }
		public virtual PropertyInventory? PropertyInventory { get; set; }
	}
}
