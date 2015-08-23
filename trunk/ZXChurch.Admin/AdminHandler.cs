using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Data.OleDb;
using System.Web.Security;
using ZXChurch.Common;

namespace ZXChurch.Admin
{
    /// <summary>
    /// AdminHandler 管理员操作类
    /// </summary>
    public class AdminHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.Form["action"] != null)
            {
                switch (context.Request.Form["action"].ToLower())
                {
                    case "login":
                        Login(context);
                        break;                   
                    default: break;
                }
            }


        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void Login(HttpContext context)
        {

            if (context.Session[SessionKeys.AdminSession] == null)//防止重复登陆
            {
                //错误次数
                int errorCount = context.Session[SessionKeys.AdminLoginErrorSession] == null ? 0 : int.Parse(context.Session[SessionKeys.AdminLoginErrorSession].ToString());
                if (errorCount > 3)
                {
                    context.Response.Write("{result:'error',msg:'系统禁止登陆'}"); return;
                }
                string name = Common.RequestHelper.RequestString("username"), pwd = Common.RequestHelper.RequestString("pwd"), checkcode = Common.RequestHelper.RequestString("checkcode").ToLower();
                if (context.Session[SessionKeys.AdminLoginCheckCodeSession] == null || context.Session[SessionKeys.AdminLoginCheckCodeSession].ToString().ToLower().Equals(checkcode) == false)
                {
                    context.Response.Write("{result:'error',msg:'验证码错误'}"); return;
                }
                if (new DB().Login(name, pwd))
                {
                    context.Response.Write("{result:'ok',msg:'登陆成功'}");
                    AdminManage.SaveWebLog("管理员登陆", name + "登陆成功");
                }
                else
                {
                    errorCount++;
                    context.Session[SessionKeys.AdminLoginErrorSession] = errorCount;
                    context.Response.Write("{result:'error',msg:'用户名或密码错误！'}");
                    AdminManage.SaveWebLog("管理员登陆", name + "用户名或密码错误");
                }


            }
            else
            {
                context.Response.Write("{result:'ok',msg:'已经登陆！'}");

            }
        }
    }
}
