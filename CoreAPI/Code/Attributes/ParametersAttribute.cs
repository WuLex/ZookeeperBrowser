using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.ComponentModel
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ParametersAttribute : Attribute
    {
        public string name { get; set; }
        public string param { get; set; }
        public ParametersAttribute()
        {
        }
    }
}
