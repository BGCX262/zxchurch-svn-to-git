using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Data.OleDb;
using ZXChurch.Admin;
using ZXChurch.Common;
using System.Data;

namespace ZXChurch.Weekly
{
    /// <summary>
    /// WebHandler 月报数据处理类
    /// </summary>
    public class WebHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write(System.Configuration.ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            //Check(context);
            string action = RequestHelper.RequestString("action").ToLower();
            switch (action)
            {
                //case "save": SaveDBData(context); break;
                case "delete": DeleteDBData(context); break;
                case "checkcode":  DrawImage(context, SessionKeys.AdminLoginCheckCodeSession, 4); break;
                case "logout": SaveLog("退出系统管理"); context.Session.Abandon(); context.Response.Redirect(context.Request.UrlReferrer.ToString()); break;
                case "read-list": //前台显示数据
                    ReadList(context);
                    break;
                case "read-weekly": ReadWeekly(context); break;
                case "read-weekly-count": ReadWeeklyCount(context); break;
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
        /// 判断管理员是否有此栏目权限
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool Check(HttpContext context)
        {
            if (context.Session[SessionKeys.WeeklyAdminSession] != null) 
                return true;
            else
            {
                return new DB().CheckAdmin();
            }

        }

        //private void SaveDBData(HttpContext context)
        //{
        //    if (Check(context))
        //    {
        //        //保存数据
        //        //[Flag], [URL], [AddTime], [UserName], [Title]
        //        int id = RequestHelper.RequestInt("id",0);
        //        string url = RequestHelper.RequestString("downloadurl");
        //        string title = RequestHelper.RequestString("title");

        //        if (title == "" && url == "")
        //        {
        //            context.Response.Write("{result:'error',msg:'内容不能为空！'}");
        //        }
        //        else
        //        {
        //            if (new DB().SaveWeekly(id, title, url))
        //            {                        
        //                context.Response.Write("{result:'ok',msg:'保存成功！'}");
        //                SaveLog(title+"保存成功");
        //            }
        //            else
        //            {
        //                context.Response.Write("{result:'error',msg:'保存失败！'}");
        //                SaveLog(title + "保存失败");
        //            }
        //        }
               
        //    }
        //    else {
        //        context.Response.Write("{result:'error',msg:'没有权限！'}");
        //        SaveLog("提交的信息错误,没有权限");
        //    }
        //}

        private void DeleteDBData(HttpContext context)
        {
            if (Check(context))
            {
                //保存数据
                //[Flag], [URL], [AddTime], [UserName], [Title]
                int id = RequestHelper.RequestInt("id", 0);
               
                if (id<=0)
                {
                    context.Response.Write("{result:'error',msg:'提交的信息错误！'}");
                    SaveLog("提交的信息错误,信息ID：" + id);
                }
                else
                {
                    if (new DB().DeleteWeekly(id))
                    {
                        context.Response.Write("{result:'ok',msg:'删除成功！'}");
                        SaveLog("删除成功,信息ID：" + id);
                    }
                    else
                    {
                        context.Response.Write("{result:'error',msg:'删除失败！'}");
                        SaveLog("删除失败,信息ID：" + id);
                    }
                }

            }
            else
            {
                context.Response.Write("{result:'error',msg:'没有权限！'}");
                SaveLog("删除失败,没有权限");
            }
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

        private void SaveLog(string Doing)
        {
            AdminManage.SaveWebLog("周报管理", Doing);
        }
        /// <summary>
        /// 前台显示数据 2013-03-15
        /// </summary>
        /// <param name="context"></param>
        private void ReadList(HttpContext context)
        {
            int intPageIndex = RequestHelper.RequestInt("p", 1);
            int intPageSize = RequestHelper.RequestInt("pagesize", 20);
            int count = 0;
            if (intPageIndex <= 0) intPageIndex = 1;
            Newtonsoft.Json.JavaScriptObject jsResult = new Newtonsoft.Json.JavaScriptObject();

            Newtonsoft.Json.JavaScriptObject jsList = new Newtonsoft.Json.JavaScriptObject();

            
            DataSet ds= new DB().GetDataList(intPageIndex, intPageSize, out count);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Newtonsoft.Json.JavaScriptObject jsItem = new Newtonsoft.Json.JavaScriptObject();
                    jsItem.Add("id", dr["ID"]);
                    jsItem.Add("title", dr["Title"]);
                    jsItem.Add("url", dr["URL"]);
                    jsItem.Add("date", Convert.ToDateTime(dr["AddTime"]).ToShortDateString());
                    jsList.Add(i.ToString(), jsItem);
                    i++;
                }
                int pageCount=(count + intPageSize - 1) / intPageSize;
                if(intPageIndex>pageCount){intPageIndex=pageCount;}
                jsResult.Add("list", jsList);
                jsResult.Add("count", count);
                jsResult.Add("page", intPageIndex);
                jsResult.Add("pagecount", pageCount);
                jsResult.Add("msg", "获取数据成功");
                jsResult.Add("result", "1");
            }
            else
            {
                jsResult.Add("msg", "没有数据");
                jsResult.Add("result", "0");
            }
            context.Response.Write(Newtonsoft.Json.JavaScriptConvert.SerializeObject(jsResult));

        }
        /// <summary>
        /// 系统后台管理员操作 2013-03-19
        /// </summary>
        /// <param name="context"></param>
        private void SysAdmin(HttpContext context)
        {
            if (AdminManage.CheckSysAdmin)
            {
                string strDo = RequestHelper.RequestString("do").ToLower();
                int id = RequestHelper.RequestInt("id");
                int userid = RequestHelper.RequestInt("userid");
                string username = RequestHelper.RequestString("username");
                int usertype = RequestHelper.RequestInt("usertype");
                switch (strDo)
                {
                    case "update":
                        #region 更新管理权限
                        if (id > 0)
                        {
                            if (new DB().UpDateUser(id, usertype))
                            {
                                context.Response.Write("{result:1,msg:'保存成功！'}");
                                SaveLog("修改权限"+id+":"+usertype);
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
                    case "save":
                        #region 保存管理员信息
                        if (userid > 0 && username != "")
                        {
                            if (new DB().SaveUser(userid, username, usertype))
                            {
                                context.Response.Write("{result:1,msg:'保存成功！'}");
                                SaveLog("保存管理员信息" +userid+ username + ":" + usertype);
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
                    case "delete":
                        #region 删除管理员信息
                        if (id > 0)
                        {
                            if (new DB().DeleteUser(id))
                            {
                                context.Response.Write("{result:1,msg:'删除成功！'}");
                                SaveLog("删除管理员信息" + id);
                            }
                            else
                            {
                                context.Response.Write("{result:0,msg:'删除失败！'}");
                            }
                        }
                        else
                        {
                            context.Response.Write("{result:-1,msg:'参数错误！'}");
                        }
                        #endregion
                        break;
                    case "list":
                        #region 管理员信息
                        int intPageIndex = RequestHelper.RequestInt("p", 1);
                        int intPageSize = RequestHelper.RequestInt("pagesize", 20);
                        int count = 0;
                        if (intPageIndex <= 0) intPageIndex = 1;
                        Newtonsoft.Json.JavaScriptObject jsResult = new Newtonsoft.Json.JavaScriptObject();

                        Newtonsoft.Json.JavaScriptObject jsList = new Newtonsoft.Json.JavaScriptObject();

            
                        DataSet ds= new DB().GetUserList(intPageIndex, intPageSize, out count);
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                Newtonsoft.Json.JavaScriptObject jsItem = new Newtonsoft.Json.JavaScriptObject();
                                jsItem.Add("id", dr["id"]);
                                jsItem.Add("userid", dr["userid"]);
                                jsItem.Add("username", dr["username"]);
                                jsItem.Add("usertype", dr["usertype"]);
                                jsItem.Add("logintime", Convert.ToDateTime(dr["logintime"]).ToShortDateString());
                                jsList.Add(i.ToString(), jsItem);
                                i++;
                            }
                            int pageCount=(count + intPageSize - 1) / intPageSize;
                            if(intPageIndex>pageCount){intPageIndex=pageCount;}
                            jsResult.Add("list", jsList);
                            jsResult.Add("count", count);
                            jsResult.Add("page", intPageIndex);
                            jsResult.Add("pagecount", pageCount);
                            jsResult.Add("msg", "获取数据成功");
                            jsResult.Add("result", "1");
                        }
                        else
                        {
                            jsResult.Add("msg", "没有数据");
                            jsResult.Add("result", "0");
                        }
                        context.Response.Write(Newtonsoft.Json.JavaScriptConvert.SerializeObject(jsResult));
                        #endregion
                        break;
                    default:
                        context.Response.Write("{result:-1,msg:'无效的请求！'}");
                        SaveLog("无效的请求");
                        break;
                }
            }
            else
            {
                context.Response.Write("{result:-1,msg:'没有权限或登陆超时！'}");
            }

        }
        /// <summary>
        /// 前台显示周报 2013-04-10
        /// </summary>
        /// <param name="context"></param>
        private void ReadWeekly(HttpContext context)
        {
            int intId = RequestHelper.RequestInt("id");
            Newtonsoft.Json.JavaScriptObject jsResult = new Newtonsoft.Json.JavaScriptObject();
            if (intId > 0)
            {
                List<string> values = new DB().GetWeeklyInfo(intId);
                if (values.Count >= 3)
                {
                    #region 更新查看次数
                    string cookieName="read-weekly-"+intId;
                    HttpCookie cookie = context.Request.Cookies[cookieName];
                    if (cookie == null) //判断cookie,如果存在就不更新查看次数
                    {
                        if (new DB().UpdateWeeklyFlag(intId))
                        {
                            cookie = new HttpCookie(cookieName);
                            cookie.Expires = DateTime.Now.AddDays(1);
                            cookie.Value = intId.ToString();
                            context.Response.Cookies.Add(cookie);
                        }
                    }
                    #endregion

                    #region 邦定数据
                    Newtonsoft.Json.JavaScriptObject jsItem = new Newtonsoft.Json.JavaScriptObject();
                    jsItem.Add("title",values[1]);
                    jsItem.Add("url",values[2]);
                    jsItem.Add("message", values[3]);
                    jsItem.Add("count", values[4]);
                    jsItem.Add("addtime", values[5]);
                    jsResult.Add("data",jsItem);
                    #endregion

                    jsResult.Add("result", 1);
                    jsResult.Add("msg", "请求成功");
                }
                else
                {
                    jsResult.Add("result","-1");
                    jsResult.Add("msg","请求的信息不存在");
                }
            }
            else
            {
                jsResult.Add("result",0);
                jsResult.Add("msg","错误的参数");
            }
            context.Response.Write(Newtonsoft.Json.JavaScriptConvert.SerializeObject(jsResult));
        }
        /// <summary>
        /// 查看次数
        /// </summary>
        /// <param name="context"></param>
        private void ReadWeeklyCount(HttpContext context)
        {
            int intId = RequestHelper.RequestInt("id");
            Newtonsoft.Json.JavaScriptObject jsResult = new Newtonsoft.Json.JavaScriptObject();
            if (intId > 0)
            {
               
                    int intCount=0;
                    #region 更新查看次数
                    string cookieName = "read-weekly-" + intId;
                    HttpCookie cookie = context.Request.Cookies[cookieName];
                    if (cookie == null) //判断cookie,如果存在就不更新查看次数
                    {
                        intCount = new DB().GetWeeklyCount(intId, true);
                        cookie = new HttpCookie(cookieName);
                        cookie.Expires = DateTime.Now.AddDays(1);
                        cookie.Value = intCount.ToString();
                        context.Response.Cookies.Add(cookie);
                         jsResult.Add("count", intCount);
                    }
                    else
                    {
                        jsResult.Add("count", cookie.Value);
                    }
                    #endregion

                    jsResult.Add("result", 1);
                    jsResult.Add("msg", "请求成功");
               
                
            }
            else
            {
                jsResult.Add("result", 0);
                jsResult.Add("msg", "错误的参数");
            }
            context.Response.Write(Newtonsoft.Json.JavaScriptConvert.SerializeObject(jsResult));
        }
    }
}
