using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyDotNetProject.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyDotNetProject.Repository.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public DbContext DbContext;

        public BaseRepository(DbContext mydbcontext)
        {
            this.DbContext = mydbcontext as DbContext;
        }


        public IQueryable<T> GetEntities(Expression<Func<T, bool>> exp, bool isNoTracking = false)
        {
            if (isNoTracking)
            {
                return DbContext.Set<T>().AsNoTracking().Where(exp).AsQueryable();
            }
            else
            {
                return DbContext.Set<T>().Where(exp).AsQueryable();
            }
        }

        public IQueryable<T> GetEntities<TS>(Expression<Func<T, bool>> exp, Expression<Func<T, TS>> order, bool descending, bool isNoTracking = false)
        {
            var data = GetEntities(exp, isNoTracking);
            return descending ? data.OrderByDescending(order) : data.OrderBy(order);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        public T GetEntity(Expression<Func<T, bool>> exp, bool isNoTracking = false)
        {
            if (isNoTracking)
            {
                return DbContext.Set<T>().AsNoTracking().FirstOrDefault(exp);
            }
            else
            {
                return DbContext.Set<T>().FirstOrDefault(exp);
            }
        }

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>ID</returns>
        public bool Insert(T entity)
        {
            DbContext.Set<T>().Add(entity);
            return DbContext.SaveChanges() > 0;
        }


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        public bool Update(T entity)
        {
            DbContext.Set<T>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
            return DbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 批量修改实体
        /// </summary>
        /// <param name="entitys">实体</param>
        /// <returns>ID</returns>
        public bool BatchUpdate(List<T> entitys)
        {
            DbContext.Set<T>().AttachRange(entitys);
            foreach (var item in entitys)
            {
                DbContext.Entry(item).State = EntityState.Modified;
            }
            return DbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">ID</param>
        public bool Delete(T entity)
        {
            DbContext.Set<T>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Deleted;
            return DbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public bool BatchDelete(List<T> entitys)
        {
            DbContext.Set<T>().AttachRange(entitys);
            foreach (var item in entitys)
            {
                DbContext.Entry(item).State = EntityState.Deleted;
            }
            return DbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 获取全部集合
        /// </summary>
        /// <returns>集合</returns>
        public IQueryable<T> LoadAll(bool isNoTracking = false)
        {
            if (isNoTracking)
            {
                return DbContext.Set<T>().AsNoTracking();
            }
            else
            {
                return DbContext.Set<T>();
            }
        }

        /// <summary>
        /// 分页获取全部集合
        /// </summary>
        /// <param name="count">记录总数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns>集合</returns>
        public IQueryable<T> LoadAllWithPage(out long count, int pageIndex, int pageSize, bool isNoTracking = false)
        {
            var result = LoadAll(isNoTracking);
            count = result.Count();
            return result.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public IQueryable<T> GetEntities<TS>(out int count, int pageIndex, int pageSize, Expression<Func<T, bool>> func, Expression<Func<T, TS>> order, bool descending, bool isNoTracking = false)
        {
            var source = GetEntities(func, order, descending, isNoTracking);
            count = source.Count();
            return source.Skip((pageIndex - 1) * pageSize).Take(pageSize).AsQueryable();
        }

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>ID</returns>
        public List<T> BatchInsert(List<T> entitys)
        {
            DbContext.Set<T>().AddRange(entitys);
            DbContext.SaveChanges();
            return entitys;
        }
        public T InsertReturnEntity(T entity)
        {
            DbContext.Set<T>().Add(entity);
            DbContext.SaveChanges();
            return entity;
        }

        public bool ExecuteWithTransaction(IServiceScopeFactory serviceScopeFactory, Action<DbContext> action)
        {
            var result = false;
            using (var scope = serviceScopeFactory.CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>())
                {
                    using (var tran = dbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            action(dbContext);
                            dbContext.SaveChanges();
                            tran.Commit();
                            result = true;
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            result = false;
                            throw ex;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="isNoTracking"></param>
        /// <returns></returns>
        public bool IsExist(Expression<Func<T, bool>> exp, bool isNoTracking = false)
        {
            if (isNoTracking)
            {
                return DbContext.Set<T>().AsNoTracking().Any(exp);
            }
            else
            {
                return DbContext.Set<T>().Any(exp);
            }
        }
    }
}
