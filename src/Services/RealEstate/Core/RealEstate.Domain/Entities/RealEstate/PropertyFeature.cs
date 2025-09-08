using RealEstate.Domain.Common;
using Shared.Enums;

namespace RealEstate.Domain.Entities.RealEstate
{
    public class PropertyFeature: BaseEntity
    {
        public FeatureType FeatureType { get; set; }
        public Guid PropertyInventoryId { get; set; }
        public virtual PropertyInventory? PropertyInventory { get; set; }
    }
}
