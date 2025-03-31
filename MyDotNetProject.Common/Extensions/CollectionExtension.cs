using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Common.Extensions
{
    /// <summary>
    /// 集合扩展方法
    /// </summary>
    public static class CollectionExtension
    {
        /// <summary>
        /// 集合是否为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> input)
        {
            if (input == null || input.Count == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 集合是否有值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty<T>(this ICollection<T> input)
        {
            return !input.IsNullOrEmpty();
        }

        /// <summary>
        /// list转dataTable(仅针对T中的公共属性)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="isCustomColumnName">默认为false，为true的时候会取自定义的ColumnName</param>
        public static DataTable ToDataTable<T>(this List<T> list)
        {
            if (list == null)
            {
                return null;
            }

            Type type = typeof(T);
            var ps = type.GetProperties();
            Type targetType;
            NullableConverter nullableConvert;
            List<DataColumn> cols = new List<DataColumn>();
            foreach (var p in ps)
            {
                var columnName = p.Name;
                if (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    nullableConvert = new NullableConverter(p.PropertyType);
                    targetType = nullableConvert.UnderlyingType;
                    cols.Add(new DataColumn(columnName, targetType));
                }
                else
                {
                    cols.Add(new DataColumn(columnName, p.PropertyType));
                }
            }

            DataTable dt = new DataTable();
            dt.Columns.AddRange(cols.ToArray());

            list.ForEach((l) =>
            {
                List<object> objs = new List<object>();
                objs.AddRange(ps.Select(p => p.GetValue(l, null)));
                dt.Rows.Add(objs.ToArray());
            });

            return dt;
        }

        /// <summary>
        /// 将集合若干组
        /// </summary>
        /// <param name="source">数据集</param>
        /// <param name="pageSize">每一组大小</param>
        /// <returns></returns>
        public static List<List<T>> SpliteSourceBySize<T>(this List<T> source, int pageSize)
        {
            int listCount = ((source.Count() - 1) / pageSize) + 1;

            // 计算组数 
            List<List<T>> pages = new List<List<T>>();
            for (int pageIndex = 1; pageIndex <= listCount; pageIndex++)
            {
                var page = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                pages.Add(page);
            }
            return pages;
        }

        /// <summary> 
        /// 将集合若干组
        /// </summary>
        /// <param name="source">数据集</param> 
        /// <param name="count">组数</param>
        /// <returns></returns> 
        public static List<List<T>> SpliteSourceByCount<T>(this List<T> source, int count)
        {
            int totalCount = source.Count();
            int pageSize = totalCount / count;//取每一页大小 
            int remainder = totalCount % count;//取余数 
            List<List<T>> pages = new List<List<T>>();
            for (int pageIndex = 1; pageIndex <= count; pageIndex++)
            {
                if (pageIndex != count)
                {
                    var page = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    pages.Add(page);
                }
                else
                {
                    var page = source.Skip((pageIndex - 1) * pageSize).Take(pageSize + remainder).ToList();
                    pages.Add(page);
                }
            }
            return pages;
        }
    }
}
