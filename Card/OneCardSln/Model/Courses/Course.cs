using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNet.Model.Courses
{
    /// <summary>
    /// 课程基本信息
    /// </summary>
    public class Course
    {
        public string id { get; set; }
        /// <summary>
        /// 课程种类
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 课程时长（分钟）
        /// </summary>
        public int period { get; set; }
        /// <summary>
        /// 累计参与人次
        /// </summary>
        public int total_taken { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 课程开始时间：保存时使用
        /// </summary>
        public DateTime time_for_save { get; set; }
        /// <summary>
        /// 下次开课日期
        /// </summary>
        public string next_date { get; set; }
        /// <summary>
        /// 开课时间
        /// </summary>
        public string begin_time { get; set; }
        /// <summary>
        /// 课程重复性设置，简单描述
        /// </summary>
        public string repeat_set { get; set; }
    }
}
