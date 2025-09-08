using System.ComponentModel.DataAnnotations;
using RealEstate.Application.DTOs.Common;
using Shared.Enums;

namespace RealEstate.Application.DTOs.Setting
{
    public class  ConstantListDTO : BaseModel
	{		
		public int PagesCount { get; set; }
		public required List<ConstantListItemDTO> ItemsList { get; set; }
	}
	public class ConstantListItemDTO : BaseModel 
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
    }
    public class ConstanDTO : BaseModel
    {
        public ConstantType Type { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public string? Title_Fa { get; set; }
        public string? Description_Fa { get; set; }
        [Required]
        public string? Title_Ar { get; set; }
        public string? Description_Ar { get; set; }
    }
}
