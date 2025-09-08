using RealEstate.Domain.Common;
using RealEstate.Domain.Entities.Setting;
using Shared.Enums;

namespace RealEstate.Domain.Entities.RealEstate
{
    public class PropertyInventory : BaseEntity
	{
		public StructureType StructureType { get; set; }
		public PositionType PositionType { get; set; }
        //public RealEstateType RealEstateType { get; set; }
        public string? StartDate { get; set; }
		public string? FinishDate { get; set; }	
		public string? Prepayment { get; set; }
		public string? DuringProjectPayment { get; set; }
		public string? HandoverPayment { get; set; }
        public string? AfterHandoverPayment { get; set; }
        public int Price { get; set; }
		public string? TotalOfRooms { get; set; }
		public string? Capacity { get; set; }
		public Currency Currency { get; set; }
		public Guid RegionId { get; set; }
		public virtual Constant? Region { get; set; }
		public Guid BuilderId { get; set; }
		public virtual Constant? Builder { get; set; }
		public string? BrochureLink { get; set; }
        public virtual ICollection<PropertyInventoryTitle>? PropertyInventoryTitles { get; set; }
        public virtual ICollection<PropertyType>? PropertyTypes { get; set; }
        public virtual ICollection<PropertyFeature>? PropertyFeatures { get; set; }
        public virtual ICollection<PropertyTag>? PropertyTags { get; set; }  

	}

}
