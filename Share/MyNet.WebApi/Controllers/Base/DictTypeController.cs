using MyNet.Model;
using MyNet.Service.Base;
using MyNet.WebApi.Filters;
using MyNet.ViewModel.Base;
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
    [RoutePrefix("api/base/dicttype")]
    [TokenValidateFilter]
    [ValidateModelFilter]
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

            var dictType = OOMapper.Map<AddDictTypeViewModel, DictType>(vmAddDictType);
            rst = _dictTypeSrv.Add(dictType);

            return rst;
        }

        [HttpPost]
        [Route("delete")]
        public OptResult Delete(DelByPkViewModel vmDel)
        {
            OptResult rst = null;

            var token = base.ParseToken(ActionContext);

            rst = _dictTypeSrv.Delete(vmDel.pk);

            return rst;
        }
    }
}