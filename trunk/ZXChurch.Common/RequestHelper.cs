using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace ZXChurch.Common
{
    /// <summary>
    /// 主要用于Reqeust请求处理
    /// </summary>
    public class RequestHelper
    {
        /// <summary>
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string GetUrlReferrer()
        {
            string retVal = null;

            try
            {
                retVal = HttpContext.Current.Request.UrlReferrer.AbsoluteUri.ToString();
            }
            catch { }

            if (retVal == null)
                return "";

            return retVal;

        }

        /// <summary>
        /// 获得当前完整Url地址
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public static string GetCurrentUrl()
        {
            return HttpContext.Current.Request.Url.AbsoluteUri.ToString();
        }

        /// <summary>
        /// 获得当前页面的名称
        /// </summary>
        /// <returns>当前页面的名称，返回小写</returns>
        public static string GetCurrentPageName()
        {
            string[] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
            return urlArr[urlArr.Length - 1].ToLower();
        }

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = String.Empty;

            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            if (null == result || result == String.Empty || !result.IsIP())
            {
                return "0.0.0.0";
            }
            return result;
        }

        

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {

            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace(@"/", @"\").Trim();
                if (strPath.StartsWith(@"\") && strPath.Length > 1)
                {
                    strPath = strPath.Substring(1);
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }

        /// <summary>
        /// Request字符串
        /// </summary>
        /// <param name="strRequestString"></param>
        /// <returns></returns>
        public static string RequestString(string strRequestString)
        {
            if (HttpContext.Current.Request.Params[strRequestString] == null)
                return "";
            return HttpContext.Current.Request.Params[strRequestString];
        }
        /// <summary>
        /// Request一个int值
        /// </summary>
        /// <param name="strRequestString"></param>
        /// <returns></returns>
        public static int RequestInt(string strRequestString)
        {
            return HttpContext.Current.Request[strRequestString].ToInt();
        }

        /// <summary>
        /// Request一个int值
        /// </summary>
        /// <param name="strRequestString"></param>
        /// <returns></returns>
        public static int RequestInt(string strRequestString, int defaultvalue)
        {
            if (HttpContext.Current.Request[strRequestString].IsEmpty())
            {
                return defaultvalue;
            }
            return HttpContext.Current.Request[strRequestString].ToInt();
        }

        /// <summary>
        /// Request一个Double值
        /// </summary>
        /// <param name="strRequestString"></param>
        /// <returns></returns>
        public static Double RequestDouble(string strRequestString)
        {
            return HttpContext.Current.Request[strRequestString].ToDouble();
        }

        /// <summary>
        /// Request一个Double值
        /// </summary>
        /// <param name="strRequestString"></param>
        /// <returns></returns>
        public static Double RequestDouble(string strRequestString, double defaultvalue)
        {
            if (HttpContext.Current.Request[strRequestString].IsEmpty())
            {
                return defaultvalue;
            }
            return HttpContext.Current.Request[strRequestString].ToDouble();
        }
    }
}
