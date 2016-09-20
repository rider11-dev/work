using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneCardSln.Model.Courses
{
    /// <summary>
    /// 课程重复性设置
    /// </summary>
    public class CourseRepeatSet
    {
        public string id { get; set; }
        /// <summary>
        /// 课程id
        /// </summary>
        public string courseid { get; set; }
        /// <summary>
        /// 课程重复类型：day   week month onetime
        /// </summary>
        public string repeat_type { get; set; }
        /// <summary>
        /// 课程日期：重复类型为单次时可用
        /// </summary>
        public DateTime? date { get; set; }
        /// <summary>
        /// 周几,重复类型为每周时可用
        /// </summary>
        public int day_of_week { get; set; }
        /// <summary>
        /// 每月几号，重复类型为每月时可用
        /// </summary>
        public int day_of_month { get; set; }
        /// <summary>
        /// 开始时间(如14:30)
        /// </summary>
        public string begin_time { get; set; }
    }
}
