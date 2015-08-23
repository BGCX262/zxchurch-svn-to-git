using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.IO;

namespace ZXChurch.Common
{
    public static class StringExtension
    {
        /// <summary>
        /// 默认的时间值
        /// </summary>
        public static DateTime NullDateTime
        {
            get
            {
                return new DateTime(1900, 1, 1);
            }
        }
        public const int NullInt = 0;
        public const double NullDouble = 0;
        public const decimal NullDecimal = 0;
        public const float NullFloat = 0;
        public const string NullString = "";

        #region 类型转换
        /// <summary>
        /// 转换成Int类型
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static int ToInt(this string oString)
        {
            return oString.ToInt(NullInt);
        }
        /// <summary>
        /// 转换成Int类型
        /// </summary>
        /// <param name="oString"></param>
        /// <param name="intDefault"></param>
        /// <returns></returns>
        public static int ToInt(this string oString, int intDefault)
        {
            int intReturn = 0;
            if (int.TryParse(oString, out intReturn) == false)
            {
                intReturn = intDefault;
            }
            return intReturn;
        }
        /// <summary>
        /// 转换成Double类型
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static Double ToDouble(this string oString)
        {
            return oString.ToDouble(NullDouble);
        }
        /// <summary>
        /// 转换成Double类型
        /// </summary>
        /// <param name="oString"></param>
        /// <param name="intDefault"></param>
        /// <returns></returns>
        public static Double ToDouble(this string oString, Double dbDefault)
        {
            Double dbReturn = 0;
            if (Double.TryParse(oString, out dbReturn) == false)
            {
                dbReturn = dbDefault;
            }
            return dbReturn;
        }
        /// <summary>
        /// 转换成Double类型
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static Decimal ToDecimal(this string oString)
        {
            return oString.ToDecimal(NullDecimal);
        }
        /// <summary>
        /// 转换成Double类型
        /// </summary>
        /// <param name="oString"></param>
        /// <param name="intDefault"></param>
        /// <returns></returns>
        public static Decimal ToDecimal(this string oString, Decimal dbDefault)
        {
            Decimal dbReturn = 0;
            if (Decimal.TryParse(oString, out dbReturn) == false)
            {
                dbReturn = dbDefault;
            }
            return dbReturn;
        }
        /// <summary>
        /// 转换成float类型
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static float Tofloat(this string oString)
        {
            return oString.Tofloat(NullFloat);
        }
        /// <summary>
        /// 转换成float类型
        /// </summary>
        /// <param name="oString"></param>
        /// <param name="intDefault"></param>
        /// <returns></returns>
        public static float Tofloat(this string oString, float dbDefault)
        {
            float dbReturn = 0;
            if (float.TryParse(oString, out dbReturn) == false)
            {
                dbReturn = dbDefault;
            }
            return dbReturn;
        }
        /// <summary>
        /// 转换为bool类型
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static bool ToBool(this string oString)
        {
            if (IsEmpty(oString.Trim()))
            {
                return false;
            }
            else
            {
                switch (oString.Trim().Substring(0, 1))
                {
                    case "1":
                    case "y":
                    case "Y":
                    case "t":
                    case "T":
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// 转换成Datetime类型
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string oString)
        {
            return oString.ToDateTime(NullDateTime);
        }
        /// <summary>
        /// 转换成Datetime类型
        /// </summary>
        /// <param name="oString"></param>
        /// <param name="dtDefault">默认的时间值</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string oString, DateTime dtDefault)
        {
            DateTime oDateTime;
            if (DateTime.TryParse(oString, out oDateTime) == false)
            {
                oDateTime = dtDefault;
            }
            return oDateTime;
        }
        #endregion

        #region 字符处理
        /// <summary>
        /// string去除HTML
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static string RemoveHtml(this string oString)
        {
            return Regex.Replace(oString, @"<[^>]*>|\[[^\]]*\]", string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// string去除不安全的HTML代码
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static string RemoveUnsafeHtml(this string oString)
        {
            oString = Regex.Replace(oString, @"(\<|\s+)o([a-z]+\s?=)", "$1$2", RegexOptions.IgnoreCase);
            oString = Regex.Replace(oString, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
            return oString;
        }

        /// <summary>
        /// URL中传输的string进行编码
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static string UrlEncode(this string oString)
        {
            return System.Web.HttpUtility.UrlEncode(oString, System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// URL中传输的已编码的string进行解码
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static string UrlDecode(this string oString)
        {
            return System.Web.HttpUtility.UrlDecode(oString, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 返回 HTML 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string HtmlEncode(this string oString)
        {
            return HttpUtility.HtmlEncode(oString);
        }

        /// <summary>
        /// 返回 HTML 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlDecode(this string oString)
        {
            return HttpUtility.HtmlDecode(oString);
        }

        /// <summary>
        /// 删除字符串尾部的回车/换行/空格
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static string RTrim(this string oString)
        {
            if (oString.Length > 0)
            {
                int index = oString.Length - 1;
                if (oString[index].ToString().Equals(" ") || oString[index].ToString().Equals("\r") || oString[index].ToString().Equals("\n"))
                {
                    return oString.Remove(oString.Length - 1, 1);
                }
            }
            return oString;
        }

        /// <summary>
        /// 删除字符串尾部的指定的一位长度的字符
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static string RTrim(this string oString, string strTrim)
        {
            if (oString.Length > 0)
            {
                if (oString[oString.Length - 1].ToString().Equals(strTrim))
                {
                    return oString.Remove(oString.Length - 1, 1);
                }
            }
            return oString;
        }

        /// <summary>
        ///  替换字符串中对SQL有影响的字符
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static string SQLFilter(this string oString)
        {
            return (oString == null) ? oString : (oString.Replace("'", "''"));
        }
        /// <summary>
        /// MD5加密过后的数据
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static string MD5Normal(this string oString)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(oString, "MD5").ToLower();
        }
        /// <summary>
        /// Return a hashed string using MD5.
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static string MD5Hash(this string oString)
        {
            byte[] bytHash;
            MD5CryptoServiceProvider omcspHashProvider = new MD5CryptoServiceProvider();

            bytHash = omcspHashProvider.ComputeHash(new ASCIIEncoding().GetBytes(oString));

            return Convert.ToBase64String(bytHash);
        }
        /// <summary>
        /// 分割字符串【自定义扩展方法】
        /// </summary>
        public static string[] SplitString(this string oString, string strSplit)
        {
            if (oString.IndexOf(strSplit) < 0)
            {
                string[] tmp = { oString };
                return tmp;
            }
            return Regex.Split(oString, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 删除最后一个字符
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static string ClearLastChar(this string oString)
        {
            if (oString.IsEmpty())
                return oString;
            else
                return oString.Substring(0, oString.Length - 1);
        }
        /// <summary>
        /// 转换为JSON数据格式
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static string GetJSONStr(this string oString)
        {
            return oString.Replace("\\", "\\\\")
                          .Replace("/", "\\/")
                          .Replace("\"", "\\\"");
        }
        /// <summary>
        /// 重复某一字符串N次
        /// </summary>
        /// <param name="oString"></param>
        /// <param name="iRepeat"></param>
        /// <returns></returns>
        public static string Repeat(this string oString, int iRepeat)
        {
            if (iRepeat <= 0)
                return string.Empty;
            else
            {
                string strReturn = string.Empty;
                for (int i = 0; i < iRepeat; i++)
                {
                    strReturn += oString;
                }
                return strReturn;
            }
        }
        #endregion

        #region 字符判断
        /// <summary>
        /// 是否是email格式的字符串
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static bool IsEmail(this string oString)
        {
            return Regex.IsMatch(oString, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        ///  是否是URL格式的字符串
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static bool IsURL(this string oString)
        {
            return Regex.IsMatch(oString, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }

        /// <summary>
        ///  是否是IP格式的字符串
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static bool IsIP(this string oString)
        {
            return Regex.IsMatch(oString, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        /// <summary>
        /// 判断字符是否为空
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string oString)
        {
            return string.IsNullOrEmpty(oString);
        }

        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="oString"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string oString)
        {
            if (IsEmpty(oString) == false)
            {
                string str = oString;
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                    {
                        return true;
                    }
                }
            }
            return false;

        }
        #endregion

        #region 通过类调用的方法
        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="oString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串,失败返源串</returns>
        public static string DESDecode(string oString, string decryptKey)
        {
            try
            {
                decryptKey = GetSubString(decryptKey, 8, "");
                decryptKey = decryptKey.PadRight(8, ' ');
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };//默认密钥向量
                byte[] inputByteArray = Convert.FromBase64String(oString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();

                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return "";
            }
        }

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串,失败返回源串</returns>
        public static string DESEncode(string encryptString, string encryptKey)
        {
            encryptKey = GetSubString(encryptKey, 8, "");
            encryptKey = encryptKey.PadRight(8, ' ');
            byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
            byte[] rgbIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };//默认密钥向量
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());

        }

        /// <summary>
        /// 字符串如果操过指定长度则将超出的部分用指定字符串代替
        /// </summary>
        /// <param name="oString">要检查的字符串</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string oString, int p_Length, string p_TailString)
        {
            return GetSubString(oString, 0, p_Length, p_TailString);
        }
        /// <summary>
        /// 取指定长度的字符串
        /// </summary>
        /// <param name="oString">要检查的字符串</param>
        /// <param name="p_StartIndex">起始位置</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string oString, int p_StartIndex, int p_Length, string p_TailString)
        {


            string myResult = oString;

            //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
            if (System.Text.RegularExpressions.Regex.IsMatch(oString, "[\u0800-\u4e00]+") ||
                System.Text.RegularExpressions.Regex.IsMatch(oString, "[\xAC00-\xD7A3]+"))
            {
                //当截取的起始位置超出字段串长度时
                if (p_StartIndex >= oString.Length)
                {
                    return "";
                }
                else
                {
                    return oString.Substring(p_StartIndex,
                                                   ((p_Length + p_StartIndex) > oString.Length) ? (oString.Length - p_StartIndex) : p_Length);
                }
            }


            if (p_Length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(oString);

                //当字符串长度大于起始位置
                if (bsSrcString.Length > p_StartIndex)
                {
                    int p_EndIndex = bsSrcString.Length;

                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (p_StartIndex + p_Length))
                    {
                        p_EndIndex = p_Length + p_StartIndex;
                    }
                    else
                    {   //当不在有效范围内时,只取到字符串的结尾

                        p_Length = bsSrcString.Length - p_StartIndex;
                        p_TailString = "";
                    }



                    int nRealLength = p_Length;
                    int[] anResultFlag = new int[p_Length];
                    byte[] bsResult = null;

                    int nFlag = 0;
                    for (int i = p_StartIndex; i < p_EndIndex; i++)
                    {

                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                            {
                                nFlag = 1;
                            }
                        }
                        else
                        {
                            nFlag = 0;
                        }

                        anResultFlag[i] = nFlag;
                    }

                    if ((bsSrcString[p_EndIndex - 1] > 127) && (anResultFlag[p_Length - 1] == 1))
                    {
                        nRealLength = p_Length + 1;
                    }

                    bsResult = new byte[nRealLength];

                    Array.Copy(bsSrcString, p_StartIndex, bsResult, 0, nRealLength);

                    myResult = Encoding.Default.GetString(bsResult);

                    myResult = myResult + p_TailString;
                }
            }

            return myResult;
        }
        /// <summary>
        /// 截取字符串函数 
        /// <para>注：此方法是按字节截取的。一个汉字占两个字节</para>
        /// </summary>
        /// <param name="oString">要截取的字符串</param>
        /// <param name="p">要截取的字节长度</param>
        /// <returns></returns>
        public static string CutStringWithByte(string oString, int p)
        {
            return CutStringWithByte(oString, p, "");
        }

        /// <summary>
        /// 截取字符串函数 
        /// <para>注：此方法是按字节截取的。一个汉字占两个字节</para>
        /// </summary>
        /// <param name="oString">要截取的字符串</param>
        /// <param name="p">要截取的字节长度</param>
        /// <param name="fillChar">超过指定长度时，追加到返回字符串尾部的字符串</param>
        /// <returns></returns>
        public static string CutStringWithByte(string oString, int p, string fillChar)
        {
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(oString);
            if (mybyte.Length <= p) return oString;

            ASCIIEncoding encode = new ASCIIEncoding();
            byte[] byteArr = encode.GetBytes(oString);

            int tempLen = 0;
            string tempString = string.Empty;
            for (int i = 0, j = byteArr.Length; i < j; i++)
            {
                if ((int)byteArr[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }
                try
                {
                    tempString = string.Concat(tempString, oString.Substring(i, 1));
                }
                catch
                {
                    break;
                }
                if (tempLen >= p)
                    break;
            }

            return tempString + fillChar;
        }
        /// <summary>
        /// 从字符串的指定位置截取指定长度的子字符串
        /// </summary>
        /// <param name="oString">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <param name="length">子字符串的长度</param>
        /// <returns>子字符串</returns>
        public static string CutString(string oString, int startIndex, int length)
        {
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length = length * -1;
                    if (startIndex - length < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        startIndex = startIndex - length;
                    }
                }


                if (startIndex > oString.Length)
                {
                    return "";
                }


            }
            else
            {
                if (length < 0)
                {
                    return "";
                }
                else
                {
                    if (length + startIndex > 0)
                    {
                        length = length + startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        return "";
                    }
                }
            }

            if (oString.Length - startIndex < length)
            {
                length = oString.Length - startIndex;
            }

            return oString.Substring(startIndex, length);
        }

        /// <summary>
        /// 从字符串的指定位置开始截取到字符串结尾的了符串
        /// </summary>
        /// <param name="oString">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <returns>子字符串</returns>
        public static string CutString(string oString, int startIndex)
        {
            return CutString(oString, startIndex, oString.Length);
        }

        /// <summary>
        /// 返回只含有数字和字母的伪GUID
        /// </summary>
        /// <returns></returns>
        public static string GetGuidStrWithNumberAndChar()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        #endregion
    }
}
