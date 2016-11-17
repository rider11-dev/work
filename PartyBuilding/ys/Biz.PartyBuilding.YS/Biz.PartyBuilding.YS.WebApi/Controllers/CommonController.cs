using Biz.PartyBuilding.YS.Models;
using Biz.PartyBuilding.YS.Repository;
using DapperExtensions;
using MyNet.Components.Result;
using MyNet.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Biz.PartyBuilding.YS.WebApi.Controllers
{
    [RoutePrefix("api/party/common")]
    public class CommonController : BaseController
    {
        InfoRepository _infoRep;
        PartyTaskRepository _taskRep;
        public CommonController(InfoRepository infoRep, PartyTaskRepository taskRep)
        {
            _infoRep = infoRep;
            _taskRep = taskRep;
        }

        [HttpGet]
        [Route("isnew")]
        public OptResult IsNew()
        {
            OptResult rst;
            PredicateGroup pg = new PredicateGroup
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()
                {
                    Predicates.Field<InfoModel>(i=>i.state,Operator.Eq,"已发布"),
                    Predicates.Field<InfoModel>(i=>i.read_state,Operator.Eq,"未读"),
                }
            };
            var newInfo = _infoRep.GetList(pg).Select(i => i.id);

            var newTask = _taskRep.GetList(Predicates.Field<TaskModel>(t => t.complete_state, Operator.Eq, new string[] { "已领未完成", "未领" })).Select(t => t.id);

            rst = OptResult.Build(ResultCode.Success, "", new
            {
                info = newInfo,
                task = newTask
            });

            return rst;
        }
    }
}