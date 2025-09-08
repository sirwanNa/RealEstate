using System.ComponentModel.DataAnnotations;
using RealEstate.Application.DTOs.Common;
using Shared.Enums;

namespace RealEstate.Application.DTOs.RealEstate
{
    public class PropertyInventoryListDTO : BaseModel
	{
		public int PagesCount { get; set; }
		public required List<PropertyInventoryListItemDTO> ItemsList { get; set; }
	}
	public class PropertyInventoryListItemDTO : BaseModel
	{	
		public StructureType StructureType { get; set; }
		public string? RealEstateTypesList { get; set; }
		public  string? Title { get; set; }
        public  string? UrlTitle { get; set; }
		public string? Description { get; set; }
        public string? StartDate { get; set; }
		public string? FinishDate { get; set; }
		public int Price { get; set; }
		public string? TotalOfRooms { get; set; }
		public string? Capacity { get; set; }
		public Currency Currency { get; set; }
		public string? Region { get; set; }
		public string? Builder { get; set; }
		public string? MainFileName { get; set; }
        public string? MainFilePath { get; set; } 
    }
	public class PropertyInventoryDTO:BaseModel
	{
		public StructureType StructureType { get; set; }
        public PositionType PositionType { get; set; }
        public string? AfterHandoverPayment { get; set; }
        public string? StartDate { get; set; }
		public string? FinishDate { get; set; }
		public int Price { get; set; }
		public string? TotalOfRooms { get; set; }
		public string? Capacity { get; set; }
		public Currency Currency { get; set; }
		public Guid RegionId { get; set; }
		public Guid BuilderId { get; set; }
		[Required]
		public string? Title { get; set; }
        public string? Description { get; set; }
		[Required]
		public string? Title_Fa { get; set; }
		public string? Description_Fa { get; set; }
		[Required]
		public string? Title_Ar { get; set; }
		public string? Description_Ar { get; set; }
        public string? ShortDescription { get; set; }
        public string? ShortDescription_Fa { get; set; }
        public string? ShortDescription_Ar { get; set; }
        public List<FileUploadViewModelPost>? FilesList { get; set; }
		public List<Guid>? SelectedTagIdsList_En { get; set; }
		public List<Guid>? SelectedTagIdsList_Fa { get; set; }
		public List<Guid>? SelectedTagIdsList_Ar { get; set; }
        public List<RealEstateType>? SelectedTypesList { get; set; }       
        public List<FeatureType>? SelectedFeaturesList { get; set; }      
        public string? Prepayment { get; set; }       
        public string? DuringProjectPayment { get; set; }      
        public string? HandoverPayment { get; set; }
        public string? BrochureLink { get; set; }
    }
    public class PropertyInventoryFromFilesDTO: PropertyInventoryDTO
    {
        public string? Region { get; set; }
        public string? Builder { get; set; }
    }
    public class UploadFileResultDTO
	{
        public int Code { get; set; } 
        public List<FileUploadViewModelPost>? AddedFilesList { get; set; }
        public List<FileUploadViewModelPost>? DeletedFilesList { get; set; }
    }
    public class PropertyInventoryEndUserDTO
    {
        public StructureType StructureType { get; set; }
        //public RealEstateType RealEstateType { get; set; }
        public string? StartDate { get; set; }
        public string? FinishDate { get; set; }
        public int Price { get; set; }
        public PositionType PositionType { get; set; }
        public string? Builder { get; set; }
        public string? AmenitiesList { get; set; }
        public string? TotalOfRooms { get; set; }
        public string? Capacity { get; set; }
        public Currency Currency { get; set; }
        public string? Region { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }
        public string? Prepayment { get; set; }
        public string? DuringProjectPayment { get; set; }
        public string? HandoverPayment { get; set; }
        public string? AfterHandoverPayment { get; set; }
        public string? BrochureLink { get; set; }
        public List<FileUploadViewModelPost>? FilesList { get; set; }
        public List<string>? SelectedTagsList { get; set; }
		public List<PropertyInventoryRecommendationDTO>? RecommendationList { get; set; } 
    }
    public class PropertyInventoryRecommendationDTO
    {
        public int Price { get; set; }
        public Currency Currency { get; set; }
        public Guid PropertyInventoryId { get; set; }
        public string? Title { get; set; }
		public string? UrlTitle { get; set; }
		public string? FileName { get; set; }
    }
}
