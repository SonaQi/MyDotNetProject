using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Common.Abstracts
{
    public interface IModelMapper
    {
        /// <summary>
        ///     Model转换
        /// </summary>
        /// <typeparam name="TResult">TResult</typeparam>
        /// <typeparam name="TModel">TResult</typeparam>
        /// <param name="model">model</param>
        /// <returns></returns>
        TResult MapTo<TResult, TModel>(TModel model);

        /// <summary>
        ///  model转换
        /// </summary>
        /// <typeparam name="TResult">TResult</typeparam>
        /// <typeparam name="TModel">TModel</typeparam>
        /// <param name="result">result</param>
        /// <param name="model">model</param>
        /// <returns></returns>
        TResult MapTo<TResult, TModel>(TResult result, TModel model);
    }
}
