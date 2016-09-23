using OneCardSln.Repository.Db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using OneCardSln.Components.Logger;
using System.Xml.Linq;

namespace OneCardSln.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        public IDbSession DbSession { get; private set; }
        public virtual SqlConfEntity SqlConf { get; protected set; }

        public BaseRepository(IDbSession session)
        {
            this.DbSession = session;
        }


        public TEntity GetById(dynamic pkId, IDbTransaction trans = null)
        {
            try
            {
                return this.DbSession.Connection.Get<TEntity>(pkId as object, trans);
            }
            catch
            {
                Dispose();

                throw;
            }
        }

        public int Count(object predicate, IDbTransaction trans = null)
        {
            try
            {
                return this.DbSession.Connection.Count<TEntity>(predicate, trans);
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        public IEnumerable<TEntity> GetList(object predicate, IList<ISort> sort = null, IDbTransaction trans = null)
        {
            try
            {
                return this.DbSession.Connection.GetList<TEntity>(predicate, sort, trans);
            }
            catch
            {
                Dispose();

                throw;
            }
        }

        public IEnumerable<TEntity> GetPageList(int pageIndex, int pageSize, out long total, IList<DapperExtensions.ISort> sort, object predicate = null, IDbTransaction trans = null)
        {
            total = 0;
            try
            {
                total = this.DbSession.Connection.Count<TEntity>(predicate, trans);
                return this.DbSession.Connection.GetPage<TEntity>(predicate, sort, pageIndex, pageSize, trans);
            }
            catch
            {
                Dispose();

                throw;
            }
        }

        public IEnumerable<TReturn> GetPageList<TReturn>(int pageIndex, int pageSize, out long total, IList<DapperExtensions.ISort> sort, object predicate = null, IDbTransaction trans = null) where TReturn : class
        {
            total = 0;
            try
            {
                total = this.DbSession.Connection.Count<TEntity>(predicate, trans);
                return this.DbSession.Connection.GetPage<TReturn>(predicate, sort, pageIndex, pageSize, trans);
            }
            catch
            {
                Dispose();

                throw;
            }
        }

        public dynamic Insert(TEntity entity, IDbTransaction trans = null)
        {
            try
            {
                return this.DbSession.Connection.Insert<TEntity>(entity, trans);
            }
            catch
            {
                Dispose();
                throw;
            }
        }
        public void InsertBatch(IEnumerable<TEntity> entities, IDbTransaction trans = null)
        {
            try
            {
                this.DbSession.Connection.Insert<TEntity>(entities, trans);
            }
            catch
            {
                Dispose();

                throw;
            }
        }

        public bool Delete(object predicate, IDbTransaction trans = null)
        {
            try
            {
                return this.DbSession.Connection.Delete<TEntity>(predicate, trans);
            }
            catch
            {
                Dispose();

                throw;
            }
        }

        public bool Update(TEntity entity, IDbTransaction trans = null)
        {
            try
            {
                return this.DbSession.Connection.Update<TEntity>(entity, trans);
            }
            catch
            {
                Dispose();

                throw;
            }
        }

        public IEnumerable<object> Query(Type type, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.DbSession.Connection.Query(type, sql, param, transaction, buffered, commandTimeout, commandType);
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.DbSession.Connection.Query<TFirst, TSecond, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.DbSession.Connection.Query<TFirst, TSecond, TThird, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.DbSession.Connection.Query<TFirst, TSecond, TThird, TFourth, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
            }
            catch
            {
                Dispose();
                throw;
            }
        }


        public int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.DbSession.Connection.Execute(sql, param, transaction, commandTimeout, commandType);
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        public T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                return this.DbSession.Connection.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);
            }
            catch
            {
                Dispose();
                throw;
            }
        }




        public IEnumerable<object> QueryBySqlName(Type type, string sqlName, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Query(type, GetSql(sqlName), param, transaction, buffered, commandTimeout, commandType);
        }

        public IEnumerable<TReturn> QueryBySqlName<TFirst, TSecond, TReturn>(string sqlName, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return Query<TFirst, TSecond, TReturn>(GetSql(sqlName), map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public IEnumerable<TReturn> QueryBySqlName<TFirst, TSecond, TThird, TReturn>(string sqlName, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return Query<TFirst, TSecond, TThird, TReturn>(GetSql(sqlName), map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public IEnumerable<TReturn> QueryBySqlName<TFirst, TSecond, TThird, TFourth, TReturn>(string sqlName, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return Query<TFirst, TSecond, TThird, TFourth, TReturn>(GetSql(sqlName), map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public int ExecuteBySqlName(string sqlName, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Execute(GetSql(sqlName), param, transaction, commandTimeout, commandType);
        }
        public T ExecuteScalarBySqlName<T>(string sqlName, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return ExecuteScalar<T>(GetSql(sqlName), param, transaction, commandTimeout, commandType);
        }

        private string GetSql(string sqlName)
        {
            return SqlTextProvider.GetSql(new SqlConfEntity
            {
                area = SqlConf.area,
                group = SqlConf.group,
                name = sqlName
            });
        }

        public void Dispose()
        {
            this.DbSession.Dispose();
        }


    }
}
