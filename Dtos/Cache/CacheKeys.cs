using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Student.DTO.Cache
{
    public static class CacheKeys
    {
        /// <summary>
        /// 验证码
        /// <para>VERIFY_CODE:验证码编号</para>
        /// </summary>
        [Description("验证码")]
        public const string VERIFY_CODE = "VERIFY_CODE";

        /// <summary>
        /// 刷新令牌 
        /// <para>AUTH_REFRESH_TOKEN:刷新令牌</para>
        /// </summary>
        [Description("刷新令牌")]
        public const string AUTH_REFRESH_TOKEN = "AUTH_REFRESH_TOKEN";

        /// <summary>
        /// 认证信息
        /// <para>AUTH_INFO:账户编号:平台类型</para>
        /// </summary>
        [Description("认证信息")]
        public const string AUTH_INFO = "AUTH_INFO";

        /// <summary>
        /// 配置信息
        /// <para>CONFIG_CODE:配置代码</para>
        /// </summary>
        [Description("配置信息")]
        public const string CONFIG_CODE = "CONFIG_CODE";


        /// <summary>
        /// 权限列表
        /// <para>PERMISSIONS:账户编号:平台类型</para>
        /// </summary>
        [Description("权限列表")]
        public const string PERMISSIONS = "PERMISSIONS";

        /// <summary>
        /// 权限树
        /// <para>PERMISSION:TREE</para>
        /// </summary>
        [Description("权限树")]
        public const string PERMISSION_TREE = "PERMISSION:TREE";
    }
}
