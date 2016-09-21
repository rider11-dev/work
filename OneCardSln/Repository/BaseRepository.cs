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


        public TEntity GetById(dynamic pkId, IDbTransaction trans = null)
        {
            try
            {
                return this.DbSession.Connection.Get<TEntity>(pkId as object, trans);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                throw;
            }
        }

        public int Count(object predicate, IDbTransaction trans = null)
        {
            try
            {
                return this.DbSession.Connection.Count<TEntity>(predicate, trans);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                throw;
            }
        }

        public IEnumerable<TEntity> GetList(object predicate, IList<ISort> sort = null, IDbTransaction trans = null)
        {
            try
            {
                return this.DbSession.Connection.GetList<TEntity>(predicate, sort, trans);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
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
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                throw;
            }
        }

        public dynamic Insert(TEntity entity, IDbTransaction trans = null)
        {
            try
            {
                return this.DbSession.Connection.Insert<TEntity>(entity, trans);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                throw;
            }
        }
        public void InsertBatch(IEnumerable<TEntity> entities, IDbTransaction trans = null)
        {
            try
            {
                this.DbSession.Connection.Insert<TEntity>(entities, trans);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                throw;
            }
        }

        public bool Delete(object predicate, IDbTransaction trans = null)
        {
            try
            {
                return this.DbSession.Connection.Delete<TEntity>(predicate, trans);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                throw;
            }
        }

        public bool Update(TEntity entity, IDbTransaction trans = null)
        {
            try
            {
                return this.DbSession.Connection.Update<TEntity>(entity, trans);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                throw;
            }
        }
    }
}
