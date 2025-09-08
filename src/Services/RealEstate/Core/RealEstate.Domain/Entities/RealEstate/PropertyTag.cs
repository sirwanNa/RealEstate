using RealEstate.Domain.Common;
using RealEstate.Domain.Entities.Setting;
using Shared.Enums;

namespace RealEstate.Domain.Entities.RealEstate
{
    public class PropertyTag : BaseEntity
	{
		public Language Language { get; set; }
		public Guid PropertyInventoryId { get; set; }
		public Guid TagId { get; set; }
		public virtual PropertyInventory? PropertyInventory { get; set; }
		public virtual Constant? Tag { get; set; }
	}
}
