using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZXChurch.Common;
using System.Data;

namespace ZXChurch.Admin.News
{
    public class NewsHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.Form["action"] != null)
            {
                switch (context.Request.Form["action"].ToLower())
                {
                    case "read-news-newtop": ReadNewsNewTop(context); break;//最新文章
                    case "read-news-displaytop": ReadNewsDisplayTop(context); break;//点击排行文章
                    case "read-news-list": ReadNewsList(context); break;//文章列表
                    case "read-newstype-list": ReadNewstypeList(context); break;//文章分类
                    case "read-news-view": ReadNewsView(context); break;//文章查看
                    case "read-news-count": ReadNewsCount(context); break;//文章查看次数
                    case "read-news-cid-top": ReadNewsCidTop(context); break;//文章分类置顶
                    case "read-news-index-recom": ReadNewsIndexRecomTop(context); break;//首页推荐
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

        /// <summary>
        ///加载文章添加时间排行 2013-04-13
        /// </summary>
        /// <param name="context"></param>
        private void ReadNewsNewTop(HttpContext context)
        {
            int intTop = RequestHelper.RequestInt("c", 1);
            Newtonsoft.Json.JavaScriptObject jsResult = new Newtonsoft.Json.JavaScriptObject();

            Newtonsoft.Json.JavaScriptObject jsList = new Newtonsoft.Json.JavaScriptObject();


            DataSet ds = new DBNews().WebGetDataListOrderByIDDESC(intTop);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Newtonsoft.Json.JavaScriptObject jsItem = new Newtonsoft.Json.JavaScriptObject();
                    jsItem.Add("id", dr["ID"]);
                    jsItem.Add("title", dr["Title"]);
                    jsItem.Add("addtime", TypeHelper.ToDateTime(dr["addtime"]).ToShortDateString());
                    jsList.Add(i.ToString(), jsItem);
                    i++;
                }
                jsResult.Add("list", jsList);
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
        ///加载文章点击排行 2013-04-13
        /// </summary>
        /// <param name="context"></param>
        private void ReadNewsDisplayTop(HttpContext context)
        {
            int intTop = RequestHelper.RequestInt("c", 1);
            Newtonsoft.Json.JavaScriptObject jsResult = new Newtonsoft.Json.JavaScriptObject();

            Newtonsoft.Json.JavaScriptObject jsList = new Newtonsoft.Json.JavaScriptObject();


            DataSet ds = new DBNews().WebGetDataListOrderByDisplayOrderDESC(intTop);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Newtonsoft.Json.JavaScriptObject jsItem = new Newtonsoft.Json.JavaScriptObject();
                    jsItem.Add("id", dr["ID"]);
                    jsItem.Add("title", dr["Title"]);
                    jsList.Add(i.ToString(), jsItem);
                    i++;
                }
                jsResult.Add("list", jsList);
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
        /// 文章首页列表 2013-04-13
        /// </summary>
        /// <param name="cotext"></param>
        private void ReadNewsList(HttpContext context)
        {
            int intPageIndex = RequestHelper.RequestInt("p", 1);
            int intCid = RequestHelper.RequestInt("cid", 0);
            int intPageSize = RequestHelper.RequestInt("pagesize", 20);
            int count = 0;
            if (intPageIndex <= 0) intPageIndex = 1;
            Newtonsoft.Json.JavaScriptObject jsResult = new Newtonsoft.Json.JavaScriptObject();

            Newtonsoft.Json.JavaScriptObject jsList = new Newtonsoft.Json.JavaScriptObject();


            DataSet ds = new DBNews().WebGetDataListByNews(intPageIndex, intPageSize,intCid, out count);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Newtonsoft.Json.JavaScriptObject jsItem = new Newtonsoft.Json.JavaScriptObject();
                    jsItem.Add("id", dr["ID"]);
                    jsItem.Add("title", dr["Title"]);
                    jsItem.Add("date", Convert.ToDateTime(dr["AddTime"]).ToShortDateString());
                    jsList.Add(i.ToString(), jsItem);
                    i++;
                }
                int pageCount = (count + intPageSize - 1) / intPageSize;
                if (intPageIndex > pageCount) { intPageIndex = pageCount; }
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
        /// 文章分类 2013-04-13
        /// </summary>
        /// <param name="context"></param>
        private void ReadNewstypeList(HttpContext context)
        {           
            Newtonsoft.Json.JavaScriptObject jsResult = new Newtonsoft.Json.JavaScriptObject();
            Newtonsoft.Json.JavaScriptObject jsList = new Newtonsoft.Json.JavaScriptObject();

            DataSet ds = new DBNews().GetDataListByNewsType();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Newtonsoft.Json.JavaScriptObject jsItem = new Newtonsoft.Json.JavaScriptObject();
                    jsItem.Add("cid", dr["ID"]);
                    jsItem.Add("cname", dr["TypeName"]);
                    jsList.Add(i.ToString(), jsItem);
                    i++;
                }
                
                jsResult.Add("list", jsList);
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
        /// 文章查看
        /// </summary>
        /// <param name="context"></param>
        private void ReadNewsView(HttpContext context)
        {
            int intId = RequestHelper.RequestInt("id");
            Newtonsoft.Json.JavaScriptObject jsResult = new Newtonsoft.Json.JavaScriptObject();
            if (intId > 0)
            {
                List<string> values = new DBNews().WebGetNewsId(intId);
                if (values.Count >= 3)
                {
                    #region 更新查看次数
                    string cookieName = "read-news-" + intId;
                    HttpCookie cookie = context.Request.Cookies[cookieName];
                    if (cookie == null) //判断cookie,如果存在就不更新查看次数
                    {
                        if (new DBNews().UpdateNewsDisplayOrder(intId))
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
                    jsItem.Add("title", values[1]);
                    jsItem.Add("message", values[2]);
                    jsItem.Add("addtime", values[3]);
                    jsItem.Add("count", values[4]);
                    jsItem.Add("from", values[5]);
                    jsResult.Add("data", jsItem);
                    #endregion

                    jsResult.Add("result", 1);
                    jsResult.Add("msg", "请求成功");
                }
                else
                {
                    jsResult.Add("result", "-1");
                    jsResult.Add("msg", "请求的信息不存在");
                }
            }
            else
            {
                jsResult.Add("result", 0);
                jsResult.Add("msg", "错误的参数");
            }
            context.Response.Write(Newtonsoft.Json.JavaScriptConvert.SerializeObject(jsResult));
        }
        /// <summary>
        /// 文章查看次数
        /// </summary>
        /// <param name="context"></param>
        private void ReadNewsCount(HttpContext context)
        {
            int intId = RequestHelper.RequestInt("id");
            Newtonsoft.Json.JavaScriptObject jsResult = new Newtonsoft.Json.JavaScriptObject();
            if (intId > 0)
            {

                #region 更新查看次数
                string cookieName = "read-news-" + intId;
                HttpCookie cookie = context.Request.Cookies[cookieName];
                if (cookie == null) //判断cookie,如果存在就不更新查看次数
                {
                    int intCount = new DBNews().GetNewsDisplayOrder(intId, true);
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
            }
            else
            {
                jsResult.Add("result", 0);
                jsResult.Add("msg", "错误的参数");
            }
            context.Response.Write(Newtonsoft.Json.JavaScriptConvert.SerializeObject(jsResult));
        }
        /// <summary>
        /// 文章分类置顶排序
        /// </summary>
        /// <param name="context"></param>
        private void ReadNewsCidTop(HttpContext context)
        {
            int intTop = RequestHelper.RequestInt("c", 1);
            int intCid = RequestHelper.RequestInt("cid", 0);
            Newtonsoft.Json.JavaScriptObject jsResult = new Newtonsoft.Json.JavaScriptObject();

            Newtonsoft.Json.JavaScriptObject jsList = new Newtonsoft.Json.JavaScriptObject();


            DataSet ds = new DBNews().WebGetDataListByCidOrderByTopWeight(intCid, intTop);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Newtonsoft.Json.JavaScriptObject jsItem = new Newtonsoft.Json.JavaScriptObject();
                    jsItem.Add("id", dr["ID"]);
                    jsItem.Add("title", dr["Title"]);
                    jsList.Add(i.ToString(), jsItem);
                    i++;
                }
                jsResult.Add("list", jsList);
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

        private void ReadNewsIndexRecomTop(HttpContext context)
        {
            int intTop = RequestHelper.RequestInt("c", 1);
            int intCid = RequestHelper.RequestInt("cid", 0);
            Newtonsoft.Json.JavaScriptObject jsResult = new Newtonsoft.Json.JavaScriptObject();

            Newtonsoft.Json.JavaScriptObject jsList = new Newtonsoft.Json.JavaScriptObject();


            DataSet ds = new DBNews().WebGetDataListByCidOrderByEilWeigth(intCid, intTop);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Newtonsoft.Json.JavaScriptObject jsItem = new Newtonsoft.Json.JavaScriptObject();
                    jsItem.Add("id", dr["ID"]);
                    jsItem.Add("title", dr["Title"]);
                    jsList.Add(i.ToString(), jsItem);
                    i++;
                }
                jsResult.Add("list", jsList);
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
    }
}
