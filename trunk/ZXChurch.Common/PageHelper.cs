using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace ZXChurch.Common
{
    public class PageHelper
    {
        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="url"></param>
        public static void Redirect(string url)
        {
            if (String.IsNullOrEmpty(url))
            {
                return;
            }
            HttpContext.Current.Response.Redirect(url);
        }
        /// <summary>
        /// 向页面输出警告框
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="mess">提示信息</param>
        public static void Alert(System.Web.UI.Page page, string mess)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", string.Format("<script>alert('{0}')</script>", mess));
        }
        /// <summary>
        /// 向页面输出警告框
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="mess">提示信息</param>
        /// <param name="url">跳转地址</param>
        public static void Alert(System.Web.UI.Page page, string mess, string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", string.Format("<script>alert('{0}');window.location.href='{1}';</script>", mess, url));
        }
        /// <summary>
        /// 在页面执行JS
        /// </summary>
        /// <param name="page"></param>
        /// <param name="mess"></param>
        public static void ExecuteJs(System.Web.UI.Page page, string js)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "js", string.Format("<script type=\"text/javascript\">{0};</script>", js));
        }
       


    }
}
