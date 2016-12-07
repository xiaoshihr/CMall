using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Web.Security;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Xml;
using System.Xml.Xsl;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Runtime.Serialization.Formatters.Binary;


namespace qiaojiaren
{
    public class CommonMethod
    {

        #region 窗体提示 void Alert(string str)
        /// <summary>
        /// 窗体提示
        /// </summary>
        /// <param name="str">提示内容</param>
        public static void Alert(string str)
        {
            HttpContext.Current.Response.Write("<script>alert('" + str + "')</script>");
        }
        /// <summary>
        /// 窗体提示并做跳转
        /// </summary>
        /// <param name="str">提示语</param>
        /// <param name="url">跳转页面URL</param>
        public static void Alert(string str, string url)
        {
            HttpContext.Current.Response.Write("<script>alert('" + str + "'); window.location.href='" + url + "';</script>");
        }
        #endregion

        #region 截取字符长度 static string CutString(string str, int len)
        /// <summary>
        /// 截取字符长度
        /// </summary>
        /// <param name="str">被截取的字符串</param>
        /// <param name="len">所截取的长度</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int len)
        {
            if (str == null || str.Length == 0 || len <= 0)
            {
                return string.Empty;
            }

            int l = str.Length;


            #region 计算长度
            int clen = 0;
            while (clen < len && clen < l)
            {
                //每遇到一个中文，则将目标长度减一。
                if ((int)str[clen] > 128) { len--; }
                clen++;
            }
            #endregion

            if (clen < l)
            {
                return str.Substring(0, clen) + "...";
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// //截取字符串中文 字母
        /// </summary>
        /// <param name="content">源字符串</param>
        /// <param name="length">截取长度！</param>
        /// <returns></returns>
        public static string SubTrueString(object content, int length)
        {
            string strContent = NoHTML(content.ToString());

            bool isConvert = false;
            int splitLength = 0;
            int currLength = 0;
            int code = 0;
            int chfrom = Convert.ToInt32("4e00", 16);    //范围（0x4e00～0x9fff）转换成int（chfrom～chend）
            int chend = Convert.ToInt32("9fff", 16);
            for (int i = 0; i < strContent.Length; i++)
            {
                code = Char.ConvertToUtf32(strContent, i);
                if (code >= chfrom && code <= chend)
                {
                    currLength += 2; //中文
                }
                else
                {
                    currLength += 1;//非中文
                }
                splitLength = i + 1;
                if (currLength >= length)
                {
                    isConvert = true;
                    break;
                }
            }
            if (isConvert)
            {
                return strContent.Substring(0, splitLength);
            }
            else
            {
                return strContent;
            }
        }
        #endregion

        #region 重载窗口 - static void ReloadWin()
        /// <summary>
        /// 重载窗口
        /// </summary>
        public static void ReloadWin()
        {
            HttpContext.Current.Response.Write("<script>location.href=location.href;</script>");
        }
        #endregion

        #region /*产生验证码*/ GetCode(int codeLength)

        /// <summary>
        /// 生成一个1到10000000之间的正整数
        /// </summary>
        /// <returns></returns>
        public static int GetNums()
        {
            int a = new Random().Next(1, 100000000);
            return a;
        }
        /// <summary>
        /// 产生验证码
        /// </summary>
        /// <param name="codeLength">获取的验证码长度</param>
        /// <returns>验证码</returns>
        public static string GetCode(int codeLength)
        {

            string so = "1,2,3,4,5,6,7,8,9,0,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] strArr = so.Split(',');
            string code = "";
            Random rand = new Random();
            for (int i = 0; i < codeLength; i++)
            {
                code += strArr[rand.Next(0, strArr.Length)];
            }
            return code;
        }

        /// <summary>
        /// 获取一个随机字符串
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GetRandomChar(int count)
        {
            string[] s = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                           "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            StringBuilder sb = new StringBuilder();
            Random ran = new Random();
            for (int i = 0; i < count; i++)
            {
                int temp = ran.Next(s.Length);
                sb.Append(s[temp]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取特定位数的随机数
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GetRandomNums(int count)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('9', count - 1);
            int min = int.Parse(sb.ToString()) + 1;//最小值
            sb.Append(9);
            int max = int.Parse(sb.ToString());//最大值



            Random ran = new Random();
            return ran.Next(min, max).ToString();
        }

        /// <summary>
        /// //获取18位订单编号
        /// </summary>
        /// <returns></returns>
        public static string GetOrderNum()
        {
            Random ran = new Random();
            int random = ran.Next(9999);//四位随机数
            string dateStr = DateTime.Now.ToString("yyyyMMddhhmmss");
            return "order" + dateStr + CovertIntToString(random, 4);
        }

        /// <summary>
        /// 获取支付编号
        /// </summary>
        /// <returns></returns>
        public static string GetPayNum()
        {
            Random ran = new Random();
            int random1 = ran.Next(9999);//四位随机数
            string dateStr = DateTime.Now.ToString("yyyyMMddhhmmss");
            int random2 = ran.Next(9999);//四位随机数
            return CovertIntToString(random1, 4) + dateStr + CovertIntToString(random2, 4);
        }
        public static string GetShopNum()
        {
            Random ran = new Random();
            int random = ran.Next(9999);//四位随机数
            string dateStr = DateTime.Now.ToString("yyyyMMddhhmmss");
            return "shop-" + dateStr + CovertIntToString(random, 4);
        }
        /// <summary>
        /// //获取17位活动编号
        /// </summary>
        /// <returns></returns>
        public static string GetHuoDongNum()
        {
            Random ran = new Random();
            int random = ran.Next(9999);//四位随机数
            string dateStr = DateTime.Now.ToString("yyyyMMddhhmmss");
            return "ACT" + dateStr + CovertIntToString(random, 4);
        }
        /// <summary>
        ///  //获取购物车产品流水号
        /// </summary>
        /// <returns></returns>
        public static string GetCartNum()
        {
            Random ran = new Random();
            int random = ran.Next(9999);//四位随机数
            string dateStr = DateTime.Now.ToString("yyyyMMddhhmmss");
            return "ddpcart" + dateStr + CovertIntToString(random, 4);
        }


        /// <summary>
        ///  //获取会员唯一编号
        /// </summary>
        /// <returns></returns>
        public static string GetVIPNum()
        {
            Random ran = new Random();
            int random = ran.Next(9999);//四位随机数
            string dateStr = DateTime.Now.ToString("yyyyMMddhhmmss");
            return "VIP" + dateStr + CovertIntToString(random, 6);
        }

        /// <summary>
        /// 长度不够补0
        /// </summary>
        /// <param name="src"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string CovertIntToString(object src, int num)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('0', num);
            string sTarget = sb.ToString() + src.ToString();

            return sTarget.Substring(sTarget.Length - num, num);
        }

        private static double EARTH_RADIUS = 6378137.0;
        /// <summary>
        /// 获取地球两点之间的距离
        /// </summary>
        /// <param name="lat_a"></param>
        /// <param name="lng_a"></param>
        /// <param name="lat_b"></param>
        /// <param name="lng_b"></param>
        /// <returns></returns>
        public static double Gps2m(double lat_a, double lng_a, double lat_b, double lng_b)
        {

            double radLat1 = (lat_a * Math.PI / 180.0);
            double radLat2 = (lat_b * Math.PI / 180.0);
            double a = radLat1 - radLat2;
            double b = (lng_a - lng_b) * Math.PI / 180.0;
            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2)
                   + Math.Cos(radLat1) * Math.Cos(radLat2)
                   * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round(s * 10000) / 10000;
            return s;
        }
        #endregion

        #region 文件上传 单文件上传 多文件上传 UpFile()
      
        /// <summary>
        /// 上传单个文件
        /// </summary>
        /// <param name="filePth">文件存储路径  例如：/images/userImg/</param>
        /// <param name="typ">文件类型数组</param>
        /// <returns>文件虚拟路径</returns>
        public static string UpFile(string filePth, string[] typ)
        {
            string fileName = "";
            HttpPostedFile postedFile = null;
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                postedFile = HttpContext.Current.Request.Files[0];
                string[] strs = postedFile.FileName.Split('.');//获取文件扩展名
                if (postedFile.ContentLength > 1000 * 1000)
                {
                    fileName = "big.0";//索引为0的文件过大！
                }
                else
                {
                    bool bol = false;
                    for (int i = 0; i < typ.Length; i++)
                    {
                        if (typ[i] == strs[1])
                        {
                            bol = true;
                        }
                    }
                    if (bol)
                    {
                        fileName += DateTime.Now.ToString("yyyyMMddhhmmssffff") + GetCode(4) + "." + strs[1];//文件名
                        fileName = filePth + fileName;//文件路径
                        string pth = HttpContext.Current.Server.MapPath(fileName);
                        postedFile.SaveAs(pth);
                    }
                    else
                    {
                        fileName = "typ";
                    }
                }
            }
            return fileName;
        }

        /// <summary>
        /// 一次性上传多个文件
        /// </summary>
        /// <param name="filePth">文件路径</param>
        /// <returns>返回数组中的第一个元素如果包含big  则代表图片过大</returns>
        public static string[] UpMoreFile(string filePth)
        {
            string fileName = "";
            List<string> list = new List<string>();
            HttpPostedFile postedFile = null;
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                HttpFileCollection hfc = HttpContext.Current.Request.Files;
                for (int i = 0; i < hfc.Count; i++)
                {
                    postedFile = hfc[i];
                    if (postedFile.ContentLength > 1000 * 1000)
                    {
                        fileName = "big." + i;//索引为 i 的图片过大
                        list.Add(fileName);
                    }
                }
                if (!fileName.Contains("big"))//判断图片大小是不是超出了范围
                {

                    for (int i = 0; i < hfc.Count; i++)
                    {
                        postedFile = hfc[i];
                        if (postedFile.FileName != "")
                        {
                            string[] strs = postedFile.FileName.Split('.');//获取文件扩展名
                            fileName += DateTime.Now.ToString("yyyyMMddhhmmssffff") + GetCode(4) + "." + strs[1];//文件名
                            fileName = filePth + fileName;//文件路径
                            string pth = HttpContext.Current.Server.MapPath(fileName);
                            postedFile.SaveAs(pth);
                            list.Add(fileName);
                            fileName = "";
                        }
                    }
                }
            }
            return list.ToArray();
        }

        /// <summary>
/// 生成缩略图
/// </summary>
/// <param name="originalImagePath">源图路径（物理路径）</param>
/// <param name="thumbnailPath">缩略图路径（物理路径）</param>
/// <param name="width">缩略图宽度</param>
/// <param name="height">缩略图高度</param>
/// <param name="mode">生成缩略图的方式--"W"://指定宽，高按比例、"H"://指定高，宽按比例、"HW"://指定高宽,缩放于指定的范围内</param>
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            try
            {
                Image originalImage = Image.FromFile(originalImagePath);
                int towidth = width;
                int toheight = height;
                int x = 0;
                int y = 0;
                int ow = originalImage.Width;
                int oh = originalImage.Height;
                switch (mode)
                {
                    case "HW"://指定高宽缩放（可能变形：2006-5-27樊海斌修改：使显示图片不变形。）
                        if (originalImage.Height * 1.0 / originalImage.Width > 1.0)
                        {
                            towidth = originalImage.Width * width / originalImage.Height;
                        }
                        else
                        {
                            toheight = originalImage.Height * height / originalImage.Width;
                        }
                        break;
                    case "W"://指定宽，高按比例
                        toheight = originalImage.Height * width / originalImage.Width;
                        break;
                    case "H"://指定高，宽按比例
                        towidth = originalImage.Width * height / originalImage.Height;
                        break;
                    case "Cut"://指定高宽裁减（不变形）
                        if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                        {
                            oh = originalImage.Height;
                            ow = originalImage.Height * towidth / toheight;
                            y = 0;
                            x = (originalImage.Width - ow) / 2;
                        }
                        else
                        {
                            ow = originalImage.Width;
                            oh = originalImage.Width * height / towidth;
                            x = 0;
                            y = (originalImage.Height - oh) / 2;
                        }
                        break;
                    default:
                        break;
                }
                //新建一个bmp图片
                Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
                //新建一个画板
                Graphics g = System.Drawing.Graphics.FromImage(bitmap);
                //设置高质量插值法
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //设置高质量,低速度呈现平滑程度
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //清空画布并以透明背景色填充
                g.Clear(Color.Transparent);

                //=========用于旋转图片======樊海斌=======
                //Matrix myMatrix = new Matrix();
                //PointF pointF00 = new PointF(0,0); 
                //myMatrix.Rotate(30.6f);
                //myMatrix.RotateAt(30.6f,pointF00);   //按指定点旋转
                //g.Transform=myMatrix;
                //=========================================
                //在指定位置并且按指定大小绘制原图片的指定部分
                g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight), new Rectangle(x, y, ow, oh), GraphicsUnit.Pixel);
                try
                {
                    //以jpg格式保存缩略图
                    bitmap.Save(thumbnailPath, originalImage.RawFormat);
                }
                catch (System.Exception e)
                {
                    throw e;
                }
                finally
                {
                    originalImage.Dispose();
                    bitmap.Dispose();
                    g.Dispose();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
           
        }



        /// <summary>      
        /// 为图片生成缩略图        
        /// </summary>      
        /// <param name="phyPath">原图片的路径</param>      
        /// <param name="width">缩略图宽</param>     
        /// 
        /// <param name="height">缩略图高</param>      
        /// <returns></returns>   
        public static System.Drawing.Image GetThumbnail(System.Drawing.Image image, int width, int height)      
        {         
            Bitmap bmp = new Bitmap(width, height);          //从Bitmap创建一个System.Drawing.Graphics          
            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);          //设置           
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;          //下面这个也设成高质量          
            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;          //下面这个设成High          
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;          //把原始图像绘制成上面所设置宽高的缩小图          
            System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, width, height);               
            gr.DrawImage(image, rectDestination, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);          
            return bmp;      
        }
        #endregion

        #region  返回查询的编码的地址栏的字符串 GetUrlEncoding()
        /// <summary>
        /// 返回查询的编码的地址栏字符串
        /// </summary>
        /// <param name="dicField">传入一个字典，键为要查询的字段,值为字段的类型</param>
        /// <param name="arrStr">传入一个数组，数组元素为从客户端接收过来的值</param>
        /// <returns>返回sql字符串</returns>
        public static string GetUrlEncoding(Dictionary<string, string> dic)
        {
            StringBuilder sbsql = new StringBuilder();
            sbsql.Append("");
            foreach (var itm in dic)
            {
                sbsql.AppendFormat("&{0}={1}", itm.Key, itm.Value);
            }
            return sbsql.ToString();
        }
        #endregion

        #region  返回查询的sql字符串 组合查询 GetSqlStr()
        /// <summary>
        /// 返回查询的sql字符串  
        /// </summary>
        /// <param name="dicField">传入一个字典，键为要查询的字段,值为字段的类型</param>
        /// <param name="arrStr">传入一个数组，数组元素为从客户端接收过来的值</param>
        /// <returns>返回sql字符串</returns>
        public static string GetSqlStr(Dictionary<string, string> dic, string[] arr)
        {
            StringBuilder sbsql = new StringBuilder();
            sbsql.Append(" 1=1");
            int i = 0;
            foreach (var itm in dic)
            {
                if (!string.IsNullOrEmpty(arr[i]) && arr[i] != "请选择" && arr[i] != "全部" && arr[i] != "/")
                {
                    if (itm.Value == "nvarchar")
                    {
                        sbsql.AppendFormat(" and {0} like '%{1}%'", itm.Key, arr[i].Trim());

                    }
                    else if (itm.Value == "int")
                    {

                        sbsql.AppendFormat(" and {0} ={1}", itm.Key, int.Parse(arr[i].Trim()));
                    }
                    else if (itm.Value == "datetime")
                    {
                        int arrrows = arr[i].IndexOf("/");
                        string[] str = arr[i].Split('/');
                        if (string.IsNullOrEmpty(str[0]) || string.IsNullOrEmpty(str[1]))
                        {

                            sbsql.AppendFormat(" and CONVERT(varchar(10),{0},20)='{1}'", itm.Key, string.IsNullOrEmpty(str[0]) ? str[1] : str[0]);

                        }
                        if (!string.IsNullOrEmpty(str[0]) && !string.IsNullOrEmpty(str[1]))
                        {
                            string[] text = arr[i].Trim().Split('/');
                            sbsql.AppendFormat(" and CONVERT(varchar(10),{0},20) between '{1}' and '{2}'", itm.Key, text[0], text[1]);

                        }
                    }
                    else if (itm.Value == "text")
                    {
                        // sbsql.AppendFormat(" {0} !=''", itm.Key);
                    }
                }
                i++;
            }
            return sbsql.ToString();
        }
        #endregion

        #region  返回查询的sql字符串 组合查询 GetSqlStr()
        /// <summary>
        /// 返回查询的sql字符串    查找非等值数字类型！  本方法默认为da大于搜索条件
        /// </summary>
        /// <param name="dicField">传入一个字典，键为要查询的字段,值为字段的类型</param>
        /// <param name="arrStr">传入一个数组，数组元素为从客户端接收过来的值</param>
        /// <returns>返回sql字符串</returns>
        public static string GetSqlStrBig_int(Dictionary<string, string> dic, string[] arr)
        {
            StringBuilder sbsql = new StringBuilder();
            sbsql.Append(" 1=1");
            int i = 0;
            foreach (var itm in dic)
            {
                if (!string.IsNullOrEmpty(arr[i]) && arr[i] != "请选择" && arr[i] != "全部" && arr[i] != "/")
                {
                    if (itm.Value == "nvarchar")
                    {
                        sbsql.AppendFormat(" and {0} like '%{1}%'", itm.Key, arr[i].Trim());

                    }
                    else if (itm.Value == "int")
                    {

                        sbsql.AppendFormat(" and {0} >={1}", itm.Key, int.Parse(arr[i].Trim()));
                    }
                    else if (itm.Value == "datetime")
                    {
                        int arrrows = arr[i].IndexOf("/");
                        string[] str = arr[i].Split('/');
                        if (string.IsNullOrEmpty(str[0]) || string.IsNullOrEmpty(str[1]))
                        {

                            sbsql.AppendFormat(" and CONVERT(varchar(10),{0},20)='{1}'", itm.Key, string.IsNullOrEmpty(str[0]) ? str[1] : str[0]);

                        }
                        if (!string.IsNullOrEmpty(str[0]) && !string.IsNullOrEmpty(str[1]))
                        {
                            string[] text = arr[i].Trim().Split('/');
                            sbsql.AppendFormat(" and CONVERT(varchar(10),{0},20) between '{1}' and '{2}'", itm.Key, text[0], text[1]);

                        }
                    }
                    else if (itm.Value == "text")
                    {
                        // sbsql.AppendFormat(" {0} !=''", itm.Key);
                    }
                }
                i++;
            }
            return sbsql.ToString();
        }
        #endregion

        #region 时间处理
        /// <summary>
        /// 用于统计时间段内的天数
        /// </summary>
        /// <param name="DateTime1">时间一</param>
        /// <param name="DateTime2">时间二</param>
        /// <returns></returns>
        public static int DayDiff(DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts.Days;
        }

        /// <summary>
        /// 时间一减去时间二
        /// </summary>
        /// <param name="DateTime1">时间一</param>
        /// <param name="DateTime2">时间二</param>
        /// <returns></returns>
        public static int DiffDay(DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2);
            return ts.Days;
        }

        /// <summary>
        /// 时间格式转换
        /// </summary>
        /// <param name="dt">时间</param>
        /// <returns>特定时间格式</returns>
        public static string GetDateFomatString(DateTime dt)
        {
            return string.Format("{0:yy年MM月dd日HH时mm分}", dt);
        }
        public static string GetDateFomatString()
        {
            return string.Format("{0:yy年MM月dd日HH时mm分}", DateTime.Now);
        }
        public static string GetDateFomatAllString(DateTime dt)
        {
            return string.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);
        }
        public static string GetDateFomatAllString(object dt)
        {
            return string.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);
        }
        public static string GetDateFomatStringSimple(object dt)
        {
            return string.Format("{0:yyyy-MM-dd}", dt);
        }
        #endregion

        #region 检测字符串  字符串处理
        /// <summary>
        /// 过滤非法字符，返回纯净的字符串
        /// </summary>
        /// <param name="sourceString">用户输入的字符串</param>
        /// <returns></returns>
        public static string ValidateString(string sourceString)
        {
            sourceString = sourceString.Replace("'", "，").Replace("%", "，").Replace("--", "——").Replace("''", "‘’").Replace(":", "：");
            return sourceString;
        }


        public static void ValidateString(string sourceString, out string errInfo)
        {
            string[] strArray = new string[] { "'", "’", "‘", "--", "－－", "''", "‘’", ":", "：", "%" };
            string str = "";
            foreach (string str2 in strArray)
            {
                if (sourceString.Contains(str2))
                {
                    str = "您输入的字符格式不正确,含有不合法的字符!";
                    errInfo = str;
                    break;
                }
            }
            errInfo = str;
        }

        /// <summary>
        /// 是否在字符串数组中
        /// </summary>
        /// <param name="target">源字符串</param>
        /// <param name="splitstr">分隔字符</param>
        /// <param name="s">是否存在的子字符串</param>
        /// <returns>如存在  返回 true  否则  返回 false</returns>
        public static bool IsInArrayString(string target, char splitstr, string s)
        {
            string[] ids = target.Split(splitstr);
            for (int i = 0; i < ids.Length; i++)
            {
                if (ids[i] == s)
                    return true;
                
            }
            return false;
        }

        /// <summary>
        /// 字符串是否全部由数字序列组成
        /// </summary>
        /// <param name="str">源字符串!=?数字序列</param>
        /// <param name="splitstr">分隔字符</param>
        /// <returns>是 true  否 false</returns>
        public static bool IsArrayNum(string str, char splitstr)
        {
            string[] ids = str.Split(splitstr);
            for (int i = 0; i < ids.Length; i++)
            {
                try
                {
                    int.Parse(ids[i]);
                }
                catch (Exception)
                {

                    return false;
                }
            }
            return true;
        }

        #endregion

        #region 不可逆加密 MD5  SHA1

        /// <summary>
        /// MD5 16位加密
        /// </summary>
        /// <param name="ConvertString"></param>
        /// <returns></returns>
        public static string GetMd5Str(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }

        /// <summary>
        /// MD5　32位加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UserMd5(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 

                pwd = pwd + s[i].ToString("X");

            }
            return pwd;
        }


        //密码md5加密
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str">明文</param>
        /// <returns>密文</returns>
        public static string Md532(string str)
        {
            string cl = str;
            string md5str = "";
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            for (int i = 0; i < s.Length; i++)
            {
                md5str = md5str + s[i].ToString("x");
            }
            return md5str;
        }
        public static string MD5Encrypt(string CryptText)
        {
            MD5 myMD5 = new MD5CryptoServiceProvider();
            byte[] HashCode;
            HashCode = Encoding.Default.GetBytes(CryptText);
            HashCode = myMD5.ComputeHash(HashCode);
            StringBuilder EnText = new StringBuilder();
            foreach (byte Byte in HashCode)
            {
                EnText.AppendFormat("{0:x2}", Byte);
            }
            return EnText.ToString();
        }

        /// <summary>
        /// SHA1加密算法   不可逆加密算法
        /// </summary>
        /// <param name="souce">源字符串</param>
        /// <returns>返回加密后数据</returns>
        public static string SHA1(string souce)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(souce, "SHA1");
        }

        /// <summary>  
        /// MD5加密字符串  加密不可逆
        /// </summary>  
        /// <param name="source">源字符串</param>  
        /// <returns>加密后的字符串</returns>  
        public static string MD5Sec(string source)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(source, "MD5"); ;
        }

        //MD5不可逆加密  

        //32位加密  

        public static string GetMD5_32(string s, string _input_charset)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        //16位加密  
        public static string GetMd5_16(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        } 
        #endregion

        #region 可逆加密算法 DES
        /// <summary> 
        /// DES加密方法 
        /// </summary> 
        /// <param name="strPlain">明文</param> 
        /// <param name="strDESKey">密钥</param> 
        /// <param name="strDESIV">向量</param> 
        /// <returns>密文</returns> 
        public static string DESEncrypt(string strPlain, string strDESKey, string strDESIV)
        {
            //把密钥转换成字节数组 
            byte[] bytesDESKey = ASCIIEncoding.ASCII.GetBytes(strDESKey);
            //把向量转换成字节数组 
            byte[] bytesDESIV = ASCIIEncoding.ASCII.GetBytes(strDESIV);
            //声明1个新的DES对象 
            DESCryptoServiceProvider desEncrypt = new DESCryptoServiceProvider();
            //开辟一块内存流 
            MemoryStream msEncrypt = new MemoryStream();
            //把内存流对象包装成加密流对象 
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, desEncrypt.CreateEncryptor(bytesDESKey, bytesDESIV), CryptoStreamMode.Write);
            //把加密流对象包装成写入流对象 
            StreamWriter swEncrypt = new StreamWriter(csEncrypt);
            //写入流对象写入明文 
            swEncrypt.WriteLine(strPlain);
            //写入流关闭 
            swEncrypt.Close();
            //加密流关闭 
            csEncrypt.Close();
            //把内存流转换成字节数组，内存流现在已经是密文了 
            byte[] bytesCipher = msEncrypt.ToArray();
            //内存流关闭 
            msEncrypt.Close();
            //把密文字节数组转换为字符串，并返回 
            return UnicodeEncoding.Unicode.GetString(bytesCipher);
        }




        /// <summary> 
        /// DES解密方法 
        /// </summary> 
        /// <param name="strCipher">密文</param> 
        /// <param name="strDESKey">密钥</param> 
        /// <param name="strDESIV">向量</param> 
        /// <returns>明文</returns> 
        public static string DESDecrypt(string strCipher, string strDESKey, string strDESIV)
        {
            //把密钥转换成字节数组 
            byte[] bytesDESKey = ASCIIEncoding.ASCII.GetBytes(strDESKey);
            //把向量转换成字节数组 
            byte[] bytesDESIV = ASCIIEncoding.ASCII.GetBytes(strDESIV);
            //把密文转换成字节数组 
            byte[] bytesCipher = UnicodeEncoding.Unicode.GetBytes(strCipher);
            //声明1个新的DES对象 
            DESCryptoServiceProvider desDecrypt = new DESCryptoServiceProvider();
            //开辟一块内存流，并存放密文字节数组 
            MemoryStream msDecrypt = new MemoryStream(bytesCipher);
            //把内存流对象包装成解密流对象 
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, desDecrypt.CreateDecryptor(bytesDESKey, bytesDESIV), CryptoStreamMode.Read);
            //把解密流对象包装成读出流对象 
            StreamReader srDecrypt = new StreamReader(csDecrypt);
            //明文=读出流的读出内容 
            string strPlainText = srDecrypt.ReadLine();
            //读出流关闭 
            srDecrypt.Close();
            //解密流关闭 
            csDecrypt.Close();
            //内存流关闭 
            msDecrypt.Close();
            //返回明文 
            return strPlainText;
        } 
        #endregion

        #region//验证函数  验证身份证  邮箱  密码格式等等
        /**/
        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsEmail(string source)
        {
            return Regex.IsMatch(source, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", RegexOptions.IgnoreCase);
        }
        public static bool HasEmail(string source)
        {
            return Regex.IsMatch(source, @"[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证密码格式
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static bool ValidatePwd(out string msg, string pwd)
        {
            msg = "";
            if (pwd.Length < 6 || pwd.Length > 12)
            {
                msg = "密码必须6-12位";
                return false;
            }
            else
            {
                if (!IsNormalChar(pwd))
                {
                    msg = "密码格式不正确";
                    return false;
                }
            }
            return true;
        }


        /**/
        /// <summary>
        /// 验证网址
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsUrl(string source)
        {
            return Regex.IsMatch(source, @"^(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?$", RegexOptions.IgnoreCase);
        }
        public static bool HasUrl(string source)
        {
            return Regex.IsMatch(source, @"(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?", RegexOptions.IgnoreCase);
        }

        /**/
        /// <summary>
        /// 验证日期
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsDateTime(string source)
        {
            try
            {
                DateTime time = Convert.ToDateTime(source);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /**/
        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsMobile(string source)
        {
            return Regex.IsMatch(source, @"^1[358]\d{9}$", RegexOptions.IgnoreCase);
        }
        public static bool HasMobile(string source)
        {
            return Regex.IsMatch(source, @"1[358]\d{9}", RegexOptions.IgnoreCase);
        }


        /**/
        /// <summary>
        /// 验证IP
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsIP(string source)
        {
            return Regex.IsMatch(source, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$", RegexOptions.IgnoreCase);
        }
        public static bool HasIP(string source)
        {
            return Regex.IsMatch(source, @"(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])", RegexOptions.IgnoreCase);
        }


        /**/
        /// <summary>
        /// 验证身份证是否有效
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool IsIDCard(string Id)
        {
            if (Id.Length == 18)
            {
                bool check = IsIDCard18(Id);
                return check;
            }
            else if (Id.Length == 15)
            {
                bool check = IsIDCard15(Id);
                return check;
            }
            else
            {
                return false;
            }
        }

        public static bool IsIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }

        public static bool IsIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }


        /**/
        /// <summary>
        /// 是不是Int型的
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsInt(string source)
        {
            if (string.IsNullOrEmpty(source))
                return false;

            Regex regex = new Regex(@"^(-){0,1}\d+$");
            if (regex.Match(source).Success)
            {
                if ((long.Parse(source) > 0x7fffffffL) || (long.Parse(source) < -2147483648L))
                {
                    return false;
                }
                return true;
            }
            return false;
        }


        /**/
        /// <summary>
        /// 看字符串的长度是不是在限定数之间 一个中文为两个字符
        /// </summary>
        /// <param name="source">字符串</param>
        /// <param name="begin">大于等于</param>
        /// <param name="end">小于等于</param>
        /// <returns></returns>
        public static bool IsLengthStr(string source, int begin, int end)
        {
            int length = Regex.Replace(source, @"[^\x00-\xff]", "OK").Length;
            if ((length <= begin) && (length >= end))
            {
                return false;
            }
            return true;
        }


        /**/
        /// <summary>
        /// 是不是中国电话，格式010-85849685
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsTel(string source)
        {
            return Regex.IsMatch(source, @"^\d{3,4}-?\d{6,8}$", RegexOptions.IgnoreCase);
        }


        /**/
        /// <summary>
        /// 邮政编码 6个数字
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsPostCode(string source)
        {
            return Regex.IsMatch(source, @"^\d{6}$", RegexOptions.IgnoreCase);
        }

        /**/
        /// <summary>
        /// 中文
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsChinese(string source)
        {
            return Regex.IsMatch(source, @"^[\u4e00-\u9fa5]+$", RegexOptions.IgnoreCase);
        }
        public static bool hasChinese(string source)
        {
            return Regex.IsMatch(source, @"[\u4e00-\u9fa5]+", RegexOptions.IgnoreCase);
        }


        /**/
        /// <summary>
        /// 验证是不是正常字符 字母，数字，下划线的组合
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNormalChar(string source)
        {
            return Regex.IsMatch(source, @"[\w\d_]+", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否是数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg1
              = new System.Text.RegularExpressions.Regex(@"^[-]?\d+[.]?\d*$");
            return reg1.IsMatch(str);
        }
        #endregion

        #region 数据报表 OlEDB  张贺
        /// <summary>
        /// 用提供的函数，执行SQL命令，返回一个从指定的Excel文件中读取的内容的数据集中的数据表
        /// </summary>
        /// <remarks>
        /// 例如：
        ///  OleDbConnection conn1 = new OleDbConnection(strConn);OleDbDataAdapter da1 = new OleDbDataAdapter(sql, strConn);
        /// </remarks>
        /// <param name="OleDbConnection">OleDbConnection有效的Excel连接字符串</param>
        /// <param name="path">Excel文件的本地物理路径</param>
        /// <returns>System.Data.DataTable  返回数据库数据表</returns>
        public static System.Data.DataTable DataToBase(string path, string sql)// path Excel文件物理路径  sql = "select * from [文体用品$]";
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + "; Extended Properties=Excel 8.0;";
            OleDbConnection conn1 = new OleDbConnection(strConn);
            OleDbDataAdapter da1 = new OleDbDataAdapter(sql, strConn);
            DataSet ds = new DataSet();
            if (conn1.State == ConnectionState.Closed)
            {
                conn1.Open();
            }
            try
            {
                da1.Fill(ds, "tablename");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    dt = ds.Tables[0];
                }
                else
                {
                    dt = null;
                }
            }
            catch
            {
                dt = null;
            }
            finally
            {
                conn1.Close();
            }
            return dt;
        }

        ///<summary>
        ///用于将：从数据库中读取的内容导入到相应的本地Excel文件中
        ///</summary>
        ///<remarks>
        /// string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pth + "; Extended Properties=Excel 8.0;";OleDbConnection conn = new OleDbConnection(strConn);
        ///</remarks>
        ///<param name="dt">dt是指读取数据库返回的数据表</param>
        ///<param name="sql">向Excel文件中插入，即：sql="insert into[Sheet1$](name,sex) values(dt.Rows[i][0],dt.Rows[i][1])"</param>
        ///<param name="pth">pth代表本地Excel路径</param>
        ///<returns>返回布尔值，为真时代表：数据导入Excel文件成功。否则：失败。</returns>
        public static bool dataToExcel(string pth, string sql)
        {
            bool bol = false;
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pth + "; Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                bol = true;
            }
            catch
            {
                bol = false;
            }
            finally
            {
                conn.Close();
            }
            return bol;
        }


        public static string ReplaceSpecialChars(string input)
        {
            input = input.Replace(" ", "_x0020_")
                .Replace("%", "_x0025_")
                .Replace("#", "_x0023_")
                .Replace("&", "_x0026_")
                .Replace("/", "_x002F_");

            return input;
        }

         /// <summary>
        /// 导出的数据源的数据
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="exportFormat">导出文件的格式</param>
        /// <param name="fileName">输出文件名</param>
        /// <param name="encoding">编码</param>
        public static void Export(DataTable dt, ExportFormat exportFormat, string fileName, System.Text.Encoding encoding)
        {
            DataSet dsExport = new DataSet("Export");
            DataTable dtExport = dt.Copy();

            dtExport.TableName = "Values";
            dsExport.Tables.Add(dtExport);

            string[] headers = new string[dtExport.Columns.Count];
            string[] fields = new string[dtExport.Columns.Count];

            for (int i = 0; i < dtExport.Columns.Count; i++)
            {
                headers[i] = dtExport.Columns[i].ColumnName;
                fields[i] = ReplaceSpecialChars(dtExport.Columns[i].ColumnName);
            }

            Export(dsExport, headers, fields, exportFormat, fileName, encoding, null);
        }

        /// <summary>
        /// 导出SmartGridView的数据源的数据
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="columnIndexList">导出的列索引数组</param>
        /// <param name="exportFormat">导出文件的格式</param>
        /// <param name="fileName">输出文件名</param>
        /// <param name="encoding">编码</param>
        public static void Export(DataTable dt, int[] columnIndexList, ExportFormat exportFormat, string fileName, System.Text.Encoding encoding)
        {
            DataSet dsExport = new DataSet("Export");
            DataTable dtExport = dt.Copy();

            dtExport.TableName = "Values";
            dsExport.Tables.Add(dtExport);

            string[] headers = new string[columnIndexList.Length];
            string[] fields = new string[columnIndexList.Length];

            for (int i = 0; i < columnIndexList.Length; i++)
            {
                headers[i] = dtExport.Columns[columnIndexList[i]].ColumnName;
                fields[i] = ReplaceSpecialChars(dtExport.Columns[columnIndexList[i]].ColumnName);
            }

            Export(dsExport, headers, fields, exportFormat, fileName, encoding, null);
        }

        /// <summary>
        /// 导出SmartGridView的数据源的数据
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="columnNameList">导出的列的列名数组</param>
        /// <param name="exportFormat">导出文件的格式</param>
        /// <param name="fileName">输出文件名</param>
        /// <param name="encoding">编码</param>
        public static void Export(DataTable dt, string[] columnNameList, ExportFormat exportFormat, string fileName, Encoding encoding)
        {
            List<int> columnIndexList = new List<int>();
            DataColumnCollection dcc = dt.Columns;

            foreach (string s in columnNameList)
            {
                columnIndexList.Add(GetColumnIndexByColumnName(dcc, s));
            }

            Export(dt, columnIndexList.ToArray(), exportFormat, fileName, encoding);
        }

        /// <summary>
        /// 导出SmartGridView的数据源的数据
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="columnIndexList">导出的列索引数组</param>
        /// <param name="headers">导出的列标题数组</param>
        /// <param name="exportFormat">导出文件的格式</param>
        /// <param name="fileName">输出文件名</param>
        /// <param name="encoding">编码</param>
        public static void Export(DataTable dt, int[] columnIndexList, string[] headers, ExportFormat exportFormat, string fileName, Encoding encoding)
        {
            DataSet dsExport = new DataSet("Export");
            DataTable dtExport = dt.Copy();

            dtExport.TableName = "Values";
            dsExport.Tables.Add(dtExport);

            string[] fields = new string[columnIndexList.Length];

            for (int i = 0; i < columnIndexList.Length; i++)
            {
                fields[i] = ReplaceSpecialChars(dtExport.Columns[columnIndexList[i]].ColumnName);
            }

            Export(dsExport, headers, fields, exportFormat, fileName, encoding, null);
        }

        /// <summary>
        /// 导出SmartGridView的数据源的数据
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="columnIndexList">导出的列索引数组</param>
        /// <param name="headers">导出的列标题数组</param>
        /// <param name="exportFormat">导出文件的格式</param>
        /// <param name="fileName">输出文件名</param>
        /// <param name="encoding">编码</param>
        /// <param name="function">字段名称 / 函数 对</param>
        public static void Export(DataTable dt, int[] columnIndexList, string[] headers,
            ExportFormat exportFormat, string fileName, Encoding encoding, Dictionary<string, string> function)
        {
            DataSet dsExport = new DataSet("Export");
            DataTable dtExport = dt.Copy();

            dtExport.TableName = "Values";
            dsExport.Tables.Add(dtExport);

            string[] fields = new string[columnIndexList.Length];

            for (int i = 0; i < columnIndexList.Length; i++)
            {
                fields[i] = ReplaceSpecialChars(dtExport.Columns[columnIndexList[i]].ColumnName);
            }

            Export(dsExport, headers, fields, exportFormat, fileName, encoding, function);
        }

        /// <summary>
        /// 导出SmartGridView的数据源的数据
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="columnNameList">导出的列的列名数组</param>
        /// <param name="headers">导出的列标题数组</param>
        /// <param name="exportFormat">导出文件的格式</param>
        /// <param name="fileName">输出文件名</param>
        /// <param name="encoding">编码</param>
        public static void Export(DataTable dt, string[] columnNameList, string[] headers, ExportFormat exportFormat, string fileName, Encoding encoding)
        {
            List<int> columnIndexList = new List<int>();
            DataColumnCollection dcc = dt.Columns;

            foreach (string s in columnNameList)
            {
                columnIndexList.Add(GetColumnIndexByColumnName(dcc, s));
            }

            Export(dt, columnIndexList.ToArray(), headers, exportFormat, fileName, encoding);
        }

        /// <summary>
        /// 导出SmartGridView的数据源的数据
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="columnNameList">导出的列的列名数组</param>
        /// <param name="headers">导出的列标题数组</param>
        /// <param name="exportFormat">导出文件的格式</param>
        /// <param name="fileName">输出文件名</param>
        /// <param name="encoding">编码</param>
        /// <param name="function">字段名称 / 函数 对</param>
        public static void Export(DataTable dt, string[] columnNameList, string[] headers,
            ExportFormat exportFormat, string fileName, Encoding encoding, Dictionary<string, string> function)
        {
            List<int> columnIndexList = new List<int>();
            DataColumnCollection dcc = dt.Columns;

            foreach (string s in columnNameList)
            {
                columnIndexList.Add(GetColumnIndexByColumnName(dcc, s));
            }

            Export(dt, columnIndexList.ToArray(), headers, exportFormat, fileName, encoding, function);
        }

        /// <summary>
        /// 导出SmartGridView的数据源的数据
        /// </summary>
        /// <param name="ds">数据源</param>
        /// <param name="headers">导出的表头数组</param>
        /// <param name="fields">导出的字段数组</param>
        /// <param name="exportFormat">导出文件的格式</param>
        /// <param name="fileName">输出文件名</param>
        /// <param name="encoding">编码</param>
        /// <param name="function">字段名称 / 函数 对</param>
        private static void Export(DataSet ds, string[] headers, string[] fields,
            ExportFormat exportFormat, string fileName, Encoding encoding, Dictionary<string, string> function)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = String.Format("text/{0}", exportFormat.ToString().ToLower());
            HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment;filename={0}.{1}", fileName, exportFormat.ToString().ToLower()));
            HttpContext.Current.Response.ContentEncoding = encoding;

            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, encoding);

            CreateStylesheet(writer, headers, fields, exportFormat, function);
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            XmlDataDocument xmlDoc = new XmlDataDocument(ds);
            XslCompiledTransform xslTran = new XslCompiledTransform();
            xslTran.Load(new XmlTextReader(stream));

            System.IO.StringWriter sw = new System.IO.StringWriter();
            xslTran.Transform(xmlDoc, null, sw);

            HttpContext.Current.Response.Write(sw.ToString());
            sw.Close();
            writer.Close();
            stream.Close();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 动态生成XSL，并写入XML流
        /// </summary>
        /// <param name="writer">XML流</param>
        /// <param name="headers">表头数组</param>
        /// <param name="fields">字段数组</param>
        /// <param name="exportFormat">导出文件的格式</param>
        /// <param name="function">字段名称 / 函数 对</param>
        private static void CreateStylesheet(XmlTextWriter writer, string[] headers, string[] fields,
            ExportFormat exportFormat, Dictionary<string, string> function)
        {
            string ns = "http://www.w3.org/1999/XSL/Transform";
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("xsl", "stylesheet", ns);
            writer.WriteAttributeString("version", "1.0");
            writer.WriteStartElement("xsl:output");
            writer.WriteAttributeString("method", "text");
            writer.WriteAttributeString("version", "4.0");
            writer.WriteEndElement();

            // xsl-template
            writer.WriteStartElement("xsl:template");
            writer.WriteAttributeString("match", "/");

            // xsl:value-of for headers
            for (int i = 0; i < headers.Length; i++)
            {
                writer.WriteString("\"");
                writer.WriteStartElement("xsl:value-of");
                writer.WriteAttributeString("select", "'" + headers[i] + "'");
                writer.WriteEndElement(); // xsl:value-of
                writer.WriteString("\"");
                if (i != fields.Length - 1) writer.WriteString((exportFormat == ExportFormat.CSV) ? "," : "	");
            }

            // xsl:for-each
            writer.WriteStartElement("xsl:for-each");
            writer.WriteAttributeString("select", "Export/Values");
            writer.WriteString("\r\n");

            // xsl:value-of for data fields
            for (int i = 0; i < fields.Length; i++)
            {
                writer.WriteString("\"");
                writer.WriteStartElement("xsl:value-of");
                if (function != null && function.ContainsKey(fields[i]))
                {
                    string functionString = "";
                    function.TryGetValue(fields[i], out functionString);
                    writer.WriteAttributeString("select", functionString);
                }
                else
                {
                    writer.WriteAttributeString("select", fields[i]);
                }
                writer.WriteEndElement(); // xsl:value-of
                writer.WriteString("\"");
                if (i != fields.Length - 1) writer.WriteString((exportFormat == ExportFormat.CSV) ? "," : "	");
            }

            writer.WriteEndElement(); // xsl:for-each
            writer.WriteEndElement(); // xsl-template
            writer.WriteEndElement(); // xsl:stylesheet
        }

        /// <summary>
        /// 根据数据列的列名取数据列的列索引
        /// </summary>
        /// <param name="dcc">数据列集合</param>
        /// <param name="columnName">数据列的列名</param>
        /// <returns></returns>
        public static int GetColumnIndexByColumnName(DataColumnCollection dcc, string columnName)
        {
            int result = -1;

            for (int i = 0; i < dcc.Count; i++)
            {
                if (dcc[i].ColumnName.ToLower() == columnName.ToLower())
                {
                    result = i;
                    break;
                }
            }

            return result;
        }
        #endregion

        #region sql注入攻击
        public static string[] words = { "select", "insert", "delete", "count(", "drop table", "update", "truncate", "asc(", "mid(", "char(", "xp_cmdshell", "exec", "master", "net", "and", "or", "where" };

        public static string CheckParam(string Value)
        {
            Value = Value.Replace("'", "");
            Value = Value.Replace(";", "");
            Value = Value.Replace("--", "");
            Value = Value.Replace("/**/", "");
            return Value;
        }
        public static string CheckParamThrow(string Value)
        {
            for (int i = 0; i < words.Length; i++)
            {
                if (Value.IndexOf(words[i], StringComparison.OrdinalIgnoreCase) > 0)
                {
                    string pattern = string.Format(@"[\W]{0}[\W]", words[i]);
                    Regex rx = new Regex(pattern, RegexOptions.IgnoreCase);
                    if (rx.IsMatch(Value))
                        throw new Exception("发现sql注入痕迹!");
                }
            }
            return CheckParam(Value);
        }
        /// <summary>
        /// 查找是否含有非法参数
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool CheckParamBool(string Value)
        {
            for (int i = 0; i < words.Length; i++)
            {
                if (Value.IndexOf(words[i], StringComparison.OrdinalIgnoreCase) > 0)
                    return true;
            }
            return false;
        }
        #endregion

        #region IP地址处理
        /// <summary> 
        /// 取得客户端真实IP。如果有代理则取第一个非内网地址 
        /// by flower.b 
        /// </summary> 
        public static string IPAddress
        {
            get
            {
                string result = String.Empty;

                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (result != null && result != String.Empty)
                {
                    //可能有代理 
                    if (result.IndexOf(".") == -1)    //没有“.”肯定是非IPv4格式 
                        result = null;
                    else
                    {
                        if (result.IndexOf(",") != -1)
                        {
                            //有“,”，估计多个代理。取第一个不是内网的IP。 
                            result = result.Replace(" ", "").Replace("'", "");
                            string[] temparyip = result.Split(",;".ToCharArray());
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                if (IsIPAddress(temparyip[i])
                                    && temparyip[i].Substring(0, 3) != "10."
                                    && temparyip[i].Substring(0, 7) != "192.168"
                                    && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];    //找到不是内网的地址 
                                }
                            }
                        }
                        else if (IsIPAddress(result)) //代理即是IP格式 
                            return result;
                        else
                            result = null;    //代理中的内容 非IP，取IP 
                    }

                }
                string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (null == result || result == String.Empty)
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                if (result == null || result == String.Empty)
                    result = HttpContext.Current.Request.UserHostAddress;
                return result;
            }
        }

        /// <summary>
        /// 判断是否是IP地址格式 0.0.0.0
        /// </summary>
        /// <param name="str1">待判断的IP地址</param>
        /// <returns>true or false</returns>
        private static bool IsIPAddress(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;

            string regformat = @"^d{1,3}[.]d{1,3}[.]d{1,3}[.]d{1,3}$";

            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }
        #endregion

        #region 初始化文件夹
        /// <summary>
        /// 初始化文件夹信息:检查文件夹是否存在,如果文件夹不存在,则建立。 
        /// </summary>
        /// <param name="path">文件夹相对路径</param>
        /// <returns>返回文件夹物理路径</returns>
        public static string CreatePth(string path)
        {
            string physicsPath = HttpContext.Current.Server.MapPath(path);
            if (!Directory.Exists(physicsPath))
            {
                Directory.CreateDirectory(physicsPath);
            }
            return physicsPath;
        }
        #endregion

        #region//电子邮件发送//sendmail()
        /// <summary>
        /// 电子邮件发送  没有附件
        /// </summary>
        /// <param name="frommail">发件人邮箱</param>
        /// <param name="pwd">发件人密码</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件正文</param>
        /// <param name="tomail">收件人邮箱</param>
        /// <param name="typ">指定固定格式的邮件发送</param>
        /// <returns></returns>
        public static bool sendmail(string frommail,string pwd,string subject,string body, string tomail,int typ)
        {
            bool bol = false;
            string das = DateTime.Now.Year.ToString();
            if (true)
            {
                try
                {
                    MailAddress to = new MailAddress(tomail);
                    MailAddress from = new MailAddress(frommail);
                    MailMessage message = new MailMessage(from, to);
                    message.Subject = subject;
                    message.Body = body;
                    message.BodyEncoding = System.Text.Encoding.Default;//正文编码
                    if (typ == 1)
                    {
                        message.IsBodyHtml = true;
                        message.Priority = MailPriority.High;
                    }
                    //
                    SmtpClient smtp = new SmtpClient();
                    smtp.UseDefaultCredentials = true;
                    smtp.Port = 25;
                    string[] name = frommail.Split('@');
                    string names = name[0];
                    smtp.Credentials = new NetworkCredential(names, pwd);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Host = "smtp.163.com";
                    SmtpClient client = new SmtpClient("smtp.163.com", 587);
                    client.EnableSsl = true;
                    message.To.Add(tomail);
                    smtp.Send(message);
                    bol = true;
                }
                catch (Exception ex)
                {
                    bol = false;
                }
            }
            return bol;
        }


        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="smtpserver">SMTP服务器</param>
        /// <param name="userName">登录帐号</param>
        /// <param name="pwd">登录密码</param>
        /// <param name="nickName">发件人昵称</param>
        /// <param name="strfrom">发件人</param>
        /// <param name="strto">收件人</param>
        /// <param name="subj">主题</param>
        /// <param name="bodys">内容</param>
        public static void sendmail(string smtpserver, string userName, string pwd, string nickName, string strfrom, string strto, string subj, string bodys)
        {
            SmtpClient _smtpClient = new SmtpClient();
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            _smtpClient.Host = smtpserver;//指定SMTP服务器
            _smtpClient.Credentials = new System.Net.NetworkCredential(userName, pwd);//用户名和密码

            //MailMessage _mailMessage = new MailMessage(strfrom, strto);
            MailAddress _from = new MailAddress(strfrom, nickName);
            MailAddress _to = new MailAddress(strto);
            MailMessage _mailMessage = new MailMessage(_from, _to);
            _mailMessage.Subject = subj;//主题
            _mailMessage.Body = bodys;//内容
            _mailMessage.BodyEncoding = System.Text.Encoding.Default;//正文编码
            _mailMessage.IsBodyHtml = true;//设置为HTML格式
            _mailMessage.Priority = MailPriority.Normal;//优先级
            _smtpClient.Send(_mailMessage);
        }
        

        /// <summary>
        /// 电子邮件发送  存有附件
        /// </summary>
        /// <param name="frommail">发件人邮箱</param>
        /// <param name="pwd">发件人密码</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件正文</param>
        /// <param name="tomail">收件人邮箱</param>
        /// <param name="ext">附件扩展名</param>
        /// <param name="pth">上传附件路径</param>
        /// <returns>返回发送是否成功</returns>
        public static bool sendmail(string frommail,string pwd,string subject,string body, string tomail,string pth)
        {
            bool bol = false;
            try
            {
                MailAddress to = new MailAddress(tomail);
                MailAddress from = new MailAddress(frommail);
                MailMessage message = new MailMessage(from, to);
                message.Subject = subject;
                message.Body = body;
                if (true)
                {
                    string fileName = "";
                    HttpPostedFile postedFile = null;
                    if (HttpContext.Current.Request.Files.Count > 0)
                    {
                        postedFile = HttpContext.Current.Request.Files[0];
                        string[] strs = postedFile.FileName.Split('.');//获取文件扩展名

                        fileName += DateTime.Now.ToString("yyyyMMddhhmmssffff") + GetCode(4) + "." + strs[1];//文件名
                        fileName = pth + fileName;//文件路径
                        string serverpth = HttpContext.Current.Server.MapPath(fileName);
                        postedFile.SaveAs(serverpth);
                        Attachment ment = new Attachment(serverpth);
                        message.Attachments.Add(ment);
                    }
                }

                //
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = true;
                smtp.Port = 25;
                string[] name = frommail.Split('@');
                string names = name[0];
                smtp.Credentials = new NetworkCredential(names, pwd);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Host = "smtp.163.com";
                SmtpClient client = new SmtpClient("smtp.163.com", 587);
                client.EnableSsl = true;
                message.To.Add(tomail);
                smtp.Send(message);
                bol = true;
            }
            catch (Exception ex)
            {
                bol = false;
            }
            return bol;
        }
        #endregion

        #region 获取一个页面的HTML源代码
        /// <summary>
        /// 获取一个页面的HTML源代码  可以用于电子邮件发送一个页面
        /// </summary>
        /// <param name="url">网址路径</param>
        /// <returns>网址页面源HTML代码</returns>
        public static string GetHtml(string url)
        {      
            WebRequest req = WebRequest.Create(url);
            WebResponse res = req.GetResponse();
            Stream resStream = res.GetResponseStream(); StreamReader sr = new StreamReader(resStream, Encoding.UTF8);
            string contentHTML = sr.ReadToEnd();    //读取网页的源代码      
            resStream.Close();
            sr.Close();
            return contentHTML;
        }
        #endregion

        #region HTML处理
        /// <summary>
        /// 将html标签转化为特殊字符type=0或特殊字符转化为HTML type=1
        /// </summary>
        /// <param name="vv">源字符串</param>
        /// <param name="type">转化方式</param>
        /// <returns></returns>
        public static string HTML_Trans(string vv,int type)
        {
            if (type == 0)
            {
                vv = vv.Replace(" ", "&nbsp;");
                vv = vv.Replace("　", "&nbsp;");
                vv = vv.Replace(">", "&gt;");
                vv = vv.Replace("<", "&lt;");
                vv = vv.Replace("&", "&amp;");
                vv = vv.Replace("\"", "&quot;");
                vv = vv.Replace("'", "&apos");
            }
            if (type == 1)
            {
                vv = vv.Replace("&nbsp;"," ");
                vv = vv.Replace("&nbsp;","　");
                vv = vv.Replace("&gt;",">");
                vv = vv.Replace("&lt;", "<");
                vv = vv.Replace("&amp;","&");
                vv = vv.Replace("&quot;","\"");
                vv = vv.Replace("&apos", "'");
            }
           
            return vv;
        }

        /// <summary>
        /// 去掉非法html标签
        /// </summary>
        /// <param name="Htmlstring"></param>
        /// <returns></returns>
        public static string NoHTML(object html)
        {
            if (html == null)
                return "";
            string Htmlstring = html.ToString();
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            return Htmlstring.Trim();
        }
       
        #endregion

        #region C#图片处理 合并图片
        /// <summary>
        /// 调用此函数后使此两种图片合并，类似相册，有个
        /// 背景图，中间贴自己的目标图片
        /// </summary>
        /// <param name="sourceImg">粘贴的源图片</param>
        /// <param name="destImg">粘贴的目标图片</param>
        /// 使用说明： string pic1Path = Server.MapPath(@"\testImg\wf.png");
        /// 使用说明： string pic2Path = Server.MapPath(@"\testImg\yj.png");
        /// 使用说明： System.Drawing.Image img = CombinImage(pic1Path, pic2Path);
        /// 使用说明：img.Save(Server.MapPath(@"\testImg\Newwf.png"));
        public static System.Drawing.Image CombinImage(string sourceImg, string destImg)
        {
            System.Drawing.Image imgBack = System.Drawing.Image.FromFile(sourceImg);     //相框图片 
            System.Drawing.Image img = System.Drawing.Image.FromFile(destImg);        //照片图片



            //从指定的System.Drawing.Image创建新的System.Drawing.Graphics       
            Graphics g = Graphics.FromImage(imgBack);

            g.DrawImage(imgBack, 0, 0, 148, 124);      // g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);
            g.FillRectangle(System.Drawing.Brushes.Black, 16, 16, (int)112 + 2, ((int)73 + 2));//相片四周刷一层黑色边框



            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);
            g.DrawImage(img, 17, 17, 112, 73);
            GC.Collect();
            return imgBack;
        }
        #endregion

        #region XML
        #region  增

        /// <summary>
        /// 新建一个带有根节点的xml
        /// </summary>
        /// <param name="rootName">根节点名字</param>
        /// <returns></returns>
        public static XmlDocument CreateXml(string rootName)
        {
            XmlDocument xml = new XmlDocument();

            //写入声明
            XmlNode node = xml.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xml.AppendChild(node);

            //增加根节点
            XmlElement rootElement = xml.CreateElement(rootName);
            xml.AppendChild(rootElement);

            return xml;
        }


        /// <summary>
        /// 添加一个父节点到根节点末尾
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="parentNodeName">父节点名字</param>
        /// <returns></returns>
        public static void AddParentNode(XmlDocument xml, string parentNodeName)
        {
            XmlElement parentElement = xml.CreateElement(parentNodeName);
            xml.DocumentElement.AppendChild(parentElement);
        }


        /// <summary>
        /// 添加一个新的子节点(包括属性信息)到指定的节点中
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="currentPath">新节点的父节点xPath路径</param>
        /// <param name="elementName">新节点的名字</param>
        /// <param name="attrib">新节点的属性名字</param>
        /// <param name="attribContent">新节点的属性值</param>
        /// <param name="content">新节点值</param>
        /// <returns></returns>
        public static bool AddElement(XmlDocument xml, string currentNodePath, string elementName
                                             , string attrib, string attribContent, string content)
        {
            bool flag = false;
            XmlNode currentNode = xml.SelectSingleNode(currentNodePath);
            if (currentNode != null)
            {
                XmlElement newElement = xml.CreateElement(elementName);
                newElement.SetAttribute(attrib, attribContent);
                newElement.InnerText = content;
                currentNode.AppendChild(newElement);
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 添加一个新的子节点(不包括属性信息)到指定的节点中
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="currentNodePath">新节点的父节点xPath路径</param>
        /// <param name="elementName">新节点名字</param>
        /// <param name="content">新节点值</param>
        /// <returns></returns>
        public static bool AddElement(XmlDocument xml, string currentNodePath, string elementName
                                            , string content)
        {
            bool flag = false;
            XmlNode currentNode = xml.SelectSingleNode(currentNodePath);
            if (currentNode != null)
            {
                XmlElement newElement = xml.CreateElement(elementName);
                newElement.InnerText = content;
                currentNode.AppendChild(newElement);
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 向某一节点添加属性
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="currentNodePath">属性的节点的xPath</param>
        /// <param name="attributeName">属性名</param>
        /// <param name="attributeValue">属性值</param>
        /// <returns></returns>
        public static bool AddAttribute(XmlDocument xml, string currentNodePath, string attributeName
                                               , string attributeValue)
        {
            bool flag = false;
            XmlNode currentNode = xml.SelectSingleNode(currentNodePath);
            if (currentNodePath != null)
            {
                XmlAttribute attribute = xml.CreateAttribute(attributeName);  //创建一个新属性
                currentNode.Attributes.Append(attribute);
                XmlElement currentElement = currentNode as XmlElement;
                currentElement.SetAttribute(attributeName, attributeValue);
                flag = true;
            }
            return flag;
        }

        #endregion

        #region  查

        /// <summary>
        /// 查找指定节点名的节点列表
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns>符合要求的节点列表</returns>
        public static XmlNodeList QueryNodeList(XmlDocument xml, string nodeName)
        {
            XmlNodeList nodeList = xml.GetElementsByTagName(nodeName);
            if (nodeList.Count != 0)
            {
                return nodeList;
            }
            return null;
        }

        /// <summary>
        /// 查找指定节点的指定属性值
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodePath">节点的xPath路径,必须是独一无二的。如果有多个则只返回第一个</param>
        /// <param name="attributeName">要查询的属性</param>
        /// <returns></returns>
        public static string QueryAttribute(XmlDocument xml, string nodePath, string attributeName)
        {
            string res = null;
            XmlNode currentNode = xml.SelectSingleNode(nodePath);
            if (currentNode != null)
            {
                XmlAttributeCollection xac = currentNode.Attributes;  //当前节点的属性集合
                for (int i = 0; i < xac.Count; i++)
                {
                    if (xac[i].Name == attributeName)
                    {
                        res = xac[i].Value;
                        break;
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// 查找指定节点的值
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodepath">节点的xPath路径,必须是独一无二的。如果有多个则只返回第一个</param>
        /// <returns></returns>
        public static string QueryNode(XmlDocument xml, string nodepath)
        {
            string res = null;
            XmlNode currentNode = xml.SelectSingleNode(nodepath);
            if (currentNode != null)
            {
                res = currentNode.InnerText;
            }
            return res;
        }

        #endregion

        #region 存

        public static void Save(XmlDocument xml, string path)
        {
            xml.Save(path);
        }

        #endregion

        #region 转

        /// <summary>
        /// 将xml转换为dataset
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static DataSet XmlToDataset(XmlDocument xml)
        {
            DataSet dataSet = new DataSet();
            //TODO: dataset
            return dataSet;
        }

        #endregion

        #region XML转
        /// <summary>
        ///  xml文本到dataset
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        public static DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                //从stream装载到XmlTextReader
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        //将xml文件转换为DataSet
        public static DataSet ConvertXMLFileToDataSet(string xmlFile)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                XmlDocument xmld = new XmlDocument();
                xmld.Load(xmlFile);

                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmld.InnerXml);
                //从stream装载到XmlTextReader
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                //xmlDS.ReadXml(xmlFile);
                return xmlDS;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        //将DataSet转换为xml对象字符串
        public static string ConvertDataSetToXML(DataSet xmlDS)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;

            try
            {
                stream = new MemoryStream();
                //从stream装载到XmlTextReader
                writer = new XmlTextWriter(stream, Encoding.Unicode);

                //用WriteXml方法写入文件.
                xmlDS.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);

                UnicodeEncoding utf = new UnicodeEncoding();
                return utf.GetString(arr).Trim();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        //将DataSet转换为xml文件
        public static void ConvertDataSetToXMLFile(DataSet xmlDS, string xmlFile)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;

            try
            {
                stream = new MemoryStream();
                //从stream装载到XmlTextReader
                writer = new XmlTextWriter(stream, Encoding.Unicode);

                //用WriteXml方法写入文件.
                xmlDS.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);

                //返回Unicode编码的文本
                UnicodeEncoding utf = new UnicodeEncoding();
                StreamWriter sw = new StreamWriter(xmlFile);
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                sw.WriteLine(utf.GetString(arr).Trim());
                sw.Close();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }


        #endregion

        #endregion

        #region 内存表
        /// <summary>
        /// 查询内存表dt
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static DataTable GetDt(DataTable dt, string where)
        {
            DataTable newdt = new DataTable();
            newdt = dt.Clone(); // 克隆dt 的结构，包括所有 dt 架构和约束,并无数据； 
            DataRow[] rows = dt.Select(where); // 从dt 中查询符合条件的记录； 
            foreach (DataRow row in rows)  // 将查询的结果添加到dt中； 
            {
                newdt.Rows.Add(row.ItemArray);
            }
            return newdt;

        }


        #region //生成内存表
        public DataTable GetActiveDs(DataTable dt)
        {
            //dt = new DataTable();
            //DataColumn col1 = new DataColumn("fromcity", typeof(string));
            //dt.Columns.Add(col1);
            //DataColumn col2 = new DataColumn("tocity", typeof(string));
            //dt.Columns.Add(col2);
            //DataColumn col3 = new DataColumn("aviname", typeof(string));
            //dt.Columns.Add(col3);
            //string sql = "select cityname from city";
            //SqlDataReader dr = imp.GetSqlReader(CommandType.Text, sql);
            //while (dr.Read())
            //{
            //    SqlParameter[] sp = { 
            //                        new SqlParameter("@cityname",dr["cityname"])
            //                        };
            //    string avisql = "select cityname from city where cityname!=@cityname";
            //    SqlDataReader avidr = imp.GetSqlReader(CommandType.Text, avisql, sp);
            //    while (avidr.Read())
            //    {
            //        DataRow row = dt.NewRow();
            //        row["fromcity"] = dr["cityname"].ToString();
            //        row["tocity"] = avidr["cityname"].ToString();
            //        row["aviname"] = dr["cityname"].ToString() + "  到  " + avidr["cityname"] + "优惠啦！";
            //        dt.Rows.Add(row);
            //    }
            //}
            return dt;
        }
        #endregion
        #endregion

        #region web.config

        #region 针对AppSeting节进行操作！
        /// <summary>
        /// 针对AppSeting节进行添加操作！
        /// </summary>
        /// <param name="keys">键</param>
        /// <param name="values">值</param>
        //public static void AddAppSetting(string keys, string values)
        //{
        //    Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        //    //config.AppSettings.Settings.Add("ConnectionString", @"Data Source=MICROSOF-DB9A63\SQLEXPRESS;Initial Catalog=kelchina;Integrated Security=True");
        //    AppSettingsSection appseting = (AppSettingsSection)config.GetSection("appSettings");
        //    for (int i = 0; i < appseting.Settings.Count; i++)
        //    {
        //        if (keys == appseting.Settings.AllKeys[i])
        //        {
        //            return;//表示已存在相同的键，在这里，不可以重复添加
        //        }
        //    }
        //    config.AppSettings.Settings.Add(keys, values);
        //    config.Save();//保存
        //}
        #endregion

        #region 针对AppSeting节进行修改操作！
        ///// <summary>
        ///// 针对AppSeting节进行修改操作！
        ///// </summary>
        ///// <param name="NewValue">新值</param>
        ///// <param name="keys">键</param>
        //public static void AlertAppSetting(string keys,string NewValue)
        //{
        //    Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        //    AppSettingsSection appseting = (AppSettingsSection)config.GetSection("appSettings");
        //    for (int i = 0; i < appseting.Settings.Count; i++)
        //    {
        //        if (keys == appseting.Settings.AllKeys[i])// 存在与参数相同的键，可以对他的值进行修改！
        //        {
        //            appseting.Settings[keys].Value = NewValue;
        //            config.Save();
        //        }
        //    }
        //}

        #endregion

        #region //获取AppSetting节的值  指定键  获取值
        /// <summary>
        /// 获取AppSetting节的值  指定键  获取值
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
       // public static string GetAppStr(string keys)
       // {
           // string str = "";
           // Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
           // AppSettingsSection appseting = (AppSettingsSection)config.GetSection("appSettings");
           //for(int i=0;i<appseting.Settings.Count;i++)
           //{
           //    if (keys == appseting.Settings.AllKeys[i])
           //    {
           //        str += appseting.Settings[keys].Value;
           //    }
           //}
           //return str;
            
      //  }
        #endregion

        #region 添加connectionStrings节的值
        /// <summary>
        /// 添加connectionStrings节的值
        /// </summary>
        /// <param name="conn">model对象  对注释的san个属性进行赋值，然后调用作参数即可！</param>
        //public static void AddConnectionStrings(string ConnectionString, string name, string ProviderName)
        //{
        //    ConnectionStringSettings conn = new ConnectionStringSettings();
        //    //conn.ConnectionString =
        //    //     "Server=localhost;User ID=sa;Password=123456; " +
        //    //     "Database=Northwind;Persist Security Info=True";
        //    //conn.Name = "AppConnectionString2";
        //    //conn.ProviderName = "System.Data.SqlClient";

        //    // Add the new connection string to the web.config

        //    conn.ConnectionString = ConnectionString;
        //    conn.Name = name;
        //    conn.ProviderName = ProviderName;

        //    Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        //    if (config.ConnectionStrings.ConnectionStrings[name] != null)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        config.ConnectionStrings.ConnectionStrings.Add(conn);
        //        config.Save();
        //    }
        //}
        #endregion

        //#region 修改connectionStrings节的值
        ///// <summary>
        /////  修改connectionStrings节的值
        ///// </summary>
        ///// <param name="name">对象名称  例如 connectionString</param>
        ///// <param name="NewValue">对象值《新值》</param>
        //public static void AlertConnectionStrings(string name, string NewValue)
        //{
        //    Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        //    if (config.ConnectionStrings.ConnectionStrings[name] == null)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        config.ConnectionStrings.ConnectionStrings[name].ConnectionString = NewValue;
        //        config.Save();
        //    }
        //}
        //#endregion

        //#region 获取connectionStrings节的值
        ///// <summary>
        /////  获取connectionStrings节的值
        ///// </summary>
        ///// <param name="name">对象名称  例如 connectionString</param>
        ///// <returns></returns>
        //public static string GetConStr(string name)
        //{
        //    Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
           
        //    if (config.ConnectionStrings.ConnectionStrings[name]==null)
        //    {
        //        return "";
        //    }
        //    else
        //    {
        //        return config.ConnectionStrings.ConnectionStrings[name].ConnectionString;
        //    }
        //}
        //#endregion


        #endregion
 
        #region 清除缓存
        /// <summary>
        /// 清除缓存
        /// </summary>
        public static void ClearCmd()
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            // 关闭Shell的使用
            p.StartInfo.UseShellExecute = false;
            // 重定向标准输入
            p.StartInfo.RedirectStandardInput = true;
            // 重定向标准输出
            p.StartInfo.RedirectStandardOutput = true;
            //重定向错误输出
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 8");
            p.StandardInput.WriteLine("exit");
        }
        #endregion

        #region 短信发送接口
        /// <summary>
        /// 短信发送接口
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="conten">内容</param>
        /// <returns></returns>
        public static string sendMsg(string phone, string conten)
        {
            String ret = postWebRequest("http://www.smsbao.com/sms?u=shirupozhu&p=f25a2fc72690b780b2a14e140ef6a9e01&m=" + phone + "&c=" + conten, "", Encoding.UTF8);
            return ret;
        }
        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="paramData"></param>
        /// <param name="dataEncode"></param>
        /// <returns></returns>
        public static string postWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:32.0) Gecko/20100101 Firefox/32.0"; 
                webReq.ContentType = "application/x-www-form-urlencoded";
                //增加下面两个属性即可  
                webReq.KeepAlive = true;
                webReq.ProtocolVersion = HttpVersion.Version10;  
                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return ret;
        }
        #endregion

        #region 序列化 反序列化
        /// <summary>
        /// 序列化 将对象转化为流或者字符串
        /// 　序列化是将对象状态转换为可保持或传输的格式的过程，在序列化过程中，对象的公共字段和私有字段以及类的名称（包括包含该类的程序集）都被转换为字节流，然后写入数据流。与序列化相对的是反序列化，它将流转换为对象。这两个过程结合起来，可以轻松地存储和传输数据。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string MySerialization(object obj)//序列化
        {
            BinaryFormatter bf = new BinaryFormatter();  //声明一个序列化类

            MemoryStream ms = new MemoryStream();  //声明一个内存流

            bf.Serialize(ms, obj);  //执行序列化操作

            byte[] result = new byte[ms.Length];

            result = ms.ToArray();

            string temp = System.Convert.ToBase64String(result);

            /*此处为关键步骤，将得到的字节数组按照一定的编码格式转换为字符串，不然当对象包含中文时，进行反序列化操作时会产生编码错误*/

            ms.Flush();

            ms.Close();

            return temp;
        }

        /// <summary>
        /// 反序列化 将流或者字符串转化为对象   object as 对象类
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static object MyDeSerialization(string str) //反序列化
        {
            byte[] b = System.Convert.FromBase64String(str);
            //将得到的字符串根据相同的编码格式分成字节数组
            MemoryStream ms = new MemoryStream(b, 0, b.Length);
            //从字节数组中得到内存流
            BinaryFormatter bf = new BinaryFormatter();
            return bf.Deserialize(ms);
        }

        #endregion

        #region 生成唯一支付码
        #region 获取唯一消费码
        /// <summary>
        ///  //获取唯一消费码
        /// </summary>
        /// <returns></returns>
        public static string GetPayNumXX()
        {
            Random ran = new Random();
            int random = ran.Next(9999999);//7位随机数
            return random.ToString() + GetRandomCharXX(1);
        }
        #endregion


        #region 随机字符串
        /// <summary>
        /// 获取一个随机字符串
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GetRandomCharXX(int count)
        {
            string[] s = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            StringBuilder sb = new StringBuilder();
            Random ran = new Random();
            for (int i = 0; i < count; i++)
            {
                int temp = ran.Next(s.Length);
                sb.Append(s[temp]);
            }
            return sb.ToString();
        }
        #endregion



        #region 验证唯一支付吗是否重复，递归生成！
        /// <summary>
        /// 验证唯一支付ma是否重复，递归生成！
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        private string PayNumsXX(string nums)
        {
            string s="";
            //DataTable dt = thingBll.GetList(" CusNum='" + nums + "'").Tables[0];
            //if (dt.Rows.Count != 0)
            //{
            //    s = PayNums(GetPayNum());
            //}
            //else
            //{
            //    s = nums;
            //}
            return s;
        }
        #endregion
        #endregion

        #region Http下载文件
        /// <summary>
        /// Http下载文件
        /// </summary>
        public static string HttpDownloadFile(string url, string path)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream responseStream = response.GetResponseStream();

            //创建本地文件写入流
            Stream stream = new FileStream(path, FileMode.Create);

            byte[] bArr = new byte[1024];
            int size = responseStream.Read(bArr, 0, (int)bArr.Length);
            while (size > 0)
            {
                stream.Write(bArr, 0, size);
                size = responseStream.Read(bArr, 0, (int)bArr.Length);
            }
            stream.Close();
            responseStream.Close();
            return path;
        }
        #endregion

        #region Http上传文件
        /// <summary>
        /// Http上传文件
        /// </summary>
        public static string HttpUploadFile(string url, string path)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = "POST";
            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
            request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            int pos = path.LastIndexOf("\\");
            string fileName = path.Substring(pos + 1);

            //请求头部信息 
            StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"file\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", fileName));
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] bArr = new byte[fs.Length];
            fs.Read(bArr, 0, bArr.Length);
            fs.Close();

            Stream postStream = request.GetRequestStream();
            postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
            postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
            postStream.Write(bArr, 0, bArr.Length);
            postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            postStream.Close();

            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            //返回结果网页（html）代码
            string content = sr.ReadToEnd();
            return content;
        }
        #endregion

        #region cookie操作
        /// <summary>
        /// Cookies赋值
        /// </summary>
        /// <param name="strName">主键</param>
        /// <param name="strValue">键值</param>
        /// <param name="strDay">有效天数</param>
        /// <returns></returns>
        public static bool setCookie(string strName, string strValue, int strDay)
        {
            try
            {
                HttpCookie Cookie = new HttpCookie(strName);
                //Cookie.Domain = ".xxx.com";//当要跨域名访问的时候,给cookie指定域名即可,格式为.xxx.com
                Cookie.Expires = DateTime.Now.AddDays(strDay);
                Cookie.Value = strValue;
                System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 读取Cookies
        /// </summary>
        /// <param name="strName">主键</param>
        /// <returns></returns>

        public static string getCookie(string strName)
        {
            HttpCookie Cookie = System.Web.HttpContext.Current.Request.Cookies[strName];
            if (Cookie != null)
            {
                return Cookie.Value.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除Cookies
        /// </summary>
        /// <param name="strName">主键</param>
        /// <returns></returns>
        public static bool delCookie(string strName)
        {
            try
            {
                HttpCookie Cookie = new HttpCookie(strName);
                //Cookie.Domain = ".xxx.com";//当要跨域名访问的时候,给cookie指定域名即可,格式为.xxx.com
                Cookie.Expires = DateTime.Now.AddDays(-1);
                System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 判断数据类型和处理数据类型转换

        /// <summary>
        /// object 转 str
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ObjToStr(object o)
        {
            try
            {
                if (o == null)
                {
                    return "";
                }
                else
                {
                    return o.ToString();
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// object 转 str，若转换失败，则返回nullReplaceStr字符串
        /// </summary>
        /// <param name="o"></param>
        /// <param name="nullReplaceStr"></param>
        /// <returns></returns>
        public static string ObjToStr(object o, string nullReplaceStr)
        {
            try
            {
                if (o == null)
                {
                    return nullReplaceStr;
                }
                else
                {
                    return o.ToString();
                }
            }
            catch (Exception ex)
            {
                return nullReplaceStr;
            }
        }

        /// <summary>
        /// 判断对象是否可以转成int型
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool isNumber(object o)
        {
            int tmpInt;
            if (o == null)
            {
                return false;
            }
            if (o.ToString().Trim().Length == 0)
            {
                return false;
            }
            if (!int.TryParse(o.ToString(), out tmpInt))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 判断是否是合法的时间类型
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool isDateTime(object o)
        {
            DateTime tmpInt = new DateTime();
            if (o == null)
            {
                return false;
            }
            if (o.ToString().Trim().Length == 0)
            {
                return false;
            }
            if (!DateTime.TryParse(o.ToString(), out tmpInt))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool isDecimal(object o)
        {
            decimal tmpInt;
            if (o == null)
            {
                return false;
            }
            if (o.ToString().Trim().Length == 0)
            {
                return false;
            }
            if (!decimal.TryParse(o.ToString(), out tmpInt))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// 字符串转变成数字，如果转行失败，则返回0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int Str2Int(string str)
        {
            if (isNumber(str))
            {
                return int.Parse(str);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 字符串转变成数字，如果转行失败，则返回defaultInt
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int Str2Int(string str, int defaultInt)
        {
            if (isNumber(str))
            {
                return int.Parse(str);
            }
            else
            {
                return defaultInt;
            }
        }

        /// <summary>
        /// 对象object转int，若失败则为0
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int Obj2Int(object obj)
        {
            if (isNumber(obj))
            {
                return int.Parse(obj.ToString());
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 对象object转decimal，若失败则为defaultDec
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static decimal Obj2Decimal(object obj, decimal defaultDec)
        {
            decimal tmpDecimal = 0;
            if (obj == null || obj.ToString().Trim() == "")
            {
                return defaultDec;
            }

            if (decimal.TryParse(obj.ToString(), out tmpDecimal))
            {
                return tmpDecimal;
            }
            else
            {
                return defaultDec;
            }

        }

        /// <summary>
        /// 对象object转int，若失败则为defaultInt
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultInt"></param>
        /// <returns></returns>
        public static int Obj2Int(object obj, int defaultInt)
        {
            if (isNumber(obj))
            {
                return int.Parse(obj.ToString());
            }
            else
            {
                return defaultInt;
            }
        }


        public static float Str2Float(string str)
        {
            if (str == null || str.Trim().Length <= 0)
            {
                return 0;
            }
            float tmpFloat = 0;
            if (float.TryParse(str, out tmpFloat))
            {
                return tmpFloat;
            }
            else
            {
                return 0;
            }

        }


        public static decimal Str2Decimal(string str)
        {
            if (str == null || str.Trim().Length <= 0)
            {
                return 0;
            }
            decimal tmpFloat = 0;
            if (decimal.TryParse(str, out tmpFloat))
            {
                return tmpFloat;
            }
            else
            {
                return 0;
            }

        }

        /// <summary>
        /// object 2时间类型，如果不成功则返回1990-1-1
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime Obj2DateTime(object obj)
        {
            if (isDateTime(obj))
            {
                return DateTime.Parse(obj.ToString());
            }
            else
            {
                return DateTime.Parse("1990-1-1");
            }
        }

        public static decimal decimalF2(decimal num)
        {
            decimal ret = 0;
            ret = decimal.Parse(num.ToString("f2"));

            return ret;
        }

        #endregion

        #region 站点的基本url信息

        /// <summary>
        /// 获取当前网站根地址 http://wwww.baidu.com
        /// </summary>
        public static string GetRootUrl()
        {
            return "http://" + HttpContext.Current.Request.Url.Host;
        }

        /// <summary>
        /// 获得网站的根目录的url,比如http://www.baidu.com
        /// </summary>
        /// <returns></returns>
        public static string getWebSite()
        {
            string website = "http://" + HttpContext.Current.Request.Url.Authority;
            return website;
        }

        /// <summary>
        /// 设当前页完整地址 
        /// 比如：http://www.jb51.net/aaa/bbb.aspx?id=5&name=kelli 
        /// </summary>
        /// <returns></returns>
        public static string getTotalUrl()
        {
            string url = HttpContext.Current.Request.Url.ToString();
            return url;

        }
        /// <summary>
        /// 取得网站根目录的物理路径
        /// </summary>
        /// <returns></returns>
        public static string GetRootPath()
        {
            string AppPath = "";
            HttpContext HttpCurrent = HttpContext.Current;
            if (HttpCurrent != null)
            {
                AppPath = HttpCurrent.Server.MapPath("~");
            }
            else
            {
                AppPath = AppDomain.CurrentDomain.BaseDirectory;
                if (Regex.Match(AppPath, @"\\$", RegexOptions.Compiled).Success)
                    AppPath = AppPath.Substring(0, AppPath.Length - 1);
            }
            return AppPath;
        }

        #endregion

        #region 检索文件
        static System.Collections.ArrayList alst;
        /// <summary>
        /// 检索文件
        /// </summary>
        /// <param name="dir">目录</param>
        /// <param name="Filetype">文件类型 .css  .jpg</param>
             ///  foreach (string f in readlist(Server.MapPath(@"/Manger/")))//xiaobaigang为文件夹名称
            ///{
           ///    Response.Write(f);
          ///    //this.ListBox1.Items.Add(f);
         ///}
        public static void GetFiles(string dir,string Filetype)
        {
            try
            {
                string[] files = Directory.GetFiles(dir);//得到文件
                foreach (string file in files)//循环文件
                {
                    string exname = file.Substring(file.LastIndexOf(".") + 1);//得到后缀名
                    // if (".txt|.aspx".IndexOf(file.Substring(file.LastIndexOf(".") + 1)) > -1)//查找.txt .aspx结尾的文件
                    if (Filetype.IndexOf(file.Substring(file.LastIndexOf(".") + 1)) > -1)//如果后缀名为.txt文件
                    {
                        FileInfo fi = new FileInfo(file);//建立FileInfo对象
                        alst.Add(fi.FullName);//把.txt文件全名加人到FileInfo对象

                        //if (File.Exists(fi.FullName))
                        //{
                        //    File.Delete(fi.FullName);
                        //}
                    }
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 获取CSS文件
        /// </summary>
        /// <param name="dir"></param>
        public static void GetFiles(string dir)
        {
            try
            {
                string[] files = Directory.GetFiles(dir);//得到文件
                foreach (string file in files)//循环文件
                {
                    string exname = file.Substring(file.LastIndexOf(".") + 1);//得到后缀名
                    // if (".txt|.aspx".IndexOf(file.Substring(file.LastIndexOf(".") + 1)) > -1)//查找.txt .aspx结尾的文件
                    if (".css".IndexOf(file.Substring(file.LastIndexOf(".") + 1)) > -1)//如果后缀名为.txt文件
                    {
                        FileInfo fi = new FileInfo(file);//建立FileInfo对象
                        alst.Add(fi.FullName);//把.txt文件全名加人到FileInfo对象

                        //if (File.Exists(fi.FullName))
                        //{
                        //    File.Delete(fi.FullName);
                        //}
                    }
                }
            }
            catch
            {

            }
        }
        public static string[] readlist(string path)
        {
            alst = new System.Collections.ArrayList();//建立ArrayList对象
            GetDirs(path);//得到文件夹
            return (string[])alst.ToArray(typeof(string));//把ArrayList转化为string[]
        }

        public static void GetDirs(string d)//得到所有文件夹
        {
            GetFiles(d);//得到所有文件夹里面的文件
            try
            {
                string[] dirs = Directory.GetDirectories(d);
                foreach (string dir in dirs)
                {
                    GetDirs(dir);//递归
                }
            }
            catch
            {
            }
        }
        #endregion


    }

    #region XML操作公共类2
    /// <summary>
    /// 操作公共类XML--2
    /// </summary>
    public class CommXML
    {
        #region 构造函数
        public CommXML()
        { }

        public CommXML(string strPath)
        {
            this._XMLPath = strPath;
        }
        #endregion

        #region 公有属性
        private string _XMLPath;
        public string XMLPath
        {
            get { return this._XMLPath; }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 导入XML文件
        /// </summary>
        /// <param name="XMLPath">XML文件路径</param>
        private XmlDocument XMLLoad()
        {
            string XMLFile = XMLPath;
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                string filename = AppDomain.CurrentDomain.BaseDirectory.ToString() + XMLFile;
                if (File.Exists(filename)) xmldoc.Load(filename);
            }
            catch (Exception e)
            { }
            return xmldoc;
        }

        /// <summary>
        /// 导入XML文件
        /// </summary>
        /// <param name="XMLPath">XML文件路径</param>
        private static XmlDocument XMLLoad(string strPath)
        {
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                string filename = AppDomain.CurrentDomain.BaseDirectory.ToString() + strPath;
                if (File.Exists(filename)) xmldoc.Load(filename);
            }
            catch (Exception e)
            { }
            return xmldoc;
        }

        /// <summary>
        /// 返回完整路径
        /// </summary>
        /// <param name="strPath">Xml的路径</param>
        private static string GetXmlFullPath(string strPath)
        {
            if (strPath.IndexOf(":") > 0)
            {
                return strPath;
            }
            else
            {
                return System.Web.HttpContext.Current.Server.MapPath(strPath);
            }
        }
        #endregion

        #region 读取数据
        /// <summary>
        /// 读取指定节点的数据
        /// </summary>
        /// <param name="node">节点</param>
        /// 使用示列:
        /// XMLProsess.Read("/Node", "")
        /// XMLProsess.Read("/Node/Element[@Attribute='Name']")
        public string Read(string node)
        {
            string value = "";
            try
            {
                XmlDocument doc = XMLLoad();
                XmlNode xn = doc.SelectSingleNode(node);
                value = xn.InnerText;
            }
            catch { }
            return value;
        }

        /// <summary>
        /// 读取指定路径和节点的串联值
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// 使用示列:
        /// XMLProsess.Read(path, "/Node", "")
        /// XMLProsess.Read(path, "/Node/Element[@Attribute='Name']")
        public static string Read(string path, string node)
        {
            string value = "";
            try
            {
                XmlDocument doc = XMLLoad(path);
                XmlNode xn = doc.SelectSingleNode(node);
                value = xn.InnerText;
            }
            catch { }
            return value;
        }

        /// <summary>
        /// 读取指定路径和节点的属性值
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// 使用示列:
        /// XMLProsess.Read(path, "/Node", "")
        /// XMLProsess.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
        public static string Read(string path, string node, string attribute)
        {
            string value = "";
            try
            {
                XmlDocument doc = XMLLoad(path);
                XmlNode xn = doc.SelectSingleNode(node);
                value = (attribute.Equals("") ? xn.InnerText : xn.Attributes[attribute].Value);
            }
            catch { }
            return value;
        }

        /// <summary>
        /// 获取某一节点的所有孩子节点的值
        /// </summary>
        /// <param name="node">要查询的节点</param>
        public string[] ReadAllChildallValue(string node)
        {
            int i = 0;
            string[] str = { };
            XmlDocument doc = XMLLoad();
            XmlNode xn = doc.SelectSingleNode(node);
            XmlNodeList nodelist = xn.ChildNodes;  //得到该节点的子节点
            if (nodelist.Count > 0)
            {
                str = new string[nodelist.Count];
                foreach (XmlElement el in nodelist)//读元素值
                {
                    str[i] = el.Value;
                    i++;
                }
            }
            return str;
        }

        /// <summary>
        /// 获取某一节点的所有孩子节点的值
        /// </summary>
        /// <param name="node">要查询的节点</param>
        public XmlNodeList ReadAllChild(string node)
        {
            XmlDocument doc = XMLLoad();
            XmlNode xn = doc.SelectSingleNode(node);
            XmlNodeList nodelist = xn.ChildNodes;  //得到该节点的子节点
            return nodelist;
        }

        /// <summary> 
        /// 读取XML返回经排序或筛选后的DataView
        /// </summary>
        /// <param name="strWhere">筛选条件，如:"name='kgdiwss'"</param>
        /// <param name="strSort"> 排序条件，如:"Id desc"</param>
        public DataView GetDataViewByXml(string strWhere, string strSort)
        {
            try
            {
                string XMLFile = this.XMLPath;
                string filename = AppDomain.CurrentDomain.BaseDirectory.ToString() + XMLFile;
                DataSet ds = new DataSet();
                ds.ReadXml(filename);
                DataView dv = new DataView(ds.Tables[0]); //创建DataView来完成排序或筛选操作 
                if (strSort != null)
                {
                    dv.Sort = strSort; //对DataView中的记录进行排序
                }
                if (strWhere != null)
                {
                    dv.RowFilter = strWhere; //对DataView中的记录进行筛选，找到我们想要的记录
                }
                return dv;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 读取XML返回DataSet
        /// </summary>
        /// <param name="strXmlPath">XML文件相对路径</param>
        public DataSet GetDataSetByXml(string strXmlPath)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                if (ds.Tables.Count > 0)
                {
                    return ds;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region 插入数据
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="attribute">属性名，非空时插入该元素属性值，否则插入元素值</param>
        /// <param name="value">值</param>
        /// 使用示列:
        /// XMLProsess.Insert(path, "/Node", "Element", "", "Value")
        /// XMLProsess.Insert(path, "/Node", "Element", "Attribute", "Value")
        /// XMLProsess.Insert(path, "/Node", "", "Attribute", "Value")
        public static void Insert(string path, string node, string element, string attribute, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
                XmlNode xn = doc.SelectSingleNode(node);
                if (element.Equals(""))
                {
                    if (!attribute.Equals(""))
                    {
                        XmlElement xe = (XmlElement)xn;
                        xe.SetAttribute(attribute, value);
                    }
                }
                else
                {
                    XmlElement xe = doc.CreateElement(element);
                    if (attribute.Equals(""))
                        xe.InnerText = value;
                    else
                        xe.SetAttribute(attribute, value);
                    xn.AppendChild(xe);
                }
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
            }
            catch { }
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="strList">由XML属性名和值组成的二维数组</param>
        public static void Insert(string path, string node, string element, string[][] strList)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = doc.CreateElement(element);
                string strAttribute = "";
                string strValue = "";
                for (int i = 0; i < strList.Length; i++)
                {
                    for (int j = 0; j < strList[i].Length; j++)
                    {
                        if (j == 0)
                            strAttribute = strList[i][j];
                        else
                            strValue = strList[i][j];
                    }
                    if (strAttribute.Equals(""))
                        xe.InnerText = strValue;
                    else
                        xe.SetAttribute(strAttribute, strValue);
                }
                xn.AppendChild(xe);
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
            }
            catch { }
        }

        /// <summary>
        /// 插入一行数据
        /// </summary>
        /// <param name="strXmlPath">XML文件相对路径</param>
        /// <param name="Columns">要插入行的列名数组，如：string[] Columns = {"name","IsMarried"};</param>
        /// <param name="ColumnValue">要插入行每列的值数组，如：string[] ColumnValue={"XML大全","false"};</param>
        /// <returns>成功返回true,否则返回false</returns>
        public static bool WriteXmlByDataSet(string strXmlPath, string[] Columns, string[] ColumnValue)
        {
            try
            {
                //根据传入的XML路径得到.XSD的路径，两个文件放在同一个目录下
                string strXsdPath = strXmlPath.Substring(0, strXmlPath.IndexOf(".")) + ".xsd";
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(GetXmlFullPath(strXsdPath)); //读XML架构，关系到列的数据类型
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                DataTable dt = ds.Tables[0];
                DataRow newRow = dt.NewRow();                 //在原来的表格基础上创建新行
                for (int i = 0; i < Columns.Length; i++)      //循环给一行中的各个列赋值
                {
                    newRow[Columns[i]] = ColumnValue[i];
                }
                dt.Rows.Add(newRow);
                dt.AcceptChanges();
                ds.AcceptChanges();
                ds.WriteXml(GetXmlFullPath(strXmlPath));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改指定节点的数据
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="value">值</param>
        public void Update(string node, string value)
        {
            try
            {
                XmlDocument doc = XMLLoad();
                XmlNode xn = doc.SelectSingleNode(node);
                xn.InnerText = value;
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + XMLPath);
            }
            catch { }
        }

        /// <summary>
        /// 修改指定节点的数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="value">值</param>
        /// 使用示列:
        /// XMLProsess.Insert(path, "/Node","Value")
        /// XMLProsess.Insert(path, "/Node","Value")
        public static void Update(string path, string node, string value)
        {
            try
            {
                XmlDocument doc = XMLLoad(path);
                XmlNode xn = doc.SelectSingleNode(node);
                xn.InnerText = value;
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
            }
            catch { }
        }

        /// <summary>
        /// 修改指定节点的属性值(静态)
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// 使用示列:
        /// XMLProsess.Insert(path, "/Node", "", "Value")
        /// XMLProsess.Insert(path, "/Node", "Attribute", "Value")
        public static void Update(string path, string node, string attribute, string value)
        {
            try
            {
                XmlDocument doc = XMLLoad(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xe.InnerText = value;
                else
                    xe.SetAttribute(attribute, value);
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
            }
            catch { }
        }

        /// <summary>
        /// 更改符合条件的一条记录
        /// </summary>
        /// <param name="strXmlPath">XML文件路径</param>
        /// <param name="Columns">列名数组</param>
        /// <param name="ColumnValue">列值数组</param>
        /// <param name="strWhereColumnName">条件列名</param>
        /// <param name="strWhereColumnValue">条件列值</param>
        public static bool UpdateXmlRow(string strXmlPath, string[] Columns, string[] ColumnValue, string strWhereColumnName, string strWhereColumnValue)
        {
            try
            {
                string strXsdPath = strXmlPath.Substring(0, strXmlPath.IndexOf(".")) + ".xsd";
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(GetXmlFullPath(strXsdPath));//读XML架构，关系到列的数据类型
                ds.ReadXml(GetXmlFullPath(strXmlPath));

                //先判断行数
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //如果当前记录为符合Where条件的记录
                        if (ds.Tables[0].Rows[i][strWhereColumnName].ToString().Trim().Equals(strWhereColumnValue))
                        {
                            //循环给找到行的各列赋新值
                            for (int j = 0; j < Columns.Length; j++)
                            {
                                ds.Tables[0].Rows[i][Columns[j]] = ColumnValue[j];
                            }
                            ds.AcceptChanges();                     //更新DataSet
                            ds.WriteXml(GetXmlFullPath(strXmlPath));//重新写入XML文件
                            return true;
                        }
                    }

                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除节点值
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// <param name="value">值</param>
        /// 使用示列:
        /// XMLProsess.Delete(path, "/Node", "")
        /// XMLProsess.Delete(path, "/Node", "Attribute")
        public static void Delete(string path, string node)
        {
            try
            {
                XmlDocument doc = XMLLoad(path);
                XmlNode xn = doc.SelectSingleNode(node);
                xn.ParentNode.RemoveChild(xn);
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
            }
            catch { }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// <param name="value">值</param>
        /// 使用示列:
        /// XMLProsess.Delete(path, "/Node", "")
        /// XMLProsess.Delete(path, "/Node", "Attribute")
        public static void Delete(string path, string node, string attribute)
        {
            try
            {
                XmlDocument doc = XMLLoad(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xn.ParentNode.RemoveChild(xn);
                else
                    xe.RemoveAttribute(attribute);
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
            }
            catch { }
        }

        /// <summary>
        /// 删除所有行
        /// </summary>
        /// <param name="strXmlPath">XML路径</param>
        public static bool DeleteXmlAllRows(string strXmlPath)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Rows.Clear();
                }
                ds.WriteXml(GetXmlFullPath(strXmlPath));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 通过删除DataSet中指定索引行，重写XML以实现删除指定行
        /// </summary>
        /// <param name="iDeleteRow">要删除的行在DataSet中的Index值</param>
        public static bool DeleteXmlRowByIndex(string strXmlPath, int iDeleteRow)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Rows[iDeleteRow].Delete();
                }
                ds.WriteXml(GetXmlFullPath(strXmlPath));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除指定列中指定值的行
        /// </summary>
        /// <param name="strXmlPath">XML相对路径</param>
        /// <param name="strColumn">列名</param>
        /// <param name="ColumnValue">指定值</param>
        public static bool DeleteXmlRows(string strXmlPath, string strColumn, string[] ColumnValue)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //判断行多还是删除的值多，多的for循环放在里面
                    if (ColumnValue.Length > ds.Tables[0].Rows.Count)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            for (int j = 0; j < ColumnValue.Length; j++)
                            {
                                if (ds.Tables[0].Rows[i][strColumn].ToString().Trim().Equals(ColumnValue[j]))
                                {
                                    ds.Tables[0].Rows[i].Delete();
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < ColumnValue.Length; j++)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (ds.Tables[0].Rows[i][strColumn].ToString().Trim().Equals(ColumnValue[j]))
                                {
                                    ds.Tables[0].Rows[i].Delete();
                                }
                            }
                        }
                    }
                    ds.WriteXml(GetXmlFullPath(strXmlPath));
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
    #endregion
}

#region 枚举类型  用于数据导出
/// <summary>
/// 导出文件的格式
/// </summary>

/// <summary>
/// 导出文件的格式
/// </summary>
public enum ExportFormat
{
    /// <summary>
    /// XLS
    /// </summary>
    xls,
    /// <summary>
    /// CSV
    /// </summary>
    CSV,
    /// <summary>
    /// DOC
    /// </summary>
    DOC,
    /// <summary>
    /// TXT
    /// </summary>
    TXT
}
#endregion








