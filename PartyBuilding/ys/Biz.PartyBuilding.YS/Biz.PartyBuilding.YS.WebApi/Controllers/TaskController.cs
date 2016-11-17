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
using Biz.PartyBuilding.YS.Repository;
using Biz.PartyBuilding.YS.Models;
using DapperExtensions;

namespace Biz.PartyBuilding.YS.WebApi.Controllers
{
    [RoutePrefix("api/party/task")]
    public class TaskController : BaseController
    {
        PartyTaskRepository _rep;

        public TaskController(PartyTaskRepository rep)
        {
            _rep = rep;
        }

        [HttpGet]
        [Route("get")]
        public OptResult GetTasks()
        {
            var tasks = _rep.GetList(null);

            OptResult rst = OptResult.Build(ResultCode.Success, "",
                new
                {
                    tasks = tasks
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

            if (string.IsNullOrEmpty(task.id))
            {
                task.id = GuidExtension.GetOne();
                task.state = "编辑";

                _rep.Insert(task);
            }
            else
            {
                var oldTask = _rep.GetById(task.id);
                oldTask.name = task.name;
                oldTask.content = task.content;
                oldTask.priority = task.priority;
                oldTask.receiver = task.receiver;
                oldTask.expire_time = task.expire_time;

                _rep.Update(oldTask);
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
            var cnt = _rep.Count(Predicates.Field<TaskModel>(t => t.state, Operator.Eq, new string[] { "已领未完成", "未领" }));
            OptResult rst = OptResult.Build(ResultCode.Success, "",
                new
                {
                    flag = cnt > 0 ? "1" : "0"
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
            var tasks = _rep.GetList(Predicates.Field<TaskModel>(t => t.state, Operator.Eq, "已完成"));
            if (tasks == null || tasks.Count() < 1)
            {
                return OptResult.Build(ResultCode.Success, "",
                 new
                 {
                     has = "0"
                 });
            }


            var infos = tasks.Select<TaskModel, String>(t =>
            {
                return string.Format("任务【{0}】已完成", t.name);
            });


            OptResult rst = OptResult.Build(ResultCode.Success, "",
                new
                {
                    has = "1",
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

            var task = _rep.GetById(vm.id);
            if (task == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, "未找到指定数据", new { id = vm.id });
                return rst;
            }

            task.issue_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            task.state = "已发布";
            task.complete_state = "未领";

            _rep.Update(task);

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
            var task = _rep.GetById(vm.id);
            if (task == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, "未找到指定任务", new { id = vm.id });
                return rst;
            }

            task.complete_state = "已领未完成";
            _rep.Update(task);

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
            var oldTask = _rep.GetById(task.id);
            if (task == null)
            {
                rst = OptResult.Build(ResultCode.DataNotFound, "未找到指定任务", new { id = task.id });
                return rst;
            }

            oldTask.progress = task.progress;
            oldTask.complete_state = "已完成";
            oldTask.state = "已完成";

            _rep.Update(oldTask);

            rst = OptResult.Build(ResultCode.Success, "任务完成成功");

            return rst;
        }
    }
}
