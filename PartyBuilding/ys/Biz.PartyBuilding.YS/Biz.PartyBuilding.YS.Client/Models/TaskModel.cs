using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz.PartyBuilding.YS.Client.Models
{
    public class TaskModel:BaseModel
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
        string _name;
        public string name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    base.RaisePropertyChanged("name");
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
        string _priority;
        /// <summary>
        /// 优先级：高、中、低
        /// </summary>
        public string priority
        {
            get { return _priority; }
            set
            {
                if (_priority != value)
                {
                    _priority = value;
                    base.RaisePropertyChanged("priority");
                }
            }
        }
        string _receiver;
        public string receiver
        {
            get { return _receiver; }
            set
            {
                if (_receiver != value)
                {
                    _receiver = value;
                    base.RaisePropertyChanged("receiver");
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
        string _expire_time;
        public string expire_time
        {
            get { return _expire_time; }
            set
            {
                if (_expire_time != value)
                {
                    _expire_time = value;
                    base.RaisePropertyChanged("expire_time");
                }
            }
        }
        string _progress;
        public string progress
        {
            get { return _progress; }
            set
            {
                if (_progress != value)
                {
                    _progress = value;
                    base.RaisePropertyChanged("progress");
                }
            }
        }
        string _state;
        /// <summary>
        /// 状态：编辑、已发布、已完成、已取消
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

        string _complete_state;
        /// <summary>
        /// 完成标志:未领、已领未完成、已完成
        /// </summary>
        public string complete_state
        {
            get { return _complete_state; }
            set
            {
                if (_complete_state != value)
                {
                    _complete_state = value;
                    base.RaisePropertyChanged("complete_state");
                }
            }
        }


        public void CopyTo(TaskModel target)
        {
            if(target==null)
            {
                return;
            }
            target.id = this.id;
            target.name = this.name;
            target.content = this.content;
            target.priority = this.priority;
            target.receiver = this.receiver;
            target.issue_time = this.issue_time;
            target.expire_time = this.expire_time;
            target.progress = this.progress;
            target.state = this.state;
            target.complete_state = this.complete_state;
        }
    }
}