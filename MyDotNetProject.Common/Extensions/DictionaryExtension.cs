using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Common.Extensions
{
    /// <summary>
    /// 字典拓展.
    /// </summary>
    public static class DictionaryExtension
    {
        /// <summary>
        ///  根据键获取字典对应的值.
        /// </summary>
        /// <typeparam name="TKey">键类型.</typeparam>
        /// <typeparam name="TValue">值类型.</typeparam>
        /// <param name="srcs">字典.</param>
        /// <param name="key">查找键</param>
        /// <returns>键对应值(备注:当键不存在时,返回对应类型默认值,注意空指针)</returns>
        public static TValue TryGetValueExt<TKey, TValue>(this IDictionary<TKey, TValue> srcs, TKey key)
        {
            if (srcs.IsNullOrEmpty())
            {
                return default;
            }

            if (srcs.TryGetValue(key, out var value))
            {
                return value;
            }

            return default;
        }

        public static Dictionary<TKey, TElement> ToDictionaryExt<TSource, TKey, TElement>(
            this IEnumerable<TSource> srcs,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> valueSelector)
        {
            var dic = new Dictionary<TKey, TElement>();
            if (srcs == null || keySelector == null || valueSelector == null)
            {
                return dic;
            }

            foreach (var item in srcs)
            {
                dic[keySelector(item)] = valueSelector(item);
            }

            return dic;
        }

        public static Dictionary<TKey, TSource> ToDictionaryExt<TSource, TKey>(
            this IEnumerable<TSource> srcs,
            Func<TSource, TKey> keySelector)
        {
            var dic = new Dictionary<TKey, TSource>();
            if (srcs == null || keySelector == null)
            {
                return dic;
            }

            foreach (var item in srcs)
            {
                dic[keySelector(item)] = item;
            }

            return dic;
        }

        public static Dictionary<string, TSource> ToDictionaryExt<TSource>(
           this IEnumerable<TSource> srcs,
           Func<TSource, string> keySelector)
        {
            var dic = new Dictionary<string, TSource>(StringComparer.OrdinalIgnoreCase);
            if (srcs == null || keySelector == null)
            {
                return dic;
            }

            foreach (var item in srcs)
            {
                dic[keySelector(item)] = item;
            }

            return dic;
        }

        /// <summary>
        ///  获取字典中对象字段值.
        /// </summary>
        /// <typeparam name="TKey">键类型.</typeparam>
        /// <typeparam name="TValue">值类型.</typeparam>
        /// <typeparam name="TFld">自定类型</typeparam>
        /// <param name="srcs">对象字典.</param>
        /// <param name="key">键值.</param>
        /// <param name="fieldSelector">字段选择器.</param>
        /// <returns>返回对应字段值.</returns>
        public static TFld TryGetValueExt<TKey, TValue, TFld>(
            this IDictionary<TKey, TValue> srcs,
            TKey key,
            Func<TValue, TFld> fieldSelector)
            where TValue : class
        {
            var v = srcs.TryGetValueExt(key);
            if (!v.HasValue())
            {
                return default;
            }

            return fieldSelector(v);
        }

        /// <summary>
        /// 判断对象是否是空引用.
        /// </summary>
        /// <typeparam name="T">对象类型.</typeparam>
        /// <param name="value">对象值.</param>
        /// <returns>True:对象有值,False:空对象</returns>
        public static bool HasValue<T>(this T value)
            where T : class
        {
            return value != null;
        }
    }
}
