using DapperExtensions;
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

namespace OneCardSln.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        static ILogHelper<BaseRepository<TEntity>> _logHelper = LogHelperFactory.GetLogHelper<BaseRepository<TEntity>>();
        public IDbSession DbSession { get; private set; }

        public BaseRepository(IDbSession session)
        {
            this.DbSession = session;
        }

        public TEntity GetById(dynamic pkId)
        {
            try
            {
                return this.DbSession.Connection.Get<TEntity>(pkId as object);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                throw;
            }
        }

        public TReturn GetById<TReturn>(dynamic pkId) where TReturn : class
        {
            try
            {
                return this.DbSession.Connection.Get<TReturn>(pkId as object);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                throw;
            }
        }

        public IEnumerable<TEntity> GetByIds(IList<dynamic> pkIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TReturn> GetByIds<TReturn>(IList<dynamic> pkIds) where TReturn : class
        {
            throw new NotImplementedException();
        }

        public int Count(object predicate)
        {
            try
            {
                return this.DbSession.Connection.Count<TEntity>(predicate);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                throw;
            }
        }

        public IEnumerable<TEntity> GetList(object predicate = null, IList<ISort> sort = null)
        {
            try
            {
                return this.DbSession.Connection.GetList<TEntity>(predicate, sort);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                throw;
            }
        }

        public IEnumerable<TReturn> GetList<TReturn>(object predicate = null, IList<ISort> sort = null) where TReturn : class
        {
            try
            {
                return this.DbSession.Connection.GetList<TReturn>(predicate, sort);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                throw;
            }
        }

        public IEnumerable<TEntity> GetPageList(int pageIndex, int pageSize, out long totalRecords, IList<ISort> sort, object predicate = null)
        {
            try
            {
                totalRecords = this.DbSession.Connection.Count<TEntity>(predicate);
                var entities = this.DbSession.Connection.GetPage<TEntity>(predicate, sort, pageIndex, pageSize);
                return entities;
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                throw;
            }
        }

        public IEnumerable<TReturn> GetPageList<TReturn>(int pageIndex, int pageSize, out long totalRecords, IList<ISort> sort, object predicate = null) where TReturn : class
        {
            try
            {
                var entities = this.DbSession.Connection.GetPage<TReturn>(predicate, sort, pageIndex, pageSize);
                totalRecords = entities.Count();
                return entities;
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                throw;
            }
        }

        public dynamic Insert(TEntity entity, IDbTransaction transaction = null)
        {
            try
            {
                return this.DbSession.Connection.Insert<TEntity>(entity, transaction);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                throw;
            }
        }

        public void InsertBatch(IEnumerable<TEntity> entities, IDbTransaction transaction = null)
        {
            try
            {
                this.DbSession.Connection.Insert<TEntity>(entities, transaction);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                throw;
            }
        }

        public bool Update(TEntity entity, IDbTransaction transaction = null)
        {
            try
            {
                return this.DbSession.Connection.Update(entity, transaction);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                throw;
            }
        }

        public bool UpdateBatch(IEnumerable<TEntity> entities, IDbTransaction transaction = null)
        {
            bool succeed = false;
            foreach (var item in entities)
            {
                Update(item, transaction);
            }
            succeed = true; 
            return succeed;
        }
        public bool DeleteBatch(object predicate, IDbTransaction transaction = null)
        {
            try
            {
                return this.DbSession.Connection.Delete<TEntity>(predicate, transaction);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                throw;
            }
        }
    }
}
