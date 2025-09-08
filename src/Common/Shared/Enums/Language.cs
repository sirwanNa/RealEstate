using System.ComponentModel.DataAnnotations;

namespace Shared.Enums
{
    public enum Language
    {
        [Display(Name ="فارسی")]
		Persian,
		[Display(Name = "انگلیسی")]
		English,
		[Display(Name = "عربی")]
		Arabic       
    }
}
