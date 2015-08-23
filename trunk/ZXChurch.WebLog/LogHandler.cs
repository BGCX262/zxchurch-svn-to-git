using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZXChurch.Common;

namespace ZXChurch.WebLog
{
    /// <summary>
    /// LogHandler 的摘要说明
    /// </summary>
    public class LogHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
            string action = RequestHelper.RequestString("action").ToLower();
            switch (action)
            {
                case "save-log": SaveLog(); break;
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

        private void SaveLog()
        {
            string SysName=RequestHelper.RequestString("sysname"); 
            int AdminId=RequestHelper.RequestInt("adminid"); 
            string AdminName=RequestHelper.RequestString("adminname");
            string Doing = RequestHelper.RequestString("doing");

            new DB().SaveLog(SysName, AdminId, AdminName, Doing);

        }
    }
}
