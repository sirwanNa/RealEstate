using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Shared.Enums
{
    public static class EnumExtensions
    {
        public static string? GetDisplayName(this System.Enum enumValue)
        {
            var result = enumValue?.GetType()?
              .GetMember(enumValue.ToString())
              .FirstOrDefault()?
              .GetCustomAttribute<DisplayAttribute>()
              ?.GetName();
            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }
            else
            {
                return enumValue?.ToString();
            }
        }
    }
}
