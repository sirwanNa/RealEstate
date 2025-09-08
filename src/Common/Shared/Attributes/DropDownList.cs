using System;

namespace Shared.Attributes
{
    public class DropDownList:Attribute
    {
        public string DataSource { get; set; }
    }
}
