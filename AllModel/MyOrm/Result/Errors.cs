using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllModel.MyOrm.Result
{
    public class Errors
    {
        /// <summary>
        /// 错误字段
        /// </summary>
        [Description("错误字段")]
        public string Id { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [Description("错误信息")]
        public string Msg { get; set; }
    }
}