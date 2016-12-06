using MyNet.Components.Mapper;
using MyNet.Components.Result;
using MyNet.CustomQuery.Model;
using MyNet.CustomQuery.Service;
using MyNet.CustomQuery.WebApi.Models;
using MyNet.Model;
using MyNet.WebApi.Controllers;
using MyNet.WebApi.Extensions;
using MyNet.WebApi.Filters;
using MyNet.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyNet.CustomQuery.WebApi.Controllers
{
    [RoutePrefix("api/customquery/tables")]
    [ValidateModelFilter]
    public class TableController : BaseController
    {
        private TableService _tableSrv;
        public TableController(TableService tableSrv)
        {
            _tableSrv = tableSrv;
        }

        [HttpPost]
        [Route("querybypage")]
        public OptResult QueryByPage(PageQuery page)
        {
            OptResult rst = null;

            rst = _tableSrv.QueryByPage(page);

            return rst;
        }

        [HttpPost]
        [Route("gettablewithfields")]
        public OptResult GetTableWithFields(Dictionary<string, string> conditions)
        {
            OptResult rst = null;
            rst = _tableSrv.GetTableWithFields(conditions);
            return rst;
        }

        [HttpPost]
        [Route("add")]
        public OptResult Add(AddTableViewModel vmAddTable)
        {
            OptResult rst = null;
            //
            var token = base.ParseToken(ActionContext);
            var table = OOMapper.Map<AddTableViewModel, Table>(vmAddTable);
            rst = _tableSrv.Add(table);

            return rst;
        }

        [HttpPost]
        [Route("update")]
        public OptResult Update(EditTableViewModel vmEditTable)
        {
            OptResult rst = null;
            //
            var token = base.ParseToken(ActionContext);

            var table = OOMapper.Map<EditTableViewModel, Table>(vmEditTable);
            rst = _tableSrv.Update(table);

            return rst;
        }

        [HttpPost]
        [Route("multidelete")]
        public OptResult Delete(DelByIdsViewModel vm)
        {
            OptResult rst = null;

            var token = base.ParseToken(ActionContext);

            rst = _tableSrv.DeleteBatch(vm.pks);

            return rst;
        }
    }
}
