using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Data.OleDb;
using ZXChurch.Common;
using System.Data;

namespace ZXChurch.Admin.News
{
    /// <summary>
    /// WebHandler 文章数据处理类
    /// </summary>
    public class WebHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = RequestHelper.RequestString("action").ToLower();
            switch (action)
            {
                case "sys-admin": //系统后台管理员信息操作
                    SysAdmin(context);
                    break;
                default: break;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }        
        /// <summary>
        /// 系统后台管理员操作 2013-03-19
        /// </summary>
        /// <param name="context"></param>
        private void SysAdmin(HttpContext context)
        {
            if (AdminManage.UserType!=AdminType.NonAdmin)
            {
                string strDo = RequestHelper.RequestString("do").ToLower();
                int id = RequestHelper.RequestInt("id");
                int isShow = RequestHelper.RequestInt("IsShow");
                if (isShow==0)
                {
                    isShow = 1;
                }
                else
                {
                    isShow = 0; 
                }
                switch (strDo)
                {
                    case "update":
                        #region 更新管理权限
                        if (id > 0)
                        {
                            if (new DBNews().updateByNewsIsShow(id, isShow))
                            {
                                context.Response.Write("{IsShow:'" + isShow + "',result:1,msg:'保存成功！'}");
                            }
                            else
                            {
                                context.Response.Write("{result:0,msg:'保存失败！'}");
                            }
                        }
                        else
                        {
                            context.Response.Write("{result:-1,msg:'参数错误！'}");
                        }
                        #endregion
                        break;
                    default:
                        context.Response.Write("{result:-1,msg:'无效的请求！'}");
                        break;
                }
            }
            else
            {
                context.Response.Write("{result:-1,msg:'没有权限或登陆超时！'}");
            }

        }
    }
}
