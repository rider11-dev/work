using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.ViewModel
{
    public class FieldVM
    {
        public string id { get; set; }
        [Required(ErrorMessage = "查询表不能为空")]
        public string tbid { get; set; }
        [Required(ErrorMessage = "查询字段名不能为空")]
        [MaxLength(20, ErrorMessage = "查询字段名最大为20个字符")]
        public string fieldname { get; set; }
        [MaxLength(40, ErrorMessage = "字段显示名称最大为40个字符")]
        public string displayname { get; set; }
        [Required(ErrorMessage = "查询字段类型不能为空")]
        [MaxLength(20, ErrorMessage = "查询字段类型最大为20个字符")]
        public string fieldtype { get; set; }
        [MaxLength(255, ErrorMessageResourceName = "Remark_Length", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string remark { get; set; }
        public bool visible { get; set; }
    }
}
