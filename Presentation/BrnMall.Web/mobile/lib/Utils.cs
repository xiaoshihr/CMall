using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
namespace BrnMall.WxPayAPI
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Utils
    {
         public static string GetStar(object pice)
         {
            return (Utils.StrToDecimal(pice) / 100).ToString("f0");
         }
         public static string GetStarimg(object pice)
         {
             decimal ii = Utils.StrToDecimal(pice) / 100;
             if (ii >= 10)
             {
                 return "★★★★★★★★★★";
             }
             if (ii >= 9)
             {
                 return "★★★★★★★★★";
             }
             if (ii >= 8)
             {
                 return "★★★★★★★★";
             }
             if (ii >= 7)
             {
                 return "★★★★★★★";
             }
             if (ii >= 6)
             {
                 return "★★★★★★";
             }
             if (ii >= 5)
             {
                 return "★★★★★";
             }
             if (ii >= 4)
             {
                 return "★★★★";
             }
             if (ii >= 3)
             {
                 return "★★★";
             }
             if (ii >= 2)
             {
                 return "★★";
             }
             if (ii >= 1)
             {
                 return "★";
             }
             return "";
         }
        #region 截取字符长度
        /// <summary>
        /// 截取字符长度
        /// </summary>
        /// <param name="inputString">字符</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string CutString(string inputString, int len)
        {
            if (string.IsNullOrEmpty(inputString))
                return "";
            inputString = DropHTML(inputString);
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > len)
                    break;
            }
            //如果截过则加上半个省略号 
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (mybyte.Length > len)
                tempString += "…";
            return tempString;
        }
        #endregion

        #region 清除HTML标记
        public static string DropHTML(string Htmlstring)
        {
            if (string.IsNullOrEmpty(Htmlstring)) return "";
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
            Htmlstring.Replace("&emsp;", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
        #endregion

        #region 清除HTML标记且返回相应的长度
        public static string DropHTML(string Htmlstring, int strLen)
        {
            return CutString(DropHTML(Htmlstring), strLen);
        }
        #endregion
        #region 删除最后结尾的一个逗号
        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        public static string DelLastComma(string str)
        {
            if (str.Length < 1)
            {
                return "";
            }
            return str.Substring(0, str.LastIndexOf(","));
        }
        #endregion
        #region 生成指定长度的字符串
        /// <summary>
        /// 生成指定长度的字符串,即生成strLong个str字符串
        /// </summary>
        /// <param name="strLong">生成的长度</param>
        /// <param name="str">以str生成字符串</param>
        /// <returns></returns>
        public static string StringOfChar(int strLong, string str)
        {
            string ReturnStr = "";
            for (int i = 0; i < strLong; i++)
            {
                ReturnStr += str;
            }

            return ReturnStr;
        }
        #endregion
        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns></returns>
        public static int GetStringLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        /// <summary>
        /// string型转换为int型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(object strValue)
        {
            int defValue = 0;
            if ((strValue == null) || (strValue.ToString() == string.Empty) || (strValue.ToString().Length > 10))
            {
                return defValue;
            }

            string val = strValue.ToString();
            string firstletter = val[0].ToString();

            if (val.Length == 10 && IsNumber(firstletter) && int.Parse(firstletter) > 1)
            {
                return defValue;
            }
            else if (val.Length == 10 && !IsNumber(firstletter))
            {
                return defValue;
            }


            int intValue = defValue;
            if (strValue != null)
            {
                if (IsNumber(strValue.ToString()))
                {
                    intValue = Convert.ToInt32(strValue);
                }
            }

            return intValue;
        }

        /// <summary>
        /// string型转换为int型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(object strValue, int defValue)
        {
            if ((strValue == null) || (strValue.ToString() == string.Empty) || (strValue.ToString().Length > 10))
            {
                return defValue;
            }

            string val = strValue.ToString();
            string firstletter = val[0].ToString();

            if (val.Length == 10 && IsNumber(firstletter) && int.Parse(firstletter) > 1)
            {
                return defValue;
            }
            else if (val.Length == 10 && !IsNumber(firstletter))
            {
                return defValue;
            }


            int intValue = defValue;
            if (strValue != null)
            {
                if (IsNumber(strValue.ToString()))
                {
                    intValue = Convert.ToInt32(strValue);
                }
            }

            return intValue;
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(object strValue)
        {
            if ((strValue == null) || (strValue.ToString().Length > 10))
            {
                return 0;
            }

            float intValue = 0;
            if (strValue != null)
            {
                if (IsFloat(strValue.ToString()))
                {
                    intValue = Convert.ToSingle(strValue);
                }
            }
            return intValue;
        }

        /// <summary>
        /// 判断给定的字符串(strNumber)是否是数值型
        /// </summary>
        /// <param name="numberString">要确认的字符串</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsFloat(string numberString)
        {
            Regex rCode = new Regex("^[+-]?\\d+(\\.\\d+)?$");
            if (!rCode.IsMatch(numberString))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 转换为decimal
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static decimal StrToDecimal(object strValue)
        {
            if ((strValue == null))
            {
                return 0;
            }

            decimal intValue = 0;
            if (strValue != null)
            {
                if (IsFloat(strValue.ToString()))
                {
                    intValue = Convert.ToDecimal(strValue);
                }
            }
            return intValue;
        }

        /// <summary>
        /// 转换为double
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static double StrToDouble(object strValue)
        {
            if ((strValue == null))
            {
                return 0;
            }

            double intValue = 0;
            if (strValue != null)
            {
                if (IsFloat(strValue.ToString()))
                {
                    intValue = Convert.ToDouble(strValue);
                }
            }
            return intValue;
        }

        /// <summary>
        /// 取得四舍五入后价格
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static decimal GetPriceByRound(decimal price)
        {
            return decimal.Round(price, 0, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 取得四舍五入后价格
        /// </summary>
        /// <param name="decimals">小数点后位数</param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static decimal GetPriceByRound(int decimals, decimal price)
        {
            return decimal.Round(price, decimals, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 整数或纯数字的验证  
        /// </summary>
        public static bool IsNumber(string numberString)
        {
            Regex rCode = new Regex("^[+-]?\\d+(\\d+)?$");
            if (!rCode.IsMatch(numberString))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
        /// </summary>
        /// <param name="strNumber">要确认的字符串数组</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsNumberArray(string[] strNumber)
        {
            if (strNumber == null)
            {
                return false;
            }
            if (strNumber.Length < 1)
            {
                return false;
            }
            foreach (string id in strNumber)
            {
                if (!IsNumber(id))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool CheckNumberArray(string[] strNumber)
        {
            return CheckNumberArray(strNumber, 0);
        }

        public static bool CheckNumberArray(string[] strNumber, int defaultValue)
        {
            if (strNumber == null)
            {
                return false;
            }
            if (strNumber.Length < 1)
            {
                return false;
            }
            foreach (string id in strNumber)
            {
                if (!IsNumber(id))
                {
                    return false;
                }
                else
                {
                    if (Utils.StrToInt(id) <= defaultValue)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 删除字符串尾部的回车/换行/空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RTrim(string str)
        {
            for (int i = str.Length; i >= 0; i--)
            {
                if (str[i].Equals(" ") || str[i].Equals("\r") || str[i].Equals("\n"))
                {
                    str.Remove(i, 1);
                }
            }
            return str;
        }

        /// <summary>
        /// 清除给定字符串中的回车及换行符
        /// </summary>
        /// <param name="str">要清除的字符串</param>
        /// <returns>清除后返回的字符串</returns>
        public static string ClearBR(string str)
        {
            Regex r = null;
            Match m = null;

            r = new Regex(@"(\r\n)", RegexOptions.IgnoreCase);
            for (m = r.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "");
            }


            return str;
        }

        /// <summary>
        /// 清除给定字符串中的HTML字符
        /// </summary>
        public static string ClearHtml(string strHtml)
        {
            if (strHtml != "")
            {
                Regex r = null;
                Match m = null;

                r = new Regex(@"<\/?[^>]*>", RegexOptions.IgnoreCase);
                for (m = r.Match(strHtml); m.Success; m = m.NextMatch())
                {
                    strHtml = strHtml.Replace(m.Groups[0].ToString(), "");
                }
            }
            return strHtml;
        }

        /// <summary>
        /// 从字符串的指定位置截取指定长度的子字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <param name="length">子字符串的长度</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int startIndex, int length)
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


                if (startIndex > str.Length)
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

            if (str.Length - startIndex < length)
            {
                length = str.Length - startIndex;
            }

            return str.Substring(startIndex, length);
        }

        /// <summary>
        /// 从左取得指定长度的字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="len">指定长度</param>
        /// <returns></returns>
        public static string LeftStr(string str, int len)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            if (len >= str.Length)
            {
                len = str.Length;
            }
            return str.Substring(0, len);

        }

        /// <summary>
        /// 从右取得指定长度的字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="len">指定长度</param>
        /// <returns></returns>
        public static string RightStr(string str, int len)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            if (len > str.Length)
            {
                len = str.Length;
            }
            return str.Substring(str.Length - len, len);
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] SplitString(string source, string split)
        {
            int len = split.Length;
            ArrayList al = new ArrayList();
            int start = 0; //开始位置


            int j = -1; //匹配索引位置
            while (true)
            {
                j = source.IndexOf(split, start);
                if (j > -1)
                {
                    al.Add(source.Substring(start, j - start));
                    int s = j - start;
                    start = j + len;
                }
                else
                {
                    al.Add(source.Substring(start));
                    break;
                }
            }
            string[] result;
            if (al.Count == 0)
            {
                string[] r = new string[1];
                r[0] = source;
                result = r;
            }
            else
            {
                string[] r = new string[al.Count];
                for (int i = 0; i < al.Count; i++)
                {
                    r[i] = al[i].ToString();
                }
                result = r;
            }
            return result;
        }

        /// <summary>
        /// 取得指定长度字符,中文为2个长度.
        /// </summary>
        /// <returns></returns>
        public static string GetStringByLen(string source, int len)
        {
            if (len <= 0)
            {
                return source;
            }
            else
            {
                if (source == null || source.Trim() == string.Empty)
                {
                    return "";
                }
                else
                {
                    source = source.Replace("[NextPage]", "");
                    source = source.Replace("&nbsp;", "");
                    source = ClearHtml(source);
                    ASCIIEncoding n = new ASCIIEncoding();
                    byte[] b = n.GetBytes(source);
                    int l = 0;  // l 为字符串之实际长度
                    for (int i = 0; i <= b.Length - 1; i++)
                    {
                        if (b[i] == 63)  //判断是否为汉字或全脚符号
                        {
                            l++;
                        }
                        l++;
                        if (l >= len)
                        {
                            return source.Substring(0, i);
                        }
                    }
                    return source;
                }
            }
        }

        /// <summary>
        /// 完整的过滤编码 [sql,html]
        /// </summary>
        /// <returns></returns>
        public static string FilterEncode(string value)
        {
            return HttpUtility.HtmlEncode(SqlEncode(value));
        }

        /// <summary>
        /// 过滤'字符，防止SQL注入
        /// </summary>
        /// <returns></returns>
        public static string SqlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            else
            {
                return str.Replace("'", "''");
            }
        }

        /// <summary>
        /// 为把字符转换成XML并替换特殊字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterStringToXml(string str)
        {
            if (str == null)
            {
                return "";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(str);
                sb.Replace("&", "&amp;");
                sb.Replace("<", "&lt;");
                sb.Replace(">", "&gt;");
                sb.Replace("\"", "&quot;");
                sb.Replace("\'", "&#39;");
                return sb.ToString();
            }
        }

        

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {

            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 检测是否有危险的可能用于链接的字符串
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeUserInfoString(string str)
        {
            return !Regex.IsMatch(str, @"^\s*$|^c:\\con\\con$|[%,\*" + "\"" + @"\s\t\<\>\&]|游客|^Guest");
        }

        /// <summary>
        /// 屏蔽SQL字符
        /// </summary>
        /// <returns></returns>
        public static string ReplaceSqlWord(string source)
        {
            char[] fobWords = { '~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '+', '=', '`', '[', ']', '{', '}', ';', ':', '\"', '\'', ',', '<', '>', '.', '/', '\\', '|', '?', '_' };
            StringBuilder keyword = new StringBuilder();
            keyword.Append(LeftStr(source, 100));
            for (int i = 0; i < fobWords.Length; i++)
            {
                keyword.Replace(fobWords[i], ' ');
            }
            return keyword.ToString();
        }

        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 判断是否为base64字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBase64String(string str)
        {
            //A-Z, a-z, 0-9, +, /, =
            return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
        }

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (strPath.IndexOf(":") == -1)
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Server.MapPath(strPath);
                }
                else //非web程序引用
                {
                    strPath = strPath.Replace("\\", "/");

                    if (Utils.LeftStr(strPath, 1) == "/") strPath = strPath.Substring(1, strPath.Length - 1);

                    strPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);

                    return strPath.Replace("\\", "/");
                }
            }
            else
            {
                return strPath;
            }
        }

        ///// <summary>
        ///// 返回文件是否存在
        ///// </summary>
        ///// <param name="filePath">文件绝对路径</param>
        ///// <returns>是否存在</returns>
        //public static bool FileExists(string filePath)
        //{
        //    return System.IO.File.Exists(filePath);
        //}

        /// <summary>
        /// 逐层创建文件夹
        /// </summary>
        ///<param name="folderPath">文件夹相对路径</param>
        public static void CreateFolder(string folderPath)
        {
            try
            {
                folderPath = folderPath.Replace("\\", "/");
                folderPath = folderPath.Replace("//", "/");
                if (Utils.RightStr(folderPath, 1) != "/")
                {
                    folderPath += "/";
                }
                if (!Directory.Exists(GetMapPath(folderPath)))
                {
                    string[] strFolderPath = folderPath.Split('/');
                    string aimFolder = strFolderPath[0];
                    string CreatePath;
                    for (int i = 1; i < strFolderPath.Length - 1; i++)
                    {
                        aimFolder = aimFolder + "/" + strFolderPath[i];
                        CreatePath = GetMapPath(aimFolder);
                        if (!Directory.Exists(CreatePath))
                        {
                            Directory.CreateDirectory(CreatePath);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 移动文件夹
        /// </summary>
        ///<param name="sourceDirName">源文件夹相对路径</param>
        ///<param name="destDirName">目标文件夹相对路径</param>
        public static void MoveFolder(string sourceDirName, string destDirName)
        {
            try
            {
                sourceDirName = GetMapPath(sourceDirName);
                destDirName = GetMapPath(destDirName);

                System.IO.Directory.Move(sourceDirName, destDirName);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 复制文件夹中的所有文件到指定文件夹
        /// </summary>
        /// <param name="DirectoryPath">源文件夹路径</param>
        /// <param name="DirAddress">保存路径</param>
        /// <param name="DirFirst">true保留第一个文件夹目录，false不保留第一个文件夹目录</param>
        public static void CopyDirectory(string DirectoryPath, string DirAddress, bool DirFirst)//复制文件夹，
        {
            string s = DirectoryPath.Substring(DirectoryName(DirectoryPath));//获取文件夹名
            DirectoryInfo DirectoryArray = new DirectoryInfo(DirectoryPath);
            FileInfo[] Files = DirectoryArray.GetFiles();//获取该文件夹下的文件列表     
            DirectoryInfo[] Directorys = DirectoryArray.GetDirectories();//获取该文件夹下的文件夹列表 

            if (!DirFirst)
            {
                if (Directory.Exists(DirAddress))
                {
                    //Directory.Delete(DirAddress, true);//若文件夹存在，不管目录是否为空，删除 
                    //Directory.CreateDirectory(DirAddress);//删除后，重新创建文件夹    
                }
                else
                {
                    Directory.CreateDirectory(DirAddress);//文件夹不存在，创建     
                }
                foreach (FileInfo inf in Files)//逐个复制文件     
                {
                    if (!File.Exists(DirAddress + "\\" + inf.Name))
                        File.Copy(DirectoryPath + "\\" + inf.Name, DirAddress + "\\" + inf.Name);
                }
                foreach (DirectoryInfo Dir in Directorys)//逐个获取文件夹名称，并递归调用方法本身     
                {
                    CopyDirectory(DirectoryPath + "\\" + Dir.Name, DirAddress, true);
                }
            }
            else
            {
                if (Directory.Exists(DirAddress + "\\" + s))
                {
                    //Directory.Delete(DirAddress + "\\" + s, true);//若文件夹存在，不管目录是否为空，删除 
                    //Directory.CreateDirectory(DirAddress + "\\" + s);//删除后，重新创建文件夹    
                }
                else
                {
                    Utils.CreateFolder(DirAddress);
                    //Directory.CreateDirectory(DirAddress + "\\" + s);//文件夹不存在，创建     
                }
                foreach (FileInfo inf in Files)//逐个复制文件     
                {
                    if (!File.Exists(DirAddress + "\\" + inf.Name))
                        File.Copy(DirectoryPath + "\\" + inf.Name, DirAddress + "\\" + inf.Name);
                }
                foreach (DirectoryInfo Dir in Directorys)//逐个获取文件夹名称，并递归调用方法本身     
                {
                    CopyDirectory(DirectoryPath + "\\" + Dir.Name, DirAddress + "\\" + Dir.Name, false);
                }
            }
        }

        /// <summary>
        /// 获取文件夹名称
        /// </summary>
        /// <param name="DirectoryPath">文件夹路径</param>
        /// <returns></returns>
        public static int DirectoryName(string DirectoryPath)//获取文件夹名，截取“\” 
        {
            string path = DirectoryPath;// Globals.GetFilePath(DirectoryPath);
            int j = 0;
            j = DirectoryPath.LastIndexOf("\\");
            return j + 1;
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourceFileUrl">源文件相对路径</param>
        /// <param name="targetUrl">目标地址相对路径</param>
        public static void CopyFile(string sourceFileUrl, string destUrl)
        {
            try
            {
                File.Copy(GetMapPath(sourceFileUrl), GetMapPath(destUrl), true);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 删除文件夹及子目录和文件
        /// </summary>
        /// <param name="path">要删除的文件夹相对路径</param>
        public static void DeleteDir(string path)
        {
            try
            {
                Directory.Delete(GetMapPath(path), true);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">要删除的文件相对路径</param>
        public static void DeleteFile(string path)
        {
            try
            {
                File.Delete(GetMapPath(path));
            }
            catch
            {
            }
        }

       

        /// <summary>
        /// 保存文件
        /// </summary>
        ///<param name="filePath">文件路径</param>
        ///<param name="fileContent">文件内容</param>
        public static void SaveFile(string filePath, string fileContent)
        {
            SaveFile(filePath, fileContent, "utf-8");
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        ///<param name="filePath">文件路径</param>
        ///<param name="fileContent">文件内容</param>
        ///<param name="encoding">保存编码</param>
        public static void SaveFile(string filePath, string fileContent, string encoding)
        {
            try
            {
                using (StreamWriter iStream = new StreamWriter(GetMapPath(filePath), false, System.Text.Encoding.GetEncoding(encoding)))
                {
                    iStream.Write(fileContent);
                    iStream.Flush();
                    iStream.Close();
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// 是否时间格式
        /// </summary>
        /// <returns></returns>
        public static bool IsDateTime(string timeval)
        {
            try
            {
                Convert.ToDateTime(timeval);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 隐藏IP,只取得IP前两位
        /// </summary>
        /// <returns></returns>
        public static string HideIP(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                return "";
            }
            else
            {
                return ip.Substring(0, ip.LastIndexOf(".") + 1) + "*";
            }
        }

        /// <summary>
        /// 获得星期中文名
        /// </summary>
        /// <param name="dt">时间</param>
        /// <returns></returns>
        public static string CaculateWeekDayCN(DateTime dt)
        {
            int week = Convert.ToInt32(dt.DayOfWeek.ToString("D"));
            string weekstr = string.Empty;
            switch (week)
            {
                case 1: weekstr = "星期一"; break;
                case 2: weekstr = "星期二"; break;
                case 3: weekstr = "星期三"; break;
                case 4: weekstr = "星期四"; break;
                case 5: weekstr = "星期五"; break;
                case 6: weekstr = "星期六"; break;
                case 7: weekstr = "星期日"; break;
            }

            return weekstr;
        }

        /// <summary>
        /// 获得星期英文名
        /// </summary>
        /// <param name="dt">时间</param>
        /// <returns></returns>
        public static string CaculateWeekDayEN(DateTime dt)
        {
            int week = Convert.ToInt32(dt.DayOfWeek.ToString("D"));
            string weekstr = string.Empty;
            switch (week)
            {
                case 1: weekstr = "Monday"; break;
                case 2: weekstr = "Tuesday"; break;
                case 3: weekstr = "Wendnesday"; break;
                case 4: weekstr = "Thursday"; break;
                case 5: weekstr = "Friday"; break;
                case 6: weekstr = "Saturday"; break;
                case 7: weekstr = "Sunday"; break;
            }

            return weekstr;
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="str">待加密字符</param>
        /// <returns></returns>
        public static string MD5_32(string str)
        {
            //32位加密  
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            else
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            }
        }
        /// <summary>
        /// 16位MD5加密（取32位加密的9~25字符）
        /// </summary>
        /// <param name="str">待加密字符</param>
        /// <returns></returns>
        public static string MD5_16(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            else
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            }
        }

        /// <summary>
        /// 判断字符串是否符合IP格式
        /// </summary>
        /// <returns></returns>
        public static bool IsIP(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;
            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";

            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }

        /// <summary>
        /// html编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HtmlEncode(string str)
        {
            if (str == null)
            {
                return "";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(str);
                //sb.Replace("&","&amp;");
                sb.Replace("   ", "&nbsp;&nbsp;&nbsp;");
                sb.Replace("    ", "&nbsp;&nbsp;&nbsp;&nbsp;");
                sb.Replace("     ", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                sb.Replace("      ", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                sb.Replace("<", "&lt;");
                sb.Replace(">", "&gt;");
                sb.Replace("\"", "&quot;");
                //sb.Replace("\'","&#39;");
                sb.Replace("\r", "");
                sb.Replace("\n\n", "<p></p>");
                sb.Replace("\n", "<br>");
                return sb.ToString();
            }
        }

        public static bool IsUrl(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return Regex.IsMatch(input, @"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$");
        }

        ///// <summary>
        ///// 返回 URL 字符串的编码结果
        ///// </summary>
        ///// <param name="str">字符串</param>
        ///// <returns>编码结果</returns>
        //public static string UrlEncode(string str)
        //{
        //    return HttpUtility.UrlEncode(str, System.Text.Encoding.UTF8);
        //}


        ///// <summary>
        ///// 返回 URL 字符串的编码结果
        ///// </summary>
        ///// <param name="str">字符串</param>
        ///// <returns>解码结果</returns>
        //public static string UrlDecode(string str)
        //{
        //    return HttpUtility.UrlDecode(str);
        //}


        //public static string Escape(string str)
        //{
        //    if (str == null)
        //        return String.Empty;

        //    StringBuilder sb = new StringBuilder();
        //    int len = str.Length;

        //    for (int i = 0; i < len; i++)
        //    {
        //        char c = str[i];

        //        //everything other than the optionally escaped chars _must_ be escaped 
        //        if (Char.IsLetterOrDigit(c) || c == '-' || c == '_' || c == '/' || c == '\\' || c == '.')
        //            sb.Append(c);
        //        else
        //            sb.Append(Uri.HexEscape(c));
        //    }

        //    return sb.ToString();
        //}

        //public static string UnEscape(string str)
        //{
        //    if (str == null)
        //        return String.Empty;

        //    StringBuilder sb = new StringBuilder();
        //    int len = str.Length;
        //    int i = 0;
        //    while (i != len)
        //    {
        //        if (Uri.IsHexEncoding(str, i))
        //            sb.Append(Uri.HexUnescape(str, ref i));
        //        else
        //            sb.Append(str[i++]);
        //    }

        //    return sb.ToString();
        //}



        /// <summary>
        /// 保存指定参数的Session值
        /// </summary>
        public static void SetSession(string name, object values)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            else
            {
                System.Web.HttpContext.Current.Session[name] = values;
            }
        }

        /// <summary>
        /// 保存指定参数的Session值
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="values">参数值</param>
        /// <param name="expires">过期时间(分钟,若为0则为系统默认)</param>
        public static void SetSession(string name, object values, int expires)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            else
            {
                System.Web.HttpContext.Current.Session[name] = values;
                System.Web.HttpContext.Current.Session.Timeout = expires;
            }
        }

        /// <summary>
        /// 取得指定参数的Session值
        /// </summary>
        public static object GetSession(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "";
            }
            else
            {
                if (System.Web.HttpContext.Current.Session[name] != null)
                {
                    return System.Web.HttpContext.Current.Session[name];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 从XML中读取数据
        /// </summary>
        public static DataSet GetDataFromXml(string filePath)
        {
            filePath = Utils.GetMapPath(filePath);
            if (!Utils.FileExists(filePath))
            {
                return new DataSet();
            }
            else
            {
                filePath = filePath.Replace("\\", "/");
                if (filePath.IndexOf(":/") < 0)
                {
                    filePath = Utils.GetMapPath(filePath);
                }
                DataSet ds = new DataSet();
                ds.ReadXml(filePath);
                return ds;
            }
        }

        /// <summary>
        /// 用空格切割单词
        /// </summary>
        public static string[] SplitENWord(string word)
        {
            return ReplaceSpaceNTo1(word).Split(' ');
        }
        /// <summary>
        /// 把连续空格替换成一个
        /// </summary>
        public static string ReplaceSpaceNTo1(string str)
        {
            return System.Text.RegularExpressions.Regex.Replace(str, @"([\s]+)", " ");
        }

        /// <summary>
        /// 是否正则匹配
        /// </summary>
        public static bool IsMatchRegex(string sourceStr, string regexStr)
        {
            if (sourceStr == null || regexStr == null || sourceStr == "" || regexStr == "")
            {
                return false;
            }
            else
            {
                Regex strPattern = new Regex(regexStr);
                return strPattern.IsMatch(sourceStr);
            }
        }
        /// <summary>
        /// 把字符转换成时间，默认为当前时间
        /// </summary>
        public static DateTime StrToDateTime(string time)
        {
            return StrToDateTime(time, DateTime.Now);
        }

        /// <summary>
        /// 把字符转换成时间
        /// </summary>
        public static DateTime StrToDateTime(string time, DateTime defaultDateTime)
        {
            if (IsDateTime(time))
            {
                return Convert.ToDateTime(time);
            }
            else
            {
                return defaultDateTime;
            }
        }

        /// <summary>
        /// 把字符串转换为JS字符，用于JS输出
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string JsEncode(string str)
        {
            StringBuilder sb = new StringBuilder(str);
            sb.Replace("\"", "\\\"");
            sb.Replace("'", "\\'");
            sb.Replace("\r", "");
            sb.Replace("\n", "");
            sb.Replace("\n\r", "");
            return sb.ToString();
        }

        //提高随机数不重复概率的种子生成方法
        public static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="n">随机种子</param>
        /// <param name="length">长度</param>
        /// <param name="source">字符来源</param>
        /// <returns></returns>
        public static string GetRandomString(int seed, int length, string source)
        {
            //声明要返回的字符串
            string tmpstr = "";
            //密码中包含的字符数组
            string pwdchars = source;
            //数组索引随机数
            int iRandNum;
            //随机数生成器
            Random rnd = new Random(seed * unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < length; i++)
            {
                //Random类的Next方法生成一个指定范围的随机数
                iRandNum = rnd.Next(pwdchars.Length);
                //tmpstr随机添加一个字符
                tmpstr += pwdchars[iRandNum];
            }
            return tmpstr;
        }

        /// <summary>
        /// 得到字符串每个字符的首字母
        /// </summary>
        public static String GetIndexCode(String IndexTxt)
        {
            String _Temp = null;
            for (int i = 0; i < IndexTxt.Length; i++)
                _Temp = _Temp + GetOneIndexCode(IndexTxt.Substring(i, 1));
            return _Temp;
        }

        /// <summary>
        /// 得到单个字符的首字母
        /// </summary>
        public static String GetOneIndexCode(String OneIndexTxt)
        {
            if (Convert.ToChar(OneIndexTxt) >= 0 && Convert.ToChar(OneIndexTxt) < 256)
                return OneIndexTxt;
            else
            {
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                byte[] unicodeBytes = Encoding.Unicode.GetBytes(OneIndexTxt);
                byte[] gb2312Bytes = Encoding.Convert(Encoding.Unicode, gb2312, unicodeBytes);
                return GetX(Convert.ToInt32(
                    String.Format("{0:D2}", Convert.ToInt16(gb2312Bytes[0]) - 160)
                    + String.Format("{0:D2}", Convert.ToInt16(gb2312Bytes[1]) - 160)
                    ));
            }

        }

        /// <summary>
        /// 根据区位得到首字母
        /// </summary>
        public static String GetX(int GBCode)
        {
            if (GBCode >= 1601 && GBCode < 1637) return "A";
            if (GBCode >= 1637 && GBCode < 1833) return "B";
            if (GBCode >= 1833 && GBCode < 2078) return "C";
            if (GBCode >= 2078 && GBCode < 2274) return "D";
            if (GBCode >= 2274 && GBCode < 2302) return "E";
            if (GBCode >= 2302 && GBCode < 2433) return "F";
            if (GBCode >= 2433 && GBCode < 2594) return "G";
            if (GBCode >= 2594 && GBCode < 2787) return "H";
            if (GBCode >= 2787 && GBCode < 3106) return "J";
            if (GBCode >= 3106 && GBCode < 3212) return "K";
            if (GBCode >= 3212 && GBCode < 3472) return "L";
            if (GBCode >= 3472 && GBCode < 3635) return "M";
            if (GBCode >= 3635 && GBCode < 3722) return "N";
            if (GBCode >= 3722 && GBCode < 3730) return "O";
            if (GBCode >= 3730 && GBCode < 3858) return "P";
            if (GBCode >= 3858 && GBCode < 4027) return "Q";
            if (GBCode >= 4027 && GBCode < 4086) return "R";
            if (GBCode >= 4086 && GBCode < 4390) return "S";
            if (GBCode >= 4390 && GBCode < 4558) return "T";
            if (GBCode >= 4558 && GBCode < 4684) return "W";
            if (GBCode >= 4684 && GBCode < 4925) return "X";
            if (GBCode >= 4925 && GBCode < 5249) return "Y";
            if (GBCode >= 5249 && GBCode <= 5589) return "Z";
            return " ";
        }

        /// <summary>
        /// 截取过长标题
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="length">限制长度</param>
        /// <returns></returns>
        public static string GetSubString(string title, int length)
        {
            int totalLength = 0;
            int currentIndex = 0;
            while (totalLength < length && currentIndex < title.Length)
            {
                if (title[currentIndex] < 0 || title[currentIndex] > 255)
                    totalLength += 2;
                else
                    totalLength++;

                currentIndex++;
            }

            if (currentIndex < title.Length)
                return title.Substring(0, currentIndex) + "...";
            else
                return title.ToString();
        }

        /// <summary>
        /// 正则式判断，是否为合法文件格式，是则返回Ture,否则返回False; 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsInvalidFileName(string fileName)
        {
            if (fileName.Length > 255)
            {
                return false;
            }
            else
            {
                Regex regex = new Regex(@"[/\\<>*?]");
                return regex.IsMatch(fileName) ? false : true;
            }
        }


        /// <summary>
        /// 取得指定长度的子串，可识别中文字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len">长度，中文为2</param>
        /// <param name="p_TailString">接在尾部的字符串</param>
        /// <returns></returns>
        public static string GetUnicodeSubString(string str, int len, string p_TailString)
        {
            string result = string.Empty;// 最终返回的结果
            int byteLen = System.Text.Encoding.Default.GetByteCount(str);// 单字节字符长度
            int charLen = str.Length;// 把字符平等对待时的字符串长度
            int byteCount = 0;// 记录读取进度
            int pos = 0;// 记录截取位置
            if (byteLen > len)
            {
                for (int i = 0; i < charLen; i++)
                {
                    if (Convert.ToInt32(str.ToCharArray()[i]) > 255)// 按中文字符计算加2
                        byteCount += 2;
                    else// 按英文字符计算加1
                        byteCount += 1;
                    if (byteCount > len)// 超出时只记下上一个有效位置
                    {
                        pos = i;
                        break;
                    }
                    else if (byteCount == len)// 记下当前位置
                    {
                        pos = i + 1;
                        break;
                    }
                }

                if (pos >= 0)
                    result = str.Substring(0, pos) + p_TailString;
            }
            else
                result = str;

            return result;
        }

        /// <summary>
        /// 合并字符
        /// </summary>
        /// <param name="source">要合并的源字符串</param>
        /// <param name="target">要被合并到的目的字符串</param>
        /// <param name="mergechar">合并符</param>
        /// <returns>合并到的目的字符串</returns>
        public static string MergeString(string source, string target)
        {
            return MergeString(source, target, ",");
        }

        /// <summary>
        /// 合并字符
        /// </summary>
        /// <param name="source">要合并的源字符串</param>
        /// <param name="target">要被合并到的目的字符串</param>
        /// <param name="mergechar">合并符</param>
        /// <returns>并到字符串</returns>
        public static string MergeString(string source, string target, string mergechar)
        {
            if (string.IsNullOrEmpty(target))
            {
                target = source;
            }
            else
            {
                target += mergechar + source;
            }
            return target;
        }

        /// <summary>
        /// 隐藏手机号码，中间隐藏4位
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static string HideMobile(string mobile)
        {
            if (mobile.Length == 11)
            {
                mobile = mobile.Substring(0, 3) + "****" + mobile.Substring(7, 4);
            }

            return mobile;
        }

        /// <summary>
        /// 隐藏用户名，格式：a**b
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string HideUserName(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                return Utils.LeftStr(username, 1) + "**" + Utils.RightStr(username, 1);
            }
            else
            {
                return "";
            }
        }

        public static System.Collections.Generic.List<string> GetDataReaderField(System.Data.IDataReader dr)
        {
            System.Collections.Generic.List<string> fields = new System.Collections.Generic.List<string>();

            for (int i = 0; i < dr.FieldCount; i++)
            {
                fields.Add(dr.GetName(i));
            }

            return fields;
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string getExtendName(string filename)
        {
            if (filename != "")
            {
                int i = filename.LastIndexOf(".");
                string extendname = filename.Substring(i);
                return extendname.ToLower();
            }
            else
            {
                return "没选择文件";
            }

        }

        /// <summary>
        /// 获取文件真实名字
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string getRealName(string filename)
        {
            if (filename != "")
            {
                int i = filename.LastIndexOf("\\");
                string realname = filename.Substring(i + 1);
                return realname;
            }
            else
            {
                return "没选择文件";
            }

        }

        #region 获得配置文件节点XML文件的绝对路径
        public static string GetXmlMapPath(string xmlName)
        {
            return GetMapPath(ConfigurationManager.AppSettings[xmlName].ToString());
        }
        #endregion

        public static List<T> GetRandomList<T>(List<T> inputList, int topNum)
        {   
            List<T> outputList = new List<T>();
            Random rd = new Random(DateTime.Now.Millisecond);
            int random = inputList.Count;
            while (topNum > 0)
            {
                //Select an index and item       
                int rdIndex = rd.Next(0, random - 1);
                T add = inputList[rdIndex];
                outputList.Add(add);
                topNum--;
                random--;
            }
            return outputList;
        }

        /// <summary>
        /// 判断是否为金额
        ///     2013-10-9 陈腾龙 新增
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static bool IsMoney(string money)
        {
            if (string.IsNullOrEmpty(money))
            {
                return false;
            }
            Regex moneyFormat = new Regex(@"^[+-]?\d+\.?\d+$");
            return moneyFormat.IsMatch(money);
        }
        /// <summary>
        /// 返回文件扩展名，不含“.”
        /// </summary>
        /// <param name="_filepath">文件全名称</param>
        /// <returns>string</returns>
        public static string GetFileExt(string _filepath)
        {
            if (string.IsNullOrEmpty(_filepath))
            {
                return "";
            }
            if (_filepath.LastIndexOf(".") > 0)
            {
                return _filepath.Substring(_filepath.LastIndexOf(".") + 1); //文件扩展名，不含“.”
            }
            return "";
        }

        /// <summary>
        /// 返回文件名，不含路径
        /// </summary>
        /// <param name="_filepath">文件相对路径</param>
        /// <returns>string</returns>
        public static string GetFileName(string _filepath)
        {
            return _filepath.Substring(_filepath.LastIndexOf(@"/") + 1);
        }

        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="_filepath">文件相对路径</param>
        /// <returns>bool</returns>
        public static bool FileExists(string _filepath)
        {
            string fullpath = GetMapPath(_filepath);
            if (File.Exists(fullpath))
            {
                return true;
            }
            return false;
        }

       
        #region 生成日期随机码
        /// <summary>
        /// 生成日期随机码
        /// </summary>
        /// <returns></returns>
        public static string GetRamCode()
        {
            #region
            return DateTime.Now.ToString("yyyyMMddHHmmssffff");
            #endregion
        }
        #endregion
        #region 删除最后结尾的指定字符后的字符
        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            if (str.LastIndexOf(strchar) >= 0 && str.LastIndexOf(strchar) == str.Length - 1)
            {
                return str.Substring(0, str.LastIndexOf(strchar));
            }
            return str;
        }
        #endregion
        #region 读取或写入cookie
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = UrlEncode(strValue);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string key, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = UrlEncode(strValue);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string key, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = UrlEncode(strValue);
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = UrlEncode(strValue);
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
                return UrlDecode(HttpContext.Current.Request.Cookies[strName].Value.ToString());
            return "";
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName, string key)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
                return UrlDecode(HttpContext.Current.Request.Cookies[strName][key].ToString());

            return "";
        }
        #endregion
        #region 替换指定的字符串
        /// <summary>
        /// 替换指定的字符串
        /// </summary>
        /// <param name="originalStr">原字符串</param>
        /// <param name="oldStr">旧字符串</param>
        /// <param name="newStr">新字符串</param>
        /// <returns></returns>
        public static string ReplaceStr(string originalStr, string oldStr, string newStr)
        {
            if (string.IsNullOrEmpty(oldStr))
            {
                return "";
            }
            return originalStr.Replace(oldStr, newStr);
        }
        #endregion

        #region 显示分页
        /// <summary>
        /// 返回分页页码
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="linkUrl">链接地址，__id__代表页码</param>
        /// <param name="centSize">中间页码数量</param>
        /// <returns></returns>
        public static string OutPageList(int pageSize, int pageIndex, int totalCount, string linkUrl, int centSize)
        {
            //计算页数
            if (totalCount < 1 || pageSize < 1)
            {
                return "";
            }
            int pageCount = totalCount / pageSize;
            if (pageCount < 1)
            {
                return "";
            }
            if (totalCount % pageSize > 0)
            {
                pageCount += 1;
            }
            if (pageCount <= 1)
            {
                return "";
            }
            StringBuilder pageStr = new StringBuilder();
            string pageId = "__id__";
            string firstBtn = "<a href=\"" + ReplaceStr(linkUrl, pageId, (pageIndex - 1).ToString()) + "\">«上一页</a>";
            string lastBtn = "<a href=\"" + ReplaceStr(linkUrl, pageId, (pageIndex + 1).ToString()) + "\">下一页»</a>";
            string firstStr = "<a href=\"" + ReplaceStr(linkUrl, pageId, "1") + "\">1</a>";
            string lastStr = "<a href=\"" + ReplaceStr(linkUrl, pageId, pageCount.ToString()) + "\">" + pageCount.ToString() + "</a>";

            if (pageIndex <= 1)
            {
                firstBtn = "<span class=\"disabled\">«上一页</span>";
            }
            if (pageIndex >= pageCount)
            {
                lastBtn = "<span class=\"disabled\">下一页»</span>";
            }
            if (pageIndex == 1)
            {
                firstStr = "<span class=\"current\">1</span>";
            }
            if (pageIndex == pageCount)
            {
                lastStr = "<span class=\"current\">" + pageCount.ToString() + "</span>";
            }
            int firstNum = pageIndex - (centSize / 2); //中间开始的页码
            if (pageIndex < centSize)
                firstNum = 2;
            int lastNum = pageIndex + centSize - ((centSize / 2) + 1); //中间结束的页码
            if (lastNum >= pageCount)
                lastNum = pageCount - 1;
            pageStr.Append("<span>共" + totalCount + "记录</span>");
            pageStr.Append(firstBtn + firstStr);
            if (pageIndex >= centSize)
            {
                pageStr.Append("<span>...</span>\n");
            }
            for (int i = firstNum; i <= lastNum; i++)
            {
                if (i == pageIndex)
                {
                    pageStr.Append("<span class=\"current\">" + i + "</span>");
                }
                else
                {
                    pageStr.Append("<a href=\"" + ReplaceStr(linkUrl, pageId, i.ToString()) + "\">" + i + "</a>");
                }
            }
            if (pageCount - pageIndex > centSize - ((centSize / 2)))
            {
                pageStr.Append("<span>...</span>");
            }
            pageStr.Append(lastStr + lastBtn);
            return pageStr.ToString();
        }
        #endregion

        #region URL处理
        /// <summary>
        /// URL字符编码
        /// </summary>
        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            str = str.Replace("'", "");
            return HttpContext.Current.Server.UrlEncode(str);
        }

        /// <summary>
        /// URL字符解码
        /// </summary>
        public static string UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return HttpContext.Current.Server.UrlDecode(str);
        }

        /// <summary>
        /// 组合URL参数
        /// </summary>
        /// <param name="_url">页面地址</param>
        /// <param name="_keys">参数名称</param>
        /// <param name="_values">参数值</param>
        /// <returns>String</returns>
        public static string CombUrlTxt(string _url, string _keys, params string[] _values)
        {
            StringBuilder urlParams = new StringBuilder();
            try
            {
                string[] keyArr = _keys.Split(new char[] { '&' });
                for (int i = 0; i < keyArr.Length; i++)
                {
                    if (!string.IsNullOrEmpty(_values[i]) && _values[i] != "0")
                    {
                        _values[i] = UrlEncode(_values[i]);
                        urlParams.Append(string.Format(keyArr[i], _values) + "&");
                    }
                }
                if (!string.IsNullOrEmpty(urlParams.ToString()) && _url.IndexOf("?") == -1)
                    urlParams.Insert(0, "?");
            }
            catch
            {
                return _url;
            }
            return _url + DelLastChar(urlParams.ToString(), "&");
        }
        #endregion

        #region URL请求数据
        /// <summary>
        /// HTTP POST方式请求数据
        /// </summary>
        /// <param name="url">URL.</param>
        /// <param name="param">POST的数据</param>
        /// <returns></returns>
        public static string HttpPost(string url, string param)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;

            StreamWriter requestStream = null;
            WebResponse response = null;
            string responseStr = null;

            try
            {
                requestStream = new StreamWriter(request.GetRequestStream());
                requestStream.Write(param);
                requestStream.Close();

                response = request.GetResponse();
                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                request = null;
                requestStream = null;
                response = null;
            }

            return responseStr;
        }

        /// <summary>
        /// HTTP GET方式请求数据.
        /// </summary>
        /// <param name="url">URL.</param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            //request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;

            WebResponse response = null;
            string responseStr = null;

            try
            {
                response = request.GetResponse();

                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                request = null;
                response = null;
            }

            return responseStr;
        }

        /// <summary>
        /// 执行URL获取页面内容
        /// </summary>
        public static string UrlExecute(string urlPath)
        {
            if (string.IsNullOrEmpty(urlPath))
            {
                return "error";
            }
            StringWriter sw = new StringWriter();
            try
            {
                HttpContext.Current.Server.Execute(urlPath, sw);
                return sw.ToString();
            }
            catch (Exception)
            {
                return "error";
            }
            finally
            {
                sw.Close();
                sw.Dispose();
            }
        }
        #endregion
    }
}
