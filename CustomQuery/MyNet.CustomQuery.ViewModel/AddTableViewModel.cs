using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.ViewModel
{
    public class AddTableViewModel
    {
        [Required(ErrorMessage = "表名称不能为空")]
        [MaxLength(100, ErrorMessage = "表名称最大为100个字符")]
        public string tbname { get; set; }
        [Required(ErrorMessage = "表别名不能为空")]
        [MaxLength(20, ErrorMessage = "表别名最大为20个字符")]
        public string alias { get; set; }
        [Required(ErrorMessage = "表注释不能为空")]
        [MaxLength(20, ErrorMessage = "表注释最大为20个字符")]
        public string comment { get; set; }
    }
}
