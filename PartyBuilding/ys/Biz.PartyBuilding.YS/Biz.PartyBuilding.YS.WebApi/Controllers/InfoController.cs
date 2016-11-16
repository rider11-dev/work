using Biz.PartyBuilding.YS.Models;
using Biz.PartyBuilding.YS.WebApi.Models;
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
        static List<InfoModel> _infos = new List<InfoModel>
        {
            //new InfoModel
            //{
            //    id="8d1fd508beb641ab8883c998781a0462",
            //    title="关于集中开展学习贯彻党的十八大精神的申明",
            //    content="党的十八大的主题确定为：高举中国特色社会主义伟大旗帜，以邓小平理论、“三个代表”重要思想、科学发展观为指导，解放思想，改革开放，凝聚力量，攻坚克难，坚定不移沿着中国特色社会主义道路前进，为全面建成小康社会而奋斗。",
            //    issue_time=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            //    party="曹县县委组织部",
            //    state="编辑"
            //}
        };

        [HttpGet]
        [Route("get")]
        public OptResult GetInfos()
        {
            OptResult rst = OptResult.Build(ResultCode.Success, "",
                new
                {
                    infos = _infos
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

            if (string.IsNullOrEmpty(info.id) && _infos.Count(t => t.id == info.id) == 0)
            {
                info.id = GuidExtension.GetOne();
                info.state = "编辑";
                _infos.Add(info);

            }
            else
            {
                var oldInfo = _infos.Where(t => t.id == info.id).First();
                oldInfo.title = info.title;
                oldInfo.content = info.content;
                oldInfo.party = info.party;
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
            OptResult rst = OptResult.Build(ResultCode.Success, "",
                new
                {
                    flag = _infos.Count(info => info.state == "已发布" && info.read_state == "未读") > 0 ? "1" : "0"
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

            var info = _infos.Where(t => t.id == vm.id).FirstOrDefault();
            if (info == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, "未找到指定数据", new { id = vm.id });
                return rst;
            }

            info.issue_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            info.state = "已发布";
            info.read_state = "未读";

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
            var info = _infos.Where(t => t.id == vm.id).FirstOrDefault();
            if (info == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, "未找到指定信息", new { id = vm.id });
                return rst;
            }

            info.read_state = "已读";

            rst = OptResult.Build(ResultCode.Success, "信息已读成功");

            return rst;
        }
    }
}