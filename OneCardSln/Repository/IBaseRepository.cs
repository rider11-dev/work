
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
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        IDbSession DbSession { get; }

        #region dapper-extensions
        TEntity GetById(dynamic pkId, IDbTransaction trans = null);
        TReturn GetById<TReturn>(dynamic pkId, IDbTransaction trans = null) where TReturn : class;

        int Count(object predicate, IDbTransaction trans = null);

        IEnumerable<TEntity> GetList(object predicate, IList<ISort> sort = null, IDbTransaction trans = null);

        IEnumerable<TEntity> GetPageList(int pageIndex, int pageSize, out long total, IList<ISort> sort, object predicate = null, IDbTransaction trans = null);

        IEnumerable<TReturn> GetPageList<TReturn>(int pageIndex, int pageSize, out long total, IList<DapperExtensions.ISort> sort, object predicate = null, IDbTransaction trans = null) where TReturn : class;

        dynamic Insert(TEntity entity, IDbTransaction trans = null);

        void InsertBatch(IEnumerable<TEntity> entities, IDbTransaction trans = null);

        bool Delete(object predicate, IDbTransaction trans = null);

        bool Update(TEntity entity, IDbTransaction trans = null);

        #endregion dapper-extensions

        #region dapper

        IEnumerable<object> Query(Type type, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);


        int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);








        IEnumerable<object> QueryBySqlName(Type type, string sqlName, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        IEnumerable<TReturn> QueryBySqlName<TFirst, TSecond, TReturn>(string sqlName, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        IEnumerable<TReturn> QueryBySqlName<TFirst, TSecond, TThird, TReturn>(string sqlName, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);

        IEnumerable<TReturn> QueryBySqlName<TFirst, TSecond, TThird, TFourth, TReturn>(string sqlName, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);


        int ExecuteBySqlName(string sqlName, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        T ExecuteScalarBySqlName<T>(string sqlName, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        int UpdateBySqlName(string sqlName, object param = null, IEnumerable<string> setFields = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        #endregion dapper
    }
}
