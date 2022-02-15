using System;
using System.Collections.Generic;
using System.Text;

namespace Student.DTO.Login
{
    /// <summary>
    /// 前端初始化配置相关
    /// </summary>
    public class UIConfigResultModel
    {
        /// <summary>
        /// 系统信息
        /// </summary>
        public UISystem System { get; set; }

        /// <summary>
        /// 权限验证
        /// </summary>
        public UIPermission Permission { get; set; }

        /// <summary>
        /// 组件配置
        /// </summary>
        public ComponentConfig Component { get; set; }

    }

    /// <summary>
    /// UI系统信息
    /// </summary>
    public class UISystem
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 版权声明
        /// </summary>
        public string Copyright { get; set; }
    }

    /// <summary>
    /// UI权限验证
    /// </summary>
    public class UIPermission
    {
        /// <summary>
        /// 开启权限验证
        /// </summary>
        public bool Validate { get; set; }

        /// <summary>
        /// 开启按钮权限
        /// </summary>
        public bool Button { get; set; }
    }

    /// <summary>
    /// 前端组件配置
    /// </summary>
    public class ComponentConfig
    {
        /// <summary>
        /// 登录组件
        /// </summary>
        public Login Login { get; set; } = new Login();

        /// <summary>
        /// 对话框组件
        /// </summary>
        public Dialog Dialog { get; set; } = new Dialog();

        /// <summary>
        /// 工具栏
        /// </summary>
        public Toolbar Toolbar { get; set; } = new Toolbar();

    }

    /// <summary>
    /// 登录组件
    /// </summary>
    public class Login
    {
        /// <summary>
        /// 启用验证码功能
        /// </summary>
        public bool VerifyCode { get; set; }
    }

    /// <summary>
    /// 对话框组件
    /// </summary>
    public class Dialog
    {
        /// <summary>
        /// 是否可以点击空白处关闭
        /// </summary>
        public bool CloseOnClickModal { get; set; }

        /// <summary>
        /// 是否可以拖动窗体
        /// </summary>
        public bool Draggable { get; set; }
    }

    /// <summary>
    /// 工具栏
    /// </summary>
    public class Toolbar
    {
        /// <summary>
        /// 全屏
        /// </summary>
        public bool Fullscreen { get; set; } = true;

        /// <summary>
        /// 皮肤设置
        /// </summary>
        public bool Skin { get; set; } = true;

        /// <summary>
        /// 退出
        /// </summary>
        public bool Logout { get; set; } = true;

        /// <summary>
        /// 用户信息
        /// </summary>
        public bool UserInfo { get; set; } = true;
    }
}
