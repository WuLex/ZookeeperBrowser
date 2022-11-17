using System;
using System.Collections.Generic;
using System.Text;

namespace AllDto.Attributes
{
    /// <summary>
    /// Swagger：隐藏属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnorePropertyAttribute : Attribute
    {
    }
}