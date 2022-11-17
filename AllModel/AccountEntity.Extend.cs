using AllModel.Code;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllModel
{
    [NotMapped]
    public partial class AccountEntity
    {
        /// <summary>
        /// 类型展示
        /// </summary>
        public string TypeName => Type.ToDescription();

        /// <summary>
        /// 激活状态展示
        /// </summary>
        public string StatusName => Status.ToDescription();
    }
}