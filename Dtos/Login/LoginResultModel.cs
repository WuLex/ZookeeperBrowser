using System;
using System.Collections.Generic;
using System.Text;

namespace Student.DTO.Login
{
    public class LoginResultModel
    {
        /// <summary>
        /// 账户信息
        /// </summary>
        public AccountDTO Account { get; set; }

        /// <summary>
        /// 认证信息
        /// </summary>
        public AuthInfoDTO AuthInfo { get; set; }

        /// <summary>
        /// 菜单列表
        /// </summary>
        //public IList<AccountMenuItem> Menus { get; set; }
    }
}
