using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Repository.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> GetEntities(Expression<Func<T, bool>> exp, bool isNoTracking = false);

        IQueryable<T> GetEntities<TS>(Expression<Func<T, bool>> exp, Expression<Func<T, TS>> order, bool descending, bool isNoTracking = false);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        T GetEntity(Expression<Func<T, bool>> exp, bool isNoTracking = false);

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>ID</returns>
        bool Insert(T entity);

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        bool Update(T entity);

        /// <summary>
        /// 批量修改实体
        /// </summary>
        /// <param name="entitys">实体</param>
        /// <returns></returns>
        bool BatchUpdate(List<T> entitys);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">entity</param>
        bool Delete(T entity);

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        bool BatchDelete(List<T> entitys);

        /// <summary>
        /// 获取全部集合
        /// </summary>
        /// <returns>集合</returns>
        IQueryable<T> LoadAll(bool isNoTracking = false);

        /// <summary>
        /// 分页获取全部集合
        /// </summary>
        /// <param name="count">记录总数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns>集合</returns>
        IQueryable<T> LoadAllWithPage(out long count, int pageIndex, int pageSize, bool isNoTracking = false);

        IQueryable<T> GetEntities<TS>(out int count, int pageIndex, int pageSize, Expression<Func<T, bool>> func, Expression<Func<T, TS>> order, bool descending, bool isNoTracking = false);

        /// 批量插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体</returns>
        List<T> BatchInsert(List<T> entitys);

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体</returns>
        T InsertReturnEntity(T entity);


        /// <summary>
        /// EFcore多表操作事务
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool ExecuteWithTransaction(IServiceScopeFactory serviceScopeFactory, Action<DbContext> action);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="isNoTracking"></param>
        /// <returns></returns>
        bool IsExist(Expression<Func<T, bool>> exp, bool isNoTracking = false);
    }
}
