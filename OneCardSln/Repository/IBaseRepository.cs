using DapperExtensions;
using OneCardSln.Repository.Db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Repository
{
    /// <summary>
    /// Repository接口
    /// </summary>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IDbSession DbSession { get; }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="pkId"></param>
        /// <returns></returns>
        TEntity GetById(dynamic pkId);

        /// <summary>
        /// 根据主键获取指定类型实体
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="pkId"></param>
        /// <returns></returns>
        TReturn GetById<TReturn>(dynamic pkId) where TReturn : class;

        /// <summary>
        /// 根据主键列表 获得实体对象集合
        /// </summary>
        /// <param name="pkIds"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetByIds(IList<dynamic> pkIds);

        /// <summary>
        /// 根据主键列表 获得指定实体对象集合
        /// </summary>
        /// <param name="pkIds"></param>
        /// <returns></returns>
        IEnumerable<TReturn> GetByIds<TReturn>(IList<dynamic> pkIds) where TReturn : class;

        /// <summary>
        /// 返回数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(object predicate);

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetList(object predicate = null, IList<ISort> sort = null);

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IEnumerable<TReturn> GetList<TReturn>(object predicate = null, IList<ISort> sort = null) where TReturn : class;

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetPageList(int pageIndex, int pageSize, out long totalRecords, IList<ISort> sort, object predicate = null);

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IEnumerable<TReturn> GetPageList<TReturn>(int pageIndex, int pageSize, out long totalRecords, IList<ISort> sort, object predicate = null) where TReturn : class;

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        dynamic Insert(TEntity entity, IDbTransaction transaction = null);


        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        void InsertBatch(IEnumerable<TEntity> entities, IDbTransaction transaction = null);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        bool Update(TEntity entity, IDbTransaction transaction = null);

        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        bool UpdateBatch(IEnumerable<TEntity> entities, IDbTransaction transaction = null);

        /// <summary>
        ///根据条件 批量删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        bool DeleteBatch(object predicate, IDbTransaction transaction = null);

    }
}
