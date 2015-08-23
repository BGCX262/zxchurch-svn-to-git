using System;
using System.Collections.Generic;
using System.Text;

namespace ZXChurch.Admin
{
    /// <summary>
    /// 定义的SessionKey
    /// </summary>
    public class SessionKeys
    {
        /// <summary>
        /// 管理员Session
        /// </summary>
        public const string AdminSession = "Admin";
        /// <summary>
        /// 月报管理Session
        /// </summary>
        public const string WeeklyAdminSession = "WeeklyAdmin";
        /// <summary>
        /// 管理员登陆
        /// </summary>
        public const string AdminLoginCheckCodeSession = "AdminLoginCheckCode";
        /// <summary>
        /// 登陆错误次数
        /// </summary>
        public const string AdminLoginErrorSession = "AdminLoginError";
    }
}
