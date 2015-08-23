using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web;
using System.Net;

namespace ZXChurch.Common
{
    public class Util
    {
        /// <summary>
        /// 获得随即验证码
        /// </summary>
        /// <param name="intSize"></param>
        /// <returns></returns>
        static public string GetRandomCode(int intSize)
        {
            string strRtn = "";

            Random Rnd = new Random();

            char[] ocdChars = "23456789ABCDEFHKMNPRSTWXYZ".ToCharArray();
            for (int i = 0; i < intSize; i++)
            {
                strRtn += ocdChars[Rnd.Next(0, ocdChars.Length)].ToString();
            }

            return strRtn;
        }
        /// <summary>
        /// 获取指定位数的随机数。
        /// </summary>
        /// <param name="RndNumCount">随机数位数。</param>
        /// <returns></returns>
        public static string GetRandomStr(int RndNumCount)
        {
            string RandomStr;
            RandomStr = "";
            Random Rnd = new Random();
            for (int i = 1; i <= RndNumCount; i++)
            {
                RandomStr += Rnd.Next(0, 9).ToString();
            }
            return RandomStr;
        }

        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="strCode">验证码内容</param>
        /// <param name="intLength">文字长度</param>
        /// <param name="intHeight">验证码区域高度</param>
        /// <returns></returns>
        public static MemoryStream IdentifyImg(string strCode, int intLength, int intHeight)
        {
            string strCodeWork = strCode;
            Bitmap bitMapImage = new System.Drawing.Bitmap((int)(intHeight * 0.86) * intLength, intHeight);
            Graphics graphicImage = Graphics.FromImage(bitMapImage);
            graphicImage.Clear(System.Drawing.Color.White);
            ///设置画笔的输出模式
            graphicImage.SmoothingMode = SmoothingMode.HighSpeed;
            LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, bitMapImage.Width, bitMapImage.Height), Color.Blue, Color.DarkRed, 0, true);

            Char[] arrChar = strCodeWork.ToCharArray();
            ///添加文本字符串            
            for (int i = 0; i < arrChar.Length; i++)
                graphicImage.DrawString(arrChar[i].ToString(), new Font(System.Drawing.FontFamily.GenericSerif, intHeight * 0.6f, FontStyle.Bold | FontStyle.Italic), brush, new Point(i * (int)(intHeight * 0.82), 0));
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(bitMapImage.Width);
                int y = random.Next(bitMapImage.Height);
                bitMapImage.SetPixel(x, y, Color.FromArgb(random.Next()));
            }
            //画图片的边框线
            graphicImage.DrawRectangle(new Pen(Color.Silver), 0, 0, bitMapImage.Width - 1, bitMapImage.Height - 1);
            MemoryStream stream = new MemoryStream();
            bitMapImage.Save(stream, ImageFormat.Jpeg);
            ///释放占用的资源
            graphicImage.Dispose();
            bitMapImage.Dispose();
            return stream;
        }
        

        //第二种验证码格式，带颜色干扰噪音。
        public static MemoryStream IdentifyImg(string strCode)
        {

            int intFontWidth = 16;//单个字体的宽度范围
            int intFontHeight = 22;//单个字体的高度范围            
            Font oftFont = new Font("Arial", 11);//字体


            int intImageWidth = strCode.Length * intFontWidth;
            int intImageHeight = intFontHeight;

            Random newRandom = new Random();
            Bitmap image = new Bitmap(intImageWidth, intImageHeight);
            Graphics g = Graphics.FromImage(image);
            //生成随机生成器
            Random random = new Random();
            //白色背景
            g.Clear(Color.White);
            //画图片的背景噪音线
            for (int i = 0; i < 10; i++)
            {
                int x1 = random.Next(image.Width);
                int x2 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                int y2 = random.Next(image.Height);

                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }
            //画图片的前景噪音点
            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);

                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }
            //灰色边框
            g.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, intImageWidth - 1, (intImageHeight - 1));
            for (int intIndex = 0; intIndex < strCode.Length; intIndex++)
            {
                string strChar = strCode.Substring(intIndex, 1);
                Brush newBrush = new SolidBrush(GetRandomColor());
                Point thePos = new Point(intIndex * intFontWidth + 1 + newRandom.Next(3), 1 + newRandom.Next(3));
                g.DrawString(strChar, oftFont, newBrush, thePos);
            }
            //将生成的图片发回客户端
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            g.Dispose();
            image.Dispose();

            return ms;

        }

        //获得随机颜色
        public static Color GetRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);
            //为了在白色背景上显示，尽量生成深色
            int int_Red = RandomNum_First.Next(256);
            int int_Green = RandomNum_Sencond.Next(256);
            int int_Blue = (int_Red + int_Green > 400) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;
            return Color.FromArgb(int_Red, int_Green, int_Blue);
        }
        private const string UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.3) Gecko/20070309 Firefox/2.0.0.3";
        /// <summary>
        ///  post一个url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string PostData(string url, string data)
        {
            // Convert to bytes

            string strReturn = "";

            try
            {
                byte[] obtPostData = Encoding.UTF8.GetBytes(data);
                HttpWebRequest orqRequest = (HttpWebRequest)WebRequest.Create(url);
                orqRequest.Timeout = 3000;

                orqRequest.Method = "POST";
                orqRequest.UserAgent = UserAgent;
                //orqRequest.Referer = LoginRefererUrl;
                orqRequest.ContentType = "application/x-www-form-urlencoded";
                orqRequest.ContentLength = obtPostData.Length;
                orqRequest.AllowAutoRedirect = false;

                // Add post data to request

                Stream stream;
                using (stream = orqRequest.GetRequestStream())
                {
                    stream.Write(obtPostData, 0, obtPostData.Length);
                }

                HttpWebResponse orsResponse = (HttpWebResponse)orqRequest.GetResponse();

                using (Stream responseStream = orsResponse.GetResponseStream())
                {
                    using (StreamReader streamRead = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        strReturn = streamRead.ReadToEnd();
                    }
                }

                //cookies.Add(loginResponse.Cookies);
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
            }

            return strReturn;
        }
    }
}
