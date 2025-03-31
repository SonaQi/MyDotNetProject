using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Common.Extensions
{
    /// <summary>
    /// 枚举扩展类
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举的描述信息
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns>枚举描述</returns>
        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();

            var name = Enum.GetName(type, value);
            if (string.IsNullOrEmpty(name))
                return null;

            var fieldInfo = type.GetField(name);
            if (fieldInfo == null)
                return null;

            var attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute), false) as DescriptionAttribute;
            if (attribute == null)
                return null;

            return attribute.Description;
        }

        /// <summary>
        /// 获取枚举的名称
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <returns>枚举名称</returns>
        public static string GetEnumName(this Enum value)
        {
            var type = value.GetType();

            return Enum.GetName(type, value);
        }

        /// <summary>
        /// 将对象转换为指定类型的枚举变量
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="enumType">枚举类型</param>
        /// <returns>枚举</returns>
        public static object ToEnum(this object value, Type enumType)
        {
            if (!enumType.IsEnum) return null;
            try
            {
                object newObject;
                var fields = enumType.GetFields();
                var field = fields.Where(t => t.FieldType.FullName.Equals("System.Int32")).First();
                value = Convert.ChangeType(value, field.FieldType);
                if (Enum.IsDefined(enumType, value))
                    newObject = Enum.Parse(enumType, value.ToString(), true);
                else if (fields.Length < 2)
                    newObject = Enum.Parse(enumType, "0");
                else
                    newObject = Enum.Parse(enumType, fields[1].Name);
                return newObject;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return Enum.Parse(enumType, "0");
        }

        /// <summary>
        /// 将对象转换为指定类型的枚举变量
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns>枚举</returns>
        public static T ToEnum<T>(this object value)
            where T : struct
        {
            var res = value.ToEnum(typeof(T));
            return res == null ? default(T) : (T)res;
        }

        /// <summary>
        /// 获取枚举集合
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<string> GetEnumValue(this Type enumType)
        {
            if (!enumType.IsEnum)
            {
                return new List<string>();
            }

            //获得枚举的字段信息（因为枚举的值实际上是一个static的字段的值）
            var fields = enumType.GetFields().Where(p => p.FieldType.IsEnum);
            var list = fields.Select(x => Convert.ToInt64(x.GetValue(null)).ToString()).ToList();
            return list;
        }
    }
}
