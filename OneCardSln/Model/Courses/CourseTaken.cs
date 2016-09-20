using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneCardSln.Model.Courses
{
    /// <summary>
    /// 课程参与记录
    /// </summary>
    public class CourseTaken
    {
        public string id { get; set; }
        /// <summary>
        /// 课程参与人id
        /// </summary>
        public string taker { get; set; }
        /// <summary>
        /// 参与人名称
        /// </summary>
        public string taker_name { get; set; }
        /// <summary>
        /// 课程id
        /// </summary>
        public string course { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string course_name { get; set; }
        /// <summary>
        /// 课程参与时间
        /// </summary>
        public DateTime? take_time { get; set; }
    }
}
