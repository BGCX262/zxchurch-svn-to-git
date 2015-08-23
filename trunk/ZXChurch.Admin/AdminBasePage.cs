using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXChurch.Common;
using System.Web;

namespace ZXChurch.Admin
{
    /// <summary>
    /// 管理后台基类
    /// </summary>
    public class AdminBasePage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
#if DEBUG
            if (Session[SessionKeys.AdminSession] == null)
            {
                Session[SessionKeys.AdminSession] = "Admin|2|1";
            }
#endif

            #region 判断登陆
            string strPageName = RequestHelper.GetCurrentPageName().ToLower();
           
                if (NeedLogin(strPageName) && CheckNotLogin())
                {

                    Response.Redirect("/zxadmin/login.aspx");
                }
               

                
                switch (strPageName)
                {
                    case "login.aspx"://登陆后访问注册页面和登陆页面时会转向首页
                        if(!CheckNotLogin())
                            Response.Redirect("default.aspx");
                        break;
                    case "logout.aspx"://已经登陆时访问退出页面
                        Session.Abandon();
                        Alert("您已经成功退出！", "login.aspx");
                        break;

                    case "checkcode.aspx": DrawImage(HttpContext.Current, SessionKeys.AdminLoginCheckCodeSession, 4); break;
                    default:
                        if (dicNeedLoginPages.ContainsKey(strPageName) && dicNeedLoginPages[strPageName] == "2" && (AdminManage.UserType == AdminType.NonAdmin || AdminManage.UserType == AdminType.Ordinary))
                        {
                            Response.Redirect("/zxadmin/default.aspx");
                        }
                        break;
                }

                
           
           
            #endregion

            base.OnInit(e);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected bool CheckNotLogin()
        {
            return Session[SessionKeys.AdminSession] == null;
        }

        protected void Alert(string mess)
        {
            PageHelper.Alert(this.Page, mess);
        }

        protected void Alert(string mess, string url)
        {
            PageHelper.Alert(this.Page, mess,url);
        }

        protected void ExecuteJs(string js)
        {
            PageHelper.ExecuteJs(this.Page,js);
        }

        private Dictionary<string, string> dicNeedLoginPages
        {
            get {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("default.aspx", "0");
                dic.Add("welcome.aspx", "0");
                dic.Add("header.aspx","0");
                dic.Add("footer.aspx", "0");
                dic.Add("leftmenu.aspx", "0");
                dic.Add("leftusermenu.aspx","1");

                dic.Add("admin_user_competence.aspx", "2");
                dic.Add("admin_user_competence_select.aspx", "2");
                dic.Add("admin_user_list.aspx", "2");
                dic.Add("admin_user_save.aspx", "2");

                dic.Add("admin_user_resetpwd.aspx", "1");
                

                dic.Add("admin_news_category.aspx", "1");
                dic.Add("admin_news_category_save.aspx", "1");
                dic.Add("admin_news_list.aspx", "1");
                dic.Add("admin_news_save.aspx", "1");
                dic.Add("webhandler.ashx", "1");
                return dic;
            }
        }

        private bool NeedLogin(string pageName)
        {
            return dicNeedLoginPages.ContainsKey(pageName);
        }


        /// <summary>
        /// 输出验证码图片
        /// </summary>
        /// <param name="SessionName">Session名称</param>
        /// <param name="length">字符长度</param>
        private void DrawImage(HttpContext context, string SessionName, int length)
        {
            context.Response.ClearContent();
            context.Response.Expires = -1;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.CacheControl = "no-cache";

            string strRandom = Util.GetRandomCode(length);
            context.Session[SessionName] = strRandom;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            ms = Util.IdentifyImg(strRandom, length, 20);
            context.Response.Clear();
            context.Response.ContentType = "image/jpeg";

            context.Response.BinaryWrite(ms.ToArray());
            context.Response.End();
        }



    }


}
