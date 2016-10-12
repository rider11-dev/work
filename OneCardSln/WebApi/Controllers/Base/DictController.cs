using OneCardSln.Model;
using OneCardSln.Service.Base;
using OneCardSln.WebApi.Filters;
using OneCardSln.WebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using OneCardSln.WebApi.Extensions;
using OneCardSln.Components.Mapper;
using OneCardSln.Model.Base;
using OneCardSln.WebApi.Models;
using OneCardSln.Components.Result;

namespace OneCardSln.WebApi.Controllers.Base
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