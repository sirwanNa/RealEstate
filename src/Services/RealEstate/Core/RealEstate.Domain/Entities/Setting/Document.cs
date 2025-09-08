using RealEstate.Domain.Common;
using Shared.Enums;

namespace RealEstate.Domain.Entities.Setting
{
    public class Document : BaseEntity
    {
        public required string FileName { get; set; }
        public required string FilePath { get; set; }
        public Guid RelatedId { get; set; }
        public EntityType EntityType { get; set; } 
    }
}
