using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZXChurch.Common
{
    /// <summary>
    /// 类型转换操作类
    /// </summary>
    public class TypeHelper
    {
        /// <summary>
        /// 转为Int
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch {
                return 0;
            }
        }
         /// <summary>
         /// 转为Int
         /// </summary>
         /// <param name="obj"></param>
         /// <param name="defaultVal"></param>
         /// <returns></returns>
        public static int ToInt(object obj,int defaultVal)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return defaultVal;
            }
        }
        /// <summary>
        /// 转为时间
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(object obj)
        {
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch {
                return Convert.ToDateTime("1900-01-01");
            }
        }
    }
}
