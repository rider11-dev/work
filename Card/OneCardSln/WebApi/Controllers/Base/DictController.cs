using MyNet.Model;
using MyNet.Service.Base;
using MyNet.WebApi.Filters;
using MyNet.WebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MyNet.WebApi.Extensions;
using MyNet.Components.Mapper;
using MyNet.Model.Base;
using MyNet.WebApi.Models;
using MyNet.Components.Result;

namespace MyNet.WebApi.Controllers.Base
{
    [RoutePrefix("api/base/dict")]
    [TokenValidateFilter]
    public class DictController : BaseController
    {
        DictService _dictSrv;

        public DictController(DictService dictSrv)
        {
            _dictSrv = dictSrv;
        }

        [HttpPost]
        [Route("add")]
        public OptResult Add(AddDictViewModel vmAddDict)
        {
            OptResult rst = null;
            if (!ModelState.IsValid)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }
            var dict = OOMapper.Map<AddDictViewModel, Dict>(vmAddDict);
            rst = _dictSrv.Add(dict);

            return rst;
        }

        [HttpPost]
        [Route("delete")]
        public OptResult Delete(DelByPkViewModel vmDel)
        {
            OptResult rst = null;
            if (!ModelState.IsValid)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }

            var token = base.ParseToken(ActionContext);

            rst = _dictSrv.Delete(vmDel.pk);

            return rst;
        }

        [HttpPost]
        [Route("querybypage")]
        public OptResult QueryByPage(PageQuery page)
        {
            OptResult rst = null;

            rst = _dictSrv.QueryByPage(page);

            return rst;
        }

        [HttpPost]
        [Route("getlist")]
        public OptResult GetList(Dictionary<string, object> conditions)
        {
            OptResult rst = null;

            rst = _dictSrv.GetList(conditions);

            return rst;
        }
    }
}