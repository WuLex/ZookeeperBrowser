using System;
using System.ComponentModel.DataAnnotations.Schema;
using ZookeeperBrowser.Model.Code;

namespace ZookeeperBrowser.Model
{
    [NotMapped]
    public partial class Account
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
