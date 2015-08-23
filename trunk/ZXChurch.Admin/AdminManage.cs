using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using ZXChurch.Common;

namespace ZXChurch.Admin
{
    /// <summary>
    /// 管理员信息类
    /// </summary>
    public class AdminManage
    {
        /// <summary>
        /// 判断登陆
        /// </summary>
        public static bool CheckAdmin
        {
            get {
                return System.Web.HttpContext.Current.Session[SessionKeys.AdminSession] != null;
            }
        }
        /// <summary>
        /// 管理员ID
        /// </summary>
        public static int AdminID {
            get {
                int id = 0;
                if (System.Web.HttpContext.Current.Session[SessionKeys.AdminSession] != null)
                {
                    id = int.Parse(System.Web.HttpContext.Current.Session[SessionKeys.AdminSession].ToString().Split('|')[1]);
                }

                return id;
            }
        }
        /// <summary>
        /// 管理员名
        /// </summary>
        public static string AdminName
        {
            get {
                string Name = "";
                if (System.Web.HttpContext.Current.Session[SessionKeys.AdminSession] != null)
                {
                    Name = System.Web.HttpContext.Current.Session[SessionKeys.AdminSession].ToString().Split('|')[0];
                }

                return Name;
            }
        }
        /// <summary>
        /// 管理员类型
        /// </summary>
        public static AdminType UserType
        {
            get {
                int intType = 0;
                if (System.Web.HttpContext.Current.Session[SessionKeys.AdminSession] != null)
                {
                    intType = int.Parse(System.Web.HttpContext.Current.Session[SessionKeys.AdminSession].ToString().Split('|')[2]);
                }                
                return (AdminType)System.Enum.Parse(typeof(AdminType),intType.ToString());
            }
        }
        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="SysName"></param>
        /// <param name="Doing"></param>
        public static void SaveWebLog(string SysName, string Doing)
        {      
            string url="http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port+"/log/LogHandler.ashx";         
            string postData = "action=save-log&adminid=" + AdminID + "&adminname=" + AdminName + "&sysname=" + SysName + "&doing=" + Doing;
            Util.PostData(url,postData);
        }

        public static Dictionary<int,string> GetAdminTypeList
        {
            get {
                Dictionary<int, string> dic = new Dictionary<int, string>();
                dic.Add(AdminType.NonAdmin.GetHashCode(), "非管理员");
                dic.Add(AdminType.Ordinary.GetHashCode(), "普通管理员");
                dic.Add(AdminType.SysAdmin.GetHashCode(), "系统管理员");
                dic.Add(AdminType.SuperAdmin.GetHashCode(), "超级管理员");
                return dic;
            }
        }
        /// <summary>
        /// 是否有系统管理员权限
        /// </summary>
        public static bool CheckSysAdmin {
            get {
                if (UserType == AdminType.SuperAdmin || UserType == AdminType.SysAdmin) { return true; }
                else return false;
            }
        }
    }

    public enum AdminType
    { 
        /// <summary>
        /// 非管理员
        /// </summary>
        NonAdmin=0,
        /// <summary>
        /// 超级管理员
        /// </summary>
        SuperAdmin=1,
        /// <summary>
        /// 系统管理员
        /// </summary>
        SysAdmin=2,
        /// <summary>
        /// 普通管理员
        /// </summary>
        Ordinary=3
    }

    
}
