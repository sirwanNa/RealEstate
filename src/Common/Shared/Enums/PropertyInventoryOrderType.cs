using System.ComponentModel.DataAnnotations;

namespace Shared.Enums
{
    public enum PropertyInventoryOrderType
    {
        //[Display(Name ="پیش فرض")]
        Default,
		//[Display(Name = "قیمت: کم به زیاد")]
		Price_Low_To_High,
		//[Display(Name = "قیمت: زیاد به کم")]
		Price_High_To_Low,
		//[Display(Name = "تاریخ: جدیدترین")]
		Date_New_To_Old,
		//[Display(Name = "تاریخ: قدیمی‌ترین")]
		Date_Old_To_New,
		CreateDate
    }
}
