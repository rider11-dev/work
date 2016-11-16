using MyNet.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.YS.Client
{
   public struct PartyBuildingApiKeys
    {
        public const string Key_ApiProvider_Party = "party";

        /*————————————party-task——————————*/
        public const string TaskGet = "task_get";
        public const string TaskSave = "task_save";
        public const string TaskIsNew = "task_isnew";
        public const string TaskCompleteNew = "task_complete_new";
        public const string TaskRelease = "task_release";
        public const string TaskTake = "task_take";
        public const string TaskComplete = "task_complete";
        /*————————————party-info——————————*/
        public const string InfoGet = "info_get";
        public const string InfoSave = "info_save";
        public const string InfoIsNew = "info_isnew";
        public const string InfoRelease = "info_release";
        public const string InfoRead = "info_read";

        /*————————————party-area——————————*/
        public const string AreaGet = "area_get";
        public const string AreaSave = "area_save";
    }
}
