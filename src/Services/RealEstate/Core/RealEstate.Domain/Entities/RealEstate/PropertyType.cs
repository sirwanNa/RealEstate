using RealEstate.Domain.Common;
using Shared.Enums;

namespace RealEstate.Domain.Entities.RealEstate
{
    public class PropertyType: BaseEntity
    {
        public RealEstateType RealEstateType { get; set; }
        public Guid PropertyInventoryId { get; set; }
        public virtual PropertyInventory? PropertyInventory { get; set; }
        
    }
}
