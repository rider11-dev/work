
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

        TEntity GetById(dynamic pkId, IDbTransaction trans = null);

        int Count(object predicate, IDbTransaction trans = null);

        IEnumerable<TEntity> GetList(object predicate, IList<ISort> sort = null, IDbTransaction trans = null);

        IEnumerable<TEntity> GetPageList(int pageIndex, int pageSize, out long total, IList<ISort> sort, object predicate = null, IDbTransaction trans = null);

        dynamic Insert(TEntity entity, IDbTransaction trans = null);

        void InsertBatch(IEnumerable<TEntity> entities, IDbTransaction trans = null);

        bool Delete(object predicate, IDbTransaction trans = null);

        bool Update(TEntity entity, IDbTransaction trans = null);

    }
}
