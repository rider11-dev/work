using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz.PartyBuilding.YS.Client.Models
{
    public class InfoModel : BaseModel
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
        string _title;
        public string title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    base.RaisePropertyChanged("title");
                }
            }
        }
        string _content;
        public string content
        {
            get { return _content; }
            set
            {
                if (_content != value)
                {
                    _content = value;
                    base.RaisePropertyChanged("content");
                }
            }
        }
        string _issue_time;
        public string issue_time
        {
            get { return _issue_time; }
            set
            {
                if (_issue_time != value)
                {
                    _issue_time = value;
                    base.RaisePropertyChanged("issue_time");
                }
            }
        }
        string _party;
        public string party
        {
            get { return _party; }
            set
            {
                if (_party != value)
                {
                    _party = value;
                    base.RaisePropertyChanged("party");
                }
            }
        }
        string _state;
        /// <summary>
        /// 状态：编辑、已发布
        /// </summary>
        public string state
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    base.RaisePropertyChanged("state");
                }
            }
        }
        string _read_state;
        /// <summary>
        /// 阅读状态：已读、未读
        /// </summary>
        public string read_state
        {
            get { return _read_state; }
            set
            {
                if (_read_state != value)
                {
                    _read_state = value;
                    base.RaisePropertyChanged("read_state");
                }
            }
        }

        public void CopyTo(InfoModel target)
        {
            if (target == null)
            {
                return;
            }
            target.title = this.title;
            target.content = this.content;
            target.issue_time = this.issue_time;
            target.party = this.party;
            target.state = this.state;
            target.read_state = this.read_state;
        }
    }
}