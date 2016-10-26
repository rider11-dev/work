using MyNet.Components.Logger;
using MyNet.Components.Result;
using MyNet.Repository;
using MyNet.Repository.Db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Service
{
    public class BaseService<TEntity> : IDisposable where TEntity : class
    {
        const string Msg_FindById = "根据主键查询数据";

        protected ILogHelper<BaseService<TEntity>> LogHelper = LogHelperFactory.GetLogHelper<BaseService<TEntity>>();

        private IDbSession _session { get; set; }

        private IBaseRepository<TEntity> _baseRep;

        public BaseService(IDbSession session, IBaseRepository<TEntity> baseRep)
        {
            _session = session;
            _baseRep = baseRep;
        }

        protected DatabaseType DbType
        {
            get { return _session.DbType; }
        }


        protected IDbTransaction Begin(IsolationLevel isolation = IsolationLevel.ReadCommitted)
        {
            return _session.Begin();
        }

        protected void Commit()
        {
            _session.Commit();
        }

        protected void Rollback()
        {
            _session.Rollback();
        }

        public void Dispose()
        {
            _baseRep.Dispose();
            _session.Dispose();
        }

        public OptResult Find(dynamic pkId)
        {
            OptResult rst = null;
            try
            {
                var entity = _baseRep.GetById(pkId as object);
                rst = OptResult.Build(ResultCode.Success, Msg_FindById, entity);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(Msg_FindById, ex);
                rst = OptResult.Build(ResultCode.DbError, Msg_FindById);
            }
            return rst;
        }
    }
}
