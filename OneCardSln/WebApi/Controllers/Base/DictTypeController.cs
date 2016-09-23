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

namespace OneCardSln.WebApi.Controllers.Base
{
    [RoutePrefix("api/base/dicttype")]
    [TokenValidateFilter]
    public class DictTypeController : BaseController
    {
        const string Msg_QueryByPage = "分页查询字典类型";

        DictTypeService _dictTypeSrv;

        public DictTypeController(DictTypeService srv)
        {
            _dictTypeSrv = srv;
        }

        [HttpPost]
        [Route("querybypage")]
        public OptResult QueryByPage(PageQuery page)
        {
            OptResult rst = null;

            rst = _dictTypeSrv.QueryByPage(page);

            return rst;
        }

        [HttpGet]
        [Route("getlist")]
        public OptResult GetList()
        {
            OptResult rst = null;
            rst = _dictTypeSrv.GetList();

            return rst;
        }

        [HttpPost]
        [Route("add")]
        public OptResult Add(AddDictTypeViewModel vmAddDictType)
        {
            OptResult rst = null;
            if (!ModelState.IsValid)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }
            var dictType = OOMapper.Map<AddDictTypeViewModel, DictType>(vmAddDictType);
            rst = _dictTypeSrv.Add(dictType);

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

            rst = _dictTypeSrv.Delete(vmDel.pk);

            return rst;
        }
    }
}