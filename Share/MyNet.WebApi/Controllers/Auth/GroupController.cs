using MyNet.Components.Result;
using MyNet.Service.Auth;
using MyNet.WebApi.Filters;
using MyNet.ViewModel.Auth.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyNet.WebApi.Extensions;
using MyNet.Components.Mapper;
using MyNet.Model.Auth;
using MyNet.WebApi.Models;
using MyNet.Model;
using MyNet.Components.Emit;

namespace MyNet.WebApi.Controllers.Auth
{
    [RoutePrefix("api/auth/group")]
    [TokenValidateFilter]
    [ValidateModelFilter]
    public class GroupController : BaseController
    {
        //私有变量
        GroupService _groupSrv;
        public GroupController(GroupService groupSrv)
        {
            _groupSrv = groupSrv;
        }

        [HttpPost]
        [Route("add")]
        public OptResult Add(GroupVM vmAddGroup)
        {
            OptResult rst = null;
            Type type = typeof(GroupVM);

            //
            var token = base.ParseToken(ActionContext);
            var group = OOMapper.Map<GroupVM, Group>(vmAddGroup);
            rst = _groupSrv.Add(group);

            return rst;
        }

        [HttpPost]
        [Route("update")]
        public OptResult Update(GroupVM vmEditGroup)
        {
            OptResult rst = null;

            //
            var token = base.ParseToken(ActionContext);

            var group = OOMapper.Map<GroupVM, Group>(vmEditGroup);
            rst = _groupSrv.Update(group);

            return rst;
        }

        [HttpPost]
        [Route("multidelete")]
        public OptResult Delete(DelByIdsViewModel vmDels)
        {
            OptResult rst = null;

            var token = base.ParseToken(ActionContext);

            rst = _groupSrv.DeleteBatch(vmDels.pks);

            return rst;
        }
        [HttpPost]
        [Route("querybypage")]
        public OptResult QueryByPage(PageQuery page)
        {
            OptResult rst = null;

            rst = _groupSrv.QueryByPage(page);

            return rst;
        }

        [Route("getall")]
        public OptResult GetAllGroups()
        {
            OptResult rst = null;

            rst = _groupSrv.GetAll();

            return rst;
        }
    }
}
