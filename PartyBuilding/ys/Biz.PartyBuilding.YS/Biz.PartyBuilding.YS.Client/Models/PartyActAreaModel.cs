using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz.PartyBuilding.YS.Client.Models
{
    /// <summary>
    /// 组织活动场所
    /// </summary>
    public class PartyActAreaModel:BaseModel
    {
         string _id;
        public string id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    base.RaisePropertyChanged("id");
                }
            }
        }
         string _town;
        public string town
        {
            get { return _town; }
            set
            {
                if (_town != value)
                {
                    _town = value;
                    base.RaisePropertyChanged("town");
                }
            }
        }
         string _village;
        public string village
        {
            get { return _village; }
            set
            {
                if (_village != value)
                {
                    _village = value;
                    base.RaisePropertyChanged("village");
                }
            }
        }
        /// <summary>
        /// 建筑面积
        /// </summary>
         string _floor_area;
        public string floor_area
        {
            get { return _floor_area; }
            set
            {
                if (_floor_area != value)
                {
                    _floor_area = value;
                    base.RaisePropertyChanged("floor_area");
                }
            }
        }
        /// <summary>
        /// 院落面积
        /// </summary>
         string _courtyard_area;
        public string courtyard_area
        {
            get { return _courtyard_area; }
            set
            {
                if (_courtyard_area != value)
                {
                    _courtyard_area = value;
                    base.RaisePropertyChanged("courtyard_area");
                }
            }
        }
         string _levels;
        public string levels
        {
            get { return _levels; }
            set
            {
                if (_levels != value)
                {
                    _levels = value;
                    base.RaisePropertyChanged("levels");
                }
            }
        }
         string _rooms;
        public string rooms
        {
            get { return _rooms; }
            set
            {
                if (_rooms != value)
                {
                    _rooms = value;
                    base.RaisePropertyChanged("rooms");
                }
            }
        }
        /// <summary>
        /// 坐落位置
        /// </summary>
         string _location;
        public string location
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    base.RaisePropertyChanged("location");
                }
            }
        }
        public string gps { get; set; }
        public List<string> pic { get; set; }

        public void CopyTo(PartyActAreaModel target)
        {
            if (target == null)
            {
                return;
            }
            target.id = this.id;
            target.town = this.town;
            target.village = this.village;
            target.floor_area = this.floor_area;
            target.courtyard_area = this.courtyard_area;
            target.levels = this.levels;
            target.rooms = this.rooms;
            target.location = this.location;
            target.gps = this.gps;
            target.pic = this.pic;

        }
    }
}