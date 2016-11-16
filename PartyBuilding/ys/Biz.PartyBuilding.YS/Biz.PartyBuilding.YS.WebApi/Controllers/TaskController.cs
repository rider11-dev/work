using Biz.PartyBuilding.YS.Models;
using MyNet.Components.Extensions;
using MyNet.Components.Result;
using MyNet.WebApi.Controllers;
using MyNet.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyNet.WebApi.Extensions;
using Biz.PartyBuilding.YS.WebApi.Models;

namespace Biz.PartyBuilding.YS.WebApi.Controllers
{
    [RoutePrefix("api/party/task")]
    public class TaskController : BaseController
    {
        static List<TaskModel> _tasks = new List<TaskModel>
        {
            //new TaskModel
            //{
            //    id="8d1fd508beb641ab8883c998781a0462",
            //    name="组织活动场所信息采集",
            //    content="组织活动场所信息采集",
            //    priority="高",
            //    receiver="所有组织",
            //    issue_time="",
            //    expire_time="2016-11-28",
            //    progress="",
            //    state="编辑"
            //}
        };

        [HttpGet]
        [Route("get")]
        public OptResult GetTasks()
        {
            OptResult rst = OptResult.Build(ResultCode.Success, "",
                new
                {
                    tasks = _tasks
                });
            return rst;
        }

        [HttpPost]
        [Route("save")]
        public OptResult Save(TaskModel task)
        {
            OptResult rst = null;
            if (task == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空或格式不正确");
                return rst;
            }

            if (string.IsNullOrEmpty(task.id) && _tasks.Count(t => t.id == task.id) == 0)
            {
                task.id = GuidExtension.GetOne();
                task.state = "编辑";
                _tasks.Add(task);

            }
            else
            {
                var oldTask = _tasks.Where(t => t.id == task.id).First();
                oldTask.name = task.name;
                oldTask.content = task.content;
                oldTask.priority = task.priority;
                oldTask.receiver = task.receiver;
                oldTask.expire_time = task.expire_time;

            }
            rst = OptResult.Build(ResultCode.Success, "保存成功");

            return rst;
        }

        /// <summary>
        /// 是否有新任务（未领、已领未完成）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("isnew")]
        public OptResult IsNew()
        {
            OptResult rst = OptResult.Build(ResultCode.Success, "",
                new
                {
                    flag = _tasks.Count(task => task.complete_state == "已领未完成" || task.complete_state == "未领") > 0 ? "1" : "0"
                });
            return rst;
        }

        /// <summary>
        /// 任务完成情况是否有更新
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("task_complete_new")]
        public OptResult TaskComplete()
        {
            var infos = _tasks.Where(t => t.state == "已完成").Select<TaskModel, String>(t =>
            {
                return string.Format("任务【{0}】已完成", t.name);
            });


            OptResult rst = OptResult.Build(ResultCode.Success, "",
                new
                {
                    has = (infos == null || infos.Count() < 1) ? "0" : "1",
                    infos = infos
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

            var task = _tasks.Where(t => t.id == vm.id).FirstOrDefault();
            if (task == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, "未找到指定数据", new { id = vm.id });
                return rst;
            }

            task.issue_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            task.state = "已发布";
            task.complete_state = "未领";

            rst = OptResult.Build(ResultCode.Success, "发布成功");

            return rst;
        }

        [HttpPost]
        [Route("take")]
        public OptResult Take(ProcessByIdModel vm)
        {
            OptResult rst = null;
            if (vm == null || string.IsNullOrEmpty(vm.id))
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空或格式不正确");
                return rst;
            }
            var task = _tasks.Where(t => t.id == vm.id).FirstOrDefault();
            if (task == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, "未找到指定任务", new { id = vm.id });
                return rst;
            }

            task.complete_state = "已领未完成";

            rst = OptResult.Build(ResultCode.Success, "任务领取成功");

            return rst;
        }

        [HttpPost]
        [Route("complete")]
        public OptResult Complete(TaskModel task)
        {
            OptResult rst = null;
            if (task == null || string.IsNullOrEmpty(task.id))
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空或格式不正确");
                return rst;
            }
            var oldTask = _tasks.Where(t => t.id == task.id).FirstOrDefault();
            if (task == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, "未找到指定任务", new { id = task.id });
                return rst;
            }

            oldTask.progress = task.progress;
            oldTask.complete_state = "已完成";
            oldTask.state = "已完成";

            rst = OptResult.Build(ResultCode.Success, "任务完成成功");

            return rst;
        }
    }
}
