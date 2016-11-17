using Biz.PartyBuilding.YS.Models;
using Biz.PartyBuilding.YS.Repository;
using Biz.PartyBuilding.YS.WebApi.Models;
using DapperExtensions;
using MyNet.Components.Extensions;
using MyNet.Components.Result;
using MyNet.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Biz.PartyBuilding.YS.WebApi.Controllers
{
    [RoutePrefix("api/party/info")]
    public class InfoController : BaseController
    {
        InfoRepository _rep;
        public InfoController(InfoRepository rep)
        {
            _rep = rep;
        }

        [HttpGet]
        [Route("get")]
        public OptResult GetInfos()
        {
            var infos = _rep.GetList(null);

            OptResult rst = OptResult.Build(ResultCode.Success, "",
                new
                {
                    infos = infos
                });
            return rst;
        }

        [HttpPost]
        [Route("save")]
        public OptResult Save(InfoModel info)
        {
            OptResult rst = null;
            if (info == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空或格式不正确");
                return rst;
            }

            if (string.IsNullOrEmpty(info.id))
            {
                info.id = GuidExtension.GetOne();
                info.state = "编辑";
                _rep.Insert(info);
            }
            else
            {
                var oldInfo = _rep.GetById(info.id);
                oldInfo.title = info.title;
                oldInfo.content = info.content;
                oldInfo.party = info.party;

                _rep.Update(info);
            }
            rst = OptResult.Build(ResultCode.Success, "保存成功");

            return rst;
        }

        /// <summary>
        /// 是否有新公告（已发布&&未读）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("isnew")]
        public OptResult IsNew()
        {
            PredicateGroup pg = new PredicateGroup
            {
                Operator = GroupOperator.And,
                Predicates = new List<IPredicate>()
                {
                    Predicates.Field<InfoModel>(i=>i.state,Operator.Eq,"已发布"),
                    Predicates.Field<InfoModel>(i=>i.read_state,Operator.Eq,"未读"),
                }
            };
            var cnt = _rep.Count(pg);

            OptResult rst = OptResult.Build(ResultCode.Success, "",
                new
                {
                    flag = cnt > 0 ? "1" : "0"
                });
            return rst;
        }

        [HttpPost]
        [Route("release")]
        public OptResult Release(ProcessByIdModel vm)
        {
            OptResult rst = null;
            if (vm == null || string.IsNullOrEmpty(vm.id))
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空或格式不正确");
                return rst;
            }

            var info = _rep.GetById(vm.id);
            if (info == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, "未找到指定数据", new { id = vm.id });
                return rst;
            }

            info.issue_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            info.state = "已发布";
            info.read_state = "未读";
            _rep.Update(info);

            rst = OptResult.Build(ResultCode.Success, "发布成功");

            return rst;
        }

        /// <summary>
        /// 已读
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("read")]
        public OptResult Read(ProcessByIdModel vm)
        {
            OptResult rst = null;
            if (vm == null || string.IsNullOrEmpty(vm.id))
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空或格式不正确");
                return rst;
            }
            var info = _rep.GetById(vm.id);
            if (info == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, "未找到指定信息", new { id = vm.id });
                return rst;
            }

            info.read_state = "已读";
            _rep.Update(info);

            rst = OptResult.Build(ResultCode.Success, "信息已读成功");

            return rst;
        }
    }
}