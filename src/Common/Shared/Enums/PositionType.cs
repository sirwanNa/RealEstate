using System.ComponentModel.DataAnnotations;

namespace Shared.Enums
{
    public enum PositionType
    {
        [Display(Name = "همیشگی")]
        Freehold,
        [Display(Name = "99 ساله")]
        Leasehold
    }
}
