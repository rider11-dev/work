using MyNet.Components.Extensions;
using MyNet.Components.Mapper;
using MyNet.Components.Result;
using MyNet.CustomQuery.Model;
using MyNet.CustomQuery.Service;
using MyNet.CustomQuery.ViewModel;
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
    [RoutePrefix("api/customquery/fields")]
    [ValidateModelFilter]
    public class FieldController : BaseController
    {
        private FieldService _fieldSrv;
        public FieldController(FieldService fieldSrv)
        {
            _fieldSrv = fieldSrv;
        }

        [HttpPost]
        [Route("querybypage")]
        public OptResult QueryByPage(PageQuery page)
        {
            OptResult rst = null;

            rst = _fieldSrv.QueryByPage(page);

            return rst;
        }

        [HttpPost]
        [Route("add")]
        public OptResult Add(AddFieldViewModel vmAddField)
        {
            OptResult rst = null;
            //
            var token = base.ParseToken(ActionContext);
            var field = OOMapper.Map<AddFieldViewModel, Field>(vmAddField);
            rst = _fieldSrv.Add(field);

            return rst;
        }

        [HttpPost]
        [Route("update")]
        public OptResult Update(EditFieldViewModel vmEditField)
        {
            OptResult rst = null;
            //
            var token = base.ParseToken(ActionContext);

            var field = OOMapper.Map<EditFieldViewModel, Field>(vmEditField);
            rst = _fieldSrv.Update(field);

            return rst;
        }

        [HttpPost]
        [Route("multidelete")]
        public OptResult Delete(DelByIdsViewModel vm)
        {
            OptResult rst = null;

            var token = base.ParseToken(ActionContext);

            rst = _fieldSrv.DeleteBatch(vm.pks);

            return rst;
        }

        [HttpPost]
        [Route("dbfields")]
        public OptResult GetDbFields(IEnumerable<string> tbids)
        {
            OptResult rst = null;

            rst = _fieldSrv.GetDbFields(tbids);

            return rst;
        }

        [HttpPost]
        [Route("init")]
        public OptResult InitFields(IEnumerable<Field> fields)
        {
            OptResult rst = null;
            if (fields.IsEmpty())
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空！");
                return rst;
            }
            rst = _fieldSrv.Init(fields);

            return rst;
        }
    }
}
