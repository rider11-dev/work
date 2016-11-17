using Biz.PartyBuilding.YS.Models;
using Biz.PartyBuilding.YS.Repository;
using DapperExtensions;
using MyNet.Components;
using MyNet.Components.Extensions;
using MyNet.Components.Logger;
using MyNet.Components.Result;
using MyNet.WebApi.Controllers;
using MyNet.WebApi.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Biz.PartyBuilding.YS.WebApi.Controllers
{
    [RoutePrefix("api/party/actarea")]
    public class PartyActAreaController : BaseController
    {
        const string SqlName_Get = "get";
        AreaRepository _rep;
        AreaPicRepository _picRep;
        protected ILogHelper<PartyActAreaController> LogHelper = LogHelperFactory.GetLogHelper<PartyActAreaController>();
        public PartyActAreaController(AreaRepository rep, AreaPicRepository picRep)
        {
            _rep = rep;
            _picRep = picRep;
        }

        [HttpGet]
        [Route("get")]
        public OptResult GetAreas()
        {
            var areas = _rep.GetList(null);
            if (areas != null && areas.Count() > 0)
            {
                foreach (var area in areas)
                {
                    area.pic = UploadHelper.GetContent(area.id);
                }
            }

            OptResult rst = OptResult.Build(ResultCode.Success, "",
                new
                {
                    infos = areas
                });
            return rst;
        }

        [HttpPost]
        [Route("save")]
        public OptResult Save(PartyActAreaModel area)
        {
            OptResult rst = null;
            if (area == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空或格式不正确");
                return rst;
            }

            if (string.IsNullOrEmpty(area.id))
            {
                area.id = GuidExtension.GetOne();

                var tran = _rep.Begin();
                try
                {
                    _rep.Insert(area, tran);
                    if (area.pic != null && area.pic.Count > 0)
                    {
                        //_picRep.InsertBatch(pics, tran);
                        //保存图片数据（直接写到文件）
                        UploadHelper.Upload(area.id, area.pic);
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    LogHelper.LogError("新增area", ex);
                    rst = OptResult.Build(ResultCode.DbError, "新增area");
                    return rst;
                }
            }
            else
            {
                var oldArea = _rep.GetById(area.id);
                oldArea.town = area.town;
                oldArea.village = area.village;
                oldArea.floor_area = area.floor_area;
                oldArea.courtyard_area = area.courtyard_area;
                oldArea.levels = area.levels;
                oldArea.rooms = area.rooms;
                oldArea.location = area.location;
                oldArea.gps = area.gps;
                oldArea.levels = area.levels;

                _rep.Update(area);
            }

            rst = OptResult.Build(ResultCode.Success, "保存成功");

            return rst;
        }
    }
}