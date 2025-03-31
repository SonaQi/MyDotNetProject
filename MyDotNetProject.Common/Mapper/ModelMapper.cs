using AutoMapper;
using MyDotNetProject.Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Common.Mapper
{
    /// <summary>
    /// Model 转换实现
    /// </summary>
    public class ModelMapper : IModelMapper
    {
        private readonly IMapper _mapper;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="mapper">mapper</param>
        public ModelMapper(IMapper mapper)
        {
            this._mapper = mapper;
        }

        /// <summary>
        ///  对象转换
        /// </summary>
        /// <typeparam name="TResult">TResult</typeparam>
        /// <typeparam name="TModel">TModel</typeparam>
        /// <param name="model">model</param>
        /// <returns><typeparamref name="TResult"/></returns>
        public TResult MapTo<TResult, TModel>(TModel model)
        {
            if (model == null)
            {
                return default(TResult);
            }

            return this._mapper.Map<TResult>(model);
        }

        /// <summary>
        /// 对象转换
        /// </summary>
        /// <typeparam name="TResult">TResult</typeparam>
        /// <typeparam name="TModel">TModel</typeparam>
        /// <param name="result">result</param>
        /// <param name="model">model</param>
        /// <returns><typeparamref name="TResult"/></returns>
        public TResult MapTo<TResult, TModel>(TResult result, TModel model)
        {
            if (model == null)
            {
                return result;
            }

            return this._mapper.Map(model, result);
        }
    }
}
