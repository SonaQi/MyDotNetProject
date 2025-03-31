using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;
using JsonProperty = Newtonsoft.Json.Serialization.JsonProperty;

namespace MyDotNetProject.Common.Extensions
{
    /// <summary>
    /// 序列化扩展
    /// </summary>
    public static class SerializeExtenstion
    {
        /// <summary>
        /// 将对象序列化为JSON
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">序列化对象</param>
        /// <param name="isNeedFormat">是否格式化</param>
        /// <returns>json字符串</returns>
        public static string ToJson<T>(this T obj, bool isNeedFormat = false)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            var format = isNeedFormat ? Formatting.Indented : Formatting.None;
            return JsonConvert.SerializeObject(obj, format);
        }

        /// <summary>
        /// 将对象序列化为JSON
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">序列化对象</param>
        /// <param name="listIgnoreProperty">忽略属性</param>
        /// <param name="isNeedFormat">是否格式化</param>
        /// <returns></returns>
        public static string ToJson<T>(this T obj, List<string> listIgnoreProperty, bool isNeedFormat = false)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            var format = isNeedFormat ? Formatting.Indented : Formatting.None;
            return JsonConvert.SerializeObject(obj, format, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new JsonPropertyContractResolver(listIgnoreProperty)
            });
        }

        /// <summary>
        /// 将JSON字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="jsonString">json字符串</param>
        /// <returns>对象实体</returns>
        public static T FromJson<T>(this string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static Tuple<bool, T> IsJson<T>(this string jsonString)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<T>(jsonString);
                return new Tuple<bool, T>(true, data); // 如果没有抛出异常，说明字符串是有效的JSON
            }
            catch (JsonReaderException)
            {
                return new Tuple<bool, T>(false, default(T)); // 如果抛出了JsonReaderException，说明字符串不是有效的JSON
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class JsonPropertyContractResolver : DefaultContractResolver
    {
        IEnumerable<string> lstIgnore;
        public JsonPropertyContractResolver(IEnumerable<string> ignoreProperties)
        {
            lstIgnore = ignoreProperties;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {

            return base.CreateProperties(type, memberSerialization).ToList().FindAll(p => !lstIgnore.Contains(p.PropertyName));//需要输出的属性  } }
        }
    }
}
