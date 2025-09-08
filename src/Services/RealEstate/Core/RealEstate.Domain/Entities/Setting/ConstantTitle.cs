using RealEstate.Domain.Common;
using Shared.Enums;

namespace RealEstate.Domain.Entities.Setting
{
    public class ConstantTitle : BaseEntity
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public Language Language { get; set; }
        public Guid ConstantId { get; set; }
        public virtual Constant? Constant { get; set; }
    }
}
