using System.ComponentModel.DataAnnotations;

namespace Shared.Enums
{
    public enum FeatureType
    {
        [Display(Name = "سیستم خانه هوشمند")]
        SmartHome,
        [Display(Name = "مناطق بازی کودکان")]
        ChildrenPlayground,
        [Display(Name = "استخر")]
        Pool,
        [Display(Name = "جیم")]
        Gym,
        [Display(Name = "امنیت 24/7")]
        Security,
        [Display(Name = "مناطق سرسبز")]
        GreenAreas,
        [Display(Name = "مناطق پیاده وری")]
        JoggingTracks,
        [Display(Name = "زمین پادل")]
        PadelCourt,
        [Display(Name = "فضای غذاخوری در فضای باز")]
        OutdoorDiningArea,
        [Display(Name = "فضای باربیکیو")]
        BBQArea,
        [Display(Name = "فضای نشیمن در فضای باز")]
        OutdoorLoungeSpace,
        [Display(Name = "کلاب")]
        Clubhouse,
        [Display(Name = "خدمات ویژه")]
        PremiumServices
    }
}
