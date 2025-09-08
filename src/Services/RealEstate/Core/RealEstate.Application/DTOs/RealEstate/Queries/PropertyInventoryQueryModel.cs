using System.ComponentModel.DataAnnotations;
using Shared.Enums;

namespace RealEstate.Application.DTOs.RealEstate.Queries
{
    public class PropertyInventoryQueryDTO
	{
        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than or equal to 1.")]
        public int PageNumber { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than or equal to 1.")]
        public int PageSize { get; set; }
        public Language Language { get; set; }
        public StructureType? StructureType { get; set; }
        public RealEstateType? RealEstateType { get; set; }
        public string? StartDate { get; set; }
        public string? FinishDate { get; set; }
        public int? Price_From { get; set; }
        public int? Price_To { get; set; }
        //public int? TotalOfRooms { get; set; }
        //public int? Capacity_From { get; set; }
        //public int? Capacity_To { get; set; }
        public Guid? RegionId { get; set; }      
        public Guid? BuilderId { get; set; } 
        public string? Title { get; set; }
        public string? Description { get; set; }
        public PropertyInventoryOrderType OrderBy { get; set; }
    }
}
