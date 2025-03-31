using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyDotNetProject.Common.Mapper
{
    public class UpperUnderscoreNamingConvention : INamingConvention
    {
        /// <summary>
        /// 表达式
        /// </summary>
        public Regex SplittingExpression { get; } = new Regex(@"[\p{Ll}\p{Lu}0-9]+(?=_?)");

        /// <summary>
        /// 分隔符
        /// </summary>
        public string SeparatorCharacter => "_";

        /// <summary>
        /// 替换值
        /// </summary>
        /// <param name="match">匹配</param>
        /// <returns>返回值</returns>
        public string ReplaceValue(Match match) => match.Value.ToUpper();
    }
}
