using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;

namespace ZXChurch.WebApplication.Class
{
    /// <summary>
    /// 系统异常记录
    /// </summary>
    public class ExecptionLoger
    {
        private static object objLock = new object();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="path"></param>
        private static void SaveLog(string content, string path, string fileName)
        {
            lock (objLock)
            {

                string strLogPach =GetMapPath(path);//存放日志文件目录
                if (!Directory.Exists(strLogPach))
                {
                    try
                    {
                        Directory.CreateDirectory(strLogPach);
                    }
                    catch
                    {
                        return;
                    }
                }
                string strfilePach = strLogPach + "/" + fileName;
                if (File.Exists(strfilePach))
                {
                    FileInfo file = new FileInfo(strfilePach);

                }
                WriteFile(strfilePach, content);
            }
        }

        static private void WriteFile(string filePach, string content)
        {
            WriteFile(filePach, content, "utf-8");
        }

        static private void WriteFile(string filePach, string content, string encode)
        {
            try
            {
                StreamWriter MyWriter = new StreamWriter(filePach, true, Encoding.GetEncoding(encode));
                MyWriter.Write(content);
                MyWriter.Close();
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// 保存异常
        /// </summary>
        /// <param name="e"></param>
        static public void SaveException(Exception e)
        {
            StringBuilder sbError = new StringBuilder(100);
            sbError.Append("\r\n===============错误出显时间：" + DateTime.Now + "===============\r\n");
            if (System.Web.HttpContext.Current != null)
            {
                sbError.Append("\r\nIP:" + System.Web.HttpContext.Current.Request.ServerVariables["Remote_Addr"]);
                sbError.Append("\r\nCookies--------------begin:");
                foreach (string skey in HttpContext.Current.Request.Cookies.Keys)
                {
                    sbError.Append("\n" + skey + "==" + HttpContext.Current.Request.Cookies[skey].Values);
                }
                sbError.Append("\r\nCookies--------------end:");          
                sbError.Append("\r\nForm:" + HttpContext.Current.Request.Form);
                sbError.AppendFormat("\r\nUrlReferrer:{0}\r\n Error URL:{1}", HttpContext.Current.Request.UrlReferrer, HttpContext.Current.Request.Url);
            }
            sbError.AppendFormat("\r\nError Source:{2}\r\n Error Message:{0}\r\nStack Trace:\r\n{1}", e.Message, e.StackTrace, e.Source);
            sbError.Append("\r\n========================================================================================\r\n");

            SaveLog(sbError.ToString(), "/error-log/", DateTime.Now.ToString("yyyyMMdd") + ".log");
        }
        /// <summary>
        /// 获取物理路径（实现在非WEB引用时列出虚拟目录路径，需要读取根目录IIsWebVirtualDir配置文件）
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string GetMapPath(string strPath)
        {
            if (System.Web.HttpContext.Current != null)
            {
                return System.Web.HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {              
                strPath = strPath.Replace("~", "");
                strPath = strPath.Replace("//", "/");
                if (strPath.StartsWith("/"))
                    strPath = strPath.TrimStart('/');
                strPath = strPath.Replace(@"/", @"\");
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
    }
}