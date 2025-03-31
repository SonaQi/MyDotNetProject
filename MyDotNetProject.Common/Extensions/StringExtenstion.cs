using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyDotNetProject.Common.Extensions
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringExtenstion
    {
        #region 常用转换

        #region To int

        /// <summary>
        /// 字符串转int，转换失败返回0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt(this string str)
        {
            return str.ToInt(0);
        }

        /// <summary>
        /// 字符串转int，换转失败返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt(this string str, int defaultValue)
        {
            int v;
            if (int.TryParse(str, out v))
            {
                return v;
            }
            else
                return defaultValue;
        }

        /// <summary>
        /// 字符串转int，转换失败返回Null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int? ToIntOrNull(this string str)
        {
            return str.ToIntOrNull(null);
        }

        /// <summary>
        /// 字符串转int，转换失败返回可空
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int? ToIntOrNull(this string str, int? defaultValue)
        {
            int v;
            if (int.TryParse(str, out v))
                return v;
            else
                return defaultValue;
        }

        /// <summary>
        /// 字符串智能转换int，取字符串中的第一个数位串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int SmartToInt(this string str, int defaultValue)
        {
            if (string.IsNullOrEmpty(str))
            {
                return defaultValue;
            }

            Match ma = Regex.Match(str, @"((-\s*)?\d+)");
            if (ma.Success)
            {
                return ma.Groups[1].Value.Replace(" ", "").ToInt(defaultValue);
            }
            else
            {
                return defaultValue;
            }
        }
        #endregion

        #region To long

        /// <summary>
        /// 字符串转long，转换失败返回0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long ToLong(this string str)
        {
            return str.ToLong(0);
        }

        /// <summary>
        /// 字符串转long，转换失败返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToLong(this string str, long defaultValue)
        {
            long v;
            if (long.TryParse(str, out v))
            {
                return v;
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 字符串转long，转换失败返回可空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long? ToLongOrNull(this string str)
        {
            long temp;
            if (long.TryParse(str, out temp))
            {
                return temp;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region To ulong

        /// <summary>
        /// 字符串转ulong，转换失败返回0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static ulong ToUlong(this string str)
        {
            return str.ToUlong(0);
        }

        /// <summary>
        /// 字符串转ulong，转换失败返回Null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static ulong? ToUlongOrNull(this string str)
        {
            ulong temp;
            if (ulong.TryParse(str, out temp))
                return temp;
            else
                return null;
        }

        /// <summary>
        /// 字符串转ulong，转换失败返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ulong ToUlong(this string str, ulong defaultValue)
        {
            ulong v;
            if (ulong.TryParse(str, out v))
                return v;
            else
                return defaultValue;
        }

        #endregion

        #region To Float

        /// <summary>
        /// 字符串转float，转换失败返回0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float ToFloat(this string str)
        {
            return str.ToFloat(0f);
        }

        /// <summary>
        /// 字符串转float，转换失败返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float ToFloat(this string str, float defaultValue)
        {
            float v;
            if (float.TryParse(str, out v))
            {
                return v;
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 智能转换float
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float SmartToFloat(this string str, float defaultValue)
        {
            if (string.IsNullOrEmpty(str))
            {
                return defaultValue;
            }

            Regex rx = new Regex(@"((-\s*)?(\d+)(\.?((?=\d)\d+))?(e[\+-]?\d+)*)");
            Match ma = rx.Match(str);
            if (ma.Success)
            {
                return ma.Groups[1].Value.Replace(" ", "").ToFloat(defaultValue);
            }
            else
            {
                return defaultValue;
            }
        }

        #endregion

        #region To Double

        /// <summary>
        /// 字符串转double，转换失败返回0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double ToDouble(this string str)
        {
            return str.ToDouble(0);
        }

        /// <summary>
        /// 字符串转double，转换失败返回Null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double? ToDoubleOrNull(this string str)
        {
            double temp;
            if (double.TryParse(str, out temp))
            {
                return temp;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 字符串转double，转换失败返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToDouble(this string str, double defaultValue)
        {
            double v;
            if (double.TryParse(str, NumberStyles.Any, null, out v))
            {
                return v;
            }
            else
            {
                return defaultValue;
            }
        }

        #endregion

        #region To Decimal

        /// <summary>
        /// 字符串转decimal，转换失败返回0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str)
        {
            return str.ToDecimal(0);
        }

        /// <summary>
        /// 字符串转decimal，转换失败返回Null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal? ToDecimalOrNull(this string str)
        {
            decimal temp;
            if (decimal.TryParse(str, out temp))
            {
                return temp;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 字符串转decimal，转换失败返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str, decimal defaultValue)
        {
            decimal v;
            if (decimal.TryParse(str, NumberStyles.Any, null, out v))
            {
                return v;
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 智能转decimal，转换失败返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal SmartToDecimal(this string str, decimal defaultValue)
        {
            if (string.IsNullOrEmpty(str))
            {
                return defaultValue;
            }

            Regex rx = new Regex(@"((-\s*)?(\d+)(\.?((?=\d)\d+))?(e[\+-]?\d+)*)");
            Match ma = rx.Match(str);
            if (ma.Success)
            {
                return ma.Groups[1].Value.Replace(" ", "").ToDecimal(defaultValue);
            }
            else
            {
                return defaultValue;
            }
        }

        #endregion

        #region To byte

        /// <summary>
        /// 字符串转byte，转换失败时返回0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte ToByte(this string str)
        {
            return str.ToByte(0);
        }

        /// <summary>
        /// 字符串转byte，转换失败时返回指定默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static byte ToByte(this string str, byte defaultValue)
        {
            byte v;
            if (byte.TryParse(str, out v))
            {
                return v;
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 字符串转byte，转换失败时返回Null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte? ToByteOrNull(this string str)
        {
            return str.ToByteOrNull(null);
        }

        /// <summary>
        /// 字符串转byte，转换失败时返回可空类型
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static byte? ToByteOrNull(this string str, byte? defaultValue)
        {
            byte v;
            if (byte.TryParse(str, out v))
            {
                return v;
            }
            else
            {
                return defaultValue;
            }
        }

        #endregion

        #region To byte[]

        /// <summary>
        /// 字符串转byte数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this string str)
        {
            return System.Text.Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        /// byte数组转字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FromByteArray(this byte[] input)
        {
            return System.Text.Encoding.UTF8.GetString(input);
        }

        #endregion

        #region To Bool

        /// <summary>
        /// 只有'True'或'true'可以转换，'Y'或'1'不可以转换，转换不成功返回false
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ToBool(this string str)
        {
            return str.ToBool(false);
        }

        /// <summary>
        /// 只有'True'或'true'可以转换，'Y'或'1'不可以转换，转换不成功返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ToBool(this string str, bool defaultValue)
        {
            bool b;
            if (bool.TryParse(str, out b))
            {
                return b;
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 只有'True'或'true'可以转换，'Y'或'1'不可以转换，转换不成功则返回null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool? ToBoolOrNull(this string str)
        {
            bool temp;
            if (bool.TryParse(str, out temp))
                return temp;
            else
                return null;
        }

        /// <summary>
        /// 'True','true','Y'和'1'都可以转换，转换不成功返回false
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ToBoolNew(this string s)
        {
            if (s == null)
            {
                return false;
            }
            s = s.Trim();

            return string.Equals(s, "True", StringComparison.CurrentCultureIgnoreCase)
                || string.Equals(s, "Y", StringComparison.CurrentCultureIgnoreCase)
                || s == "1";
        }

        #endregion

        #region To DateTime
        /// <summary>
        /// 转换时间，如果转换失败则返回预设值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime? ToDateTimeOrNull(this string str, DateTime? defaultValue = null)
        {
            DateTime d;
            if (DateTime.TryParse(str, out d))
            {
                return d;
            }
            else
            {
                if (DateTime.TryParseExact(str, new string[] { "yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss", "yyyyMMdd", "yyyyMMdd HH:mm:ss", "yyyy/MM/dd", "yyyy'/'MM'/'dd HH:mm:ss", "MM'/'dd'/'yyyy HH:mm:ss", "yyyy-M-d", "yyy-M-d hh:mm:ss" }, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out d))
                {
                    return d;
                }
                else
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// 转换时间，按给定的日期格式进行转换，转换失败返回Null
        /// </summary>
        /// <param name="str"></param>
        /// <param name="dateFmt"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime? ToDateTimeOrNull(this string str, string dateFmt, DateTime? defaultValue = null)
        {
            DateTime d;
            if (DateTime.TryParseExact(str, dateFmt, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out d))
            {
                return d;
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 转换时间，如果转换失败则返回defaultValue
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str, DateTime defaultValue = default(DateTime))
        {
            DateTime d;
            if (DateTime.TryParse(str, out d))
            {
                return d;
            }
            else
            {
                if (DateTime.TryParseExact(str, new string[] { "yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss", "yyyyMMdd", "yyyyMMdd HH:mm:ss", "yyyy/MM/dd", "yyyy/MM/dd HH:mm:ss", "MM/dd/yyyy HH:mm:ss" }, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out d))
                {
                    return d;
                }
                else
                {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// 转换时间，按给定的日期格式进行转换
        /// </summary>
        /// <param name="str"></param>
        /// <param name="dateFmt"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str, string dateFmt, DateTime defaultValue)
        {
            DateTime d;
            if (DateTime.TryParseExact(str, dateFmt, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out d))
            {
                return d;
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 转换时间，如果转换失败则返回Null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime? ToDateTimeOrNull(this string str)
        {
            return str.ToDateTimeOrNull(null);
        }

        /// <summary>
        /// 转换时间，如果转换失败则返回当前时间
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str)
        {
            return str.ToDateTime(DateTime.Now);
        }

        #endregion

        #region To TimeSpan

        /// <summary>
        /// 字符串转TimeSpan
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(this string str)
        {
            return str.ToTimeSpan(new TimeSpan());
        }

        /// <summary>
        /// 字符串转TimeSpan，转换失败时返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(this string str, TimeSpan defaultValue)
        {
            TimeSpan t;
            if (TimeSpan.TryParse(str, out t))
            {
                return t;
            }
            else
            {
                return defaultValue;
            }
        }

        #endregion

        #region To Enum

        /// <summary>
        /// int转枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this int value) where T : struct, IComparable, IConvertible, IFormattable
        {
            return value.ToEnum<T>(default(T));
        }

        /// <summary>
        /// int转枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this int value, T defaultValue) where T : struct, IComparable, IConvertible, IFormattable
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new Exception("T 必须是枚举类型");
            }
            if (Enum.IsDefined(type, value))
            {
                return (T)Enum.ToObject(type, value);
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// byte转枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this byte value, T defaultValue) where T : struct, IComparable, IConvertible, IFormattable
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new Exception("T 必须是枚举类型");
            }

            if (Enum.IsDefined(type, value))
            {
                return (T)Enum.ToObject(type, value);
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// byte转枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this byte value) where T : struct, IComparable, IConvertible, IFormattable
        {
            return value.ToEnum<T>(default(T));
        }

        /// <summary>
        /// string转枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, T defaultValue, bool ignoreCase) where T : struct, IComparable, IConvertible, IFormattable
        {
            T o;
            bool flag = Enum.TryParse<T>(value, ignoreCase, out o);
            if (flag && Enum.IsDefined(typeof(T), o))
            {
                return o;
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// string转枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, T defaultValue) where T : struct, IComparable, IConvertible, IFormattable
        {
            return value.ToEnum<T>(defaultValue, true);
        }

        /// <summary>
        /// string转枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value) where T : struct, IComparable, IConvertible, IFormattable
        {
            return value.ToEnum<T>(default(T));
        }

        /// <summary>
        /// string转枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, bool ignoreCase) where T : struct, IComparable, IConvertible, IFormattable
        {
            return value.ToEnum<T>(default(T), ignoreCase);
        }

        /// <summary>
        /// string转枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static int GetEnumValue<T>(this string value, int defaultValue, bool ignoreCase) where T : struct, IComparable, IConvertible, IFormattable
        {
            T o;
            bool flag = Enum.TryParse<T>(value, ignoreCase, out o);
            if (flag)
            {
                return System.Convert.ToInt32(o);
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// string转枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetEnumValue<T>(this string value, int defaultValue) where T : struct, IComparable, IConvertible, IFormattable
        {
            return value.GetEnumValue<T>(defaultValue, true);
        }

        /// <summary>
        /// 获取枚举的值，获取失败时返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetEnumValue<T>(this T value, int defaultValue) where T : struct, IComparable, IConvertible, IFormattable
        {
            if (!typeof(T).IsEnum)
            {
                return defaultValue;
            }
            else
            {
                return Convert.ToInt32(value);
            }
        }

        /// <summary>
        /// 获取枚举的值，获取失败时返回0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetEnumValue<T>(this T value) where T : struct, IComparable, IConvertible, IFormattable
        {
            return value.GetEnumValue<T>(0);
        }

        #endregion

        #region To Guid

        /// <summary>
        /// 字符串转Guid
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string str)
        {
            Guid g;
            if (Guid.TryParse(str, out g))
            {
                return g;
            }
            else
            {
                return Guid.Empty;
            }
        }

        #endregion

        #endregion

        #region 常用判断

        /// <summary>
        /// 判断字符串是否为null或空
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string input)
        {
            return string.IsNullOrEmpty(input);
        }

        /// <summary>
        /// 判断字符串是否不为Null和空
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        /// <summary>
        /// 判断字符串是否为null，空或空白字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// 判断字符串是否不为Null和空
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNotNullOrWhiteSpace(this string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// 判断字符串是否日期时间格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDateTime(this string str)
        {
            DateTime d;
            if (DateTime.TryParseExact(str, new string[] { "yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss", "yyyyMMdd", "yyyyMMdd HH:mm:ss", "yyyy/MM/dd", "yyyy/MM/dd HH:mm:ss", "MM/dd/yyyy", "MM/dd/yyyy HH:mm:ss" }, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out d))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 比较字符串是否相等，不考虑大小写
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string source, string value)
        {
            if (source.IsNullOrEmpty())
            {
                return false;
            }
            return source.Equals(value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 是否含有指定字符串值，忽略大小写
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ContainsIgnoreCase(this string source, string value)
        {
            if (source.IsNullOrEmpty())
            {
                return false;
            }
            return source.Contains(value, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        public static string FormatContent(IEnumerable<string> contents, bool isWithBracket = true, string separator = ",")
        {
            if (isWithBracket)
            {
                return $"【{string.Join(separator, contents)}】";
            }
            return string.Join(separator, contents);
        }


        /// <summary>
        /// ascii码从小到大排序
        /// </summary>
        /// <param name="paramsMap"></param>
        /// <returns></returns>
        public static string GetParamStr(this Dictionary<string, string> paramsMap)
        {
            var vDic = from objDic in paramsMap orderby objDic.Key ascending select objDic;
            StringBuilder str = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in vDic)
            {
                string pkey = kv.Key;
                string pvalue = kv.Value;
                str.Append(pkey + "=" + pvalue + "&");
            }

            var result = str.ToString().Substring(0, str.ToString().Length - 1);
            return result;
        }
    }
}
