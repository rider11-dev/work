using EmitMapper.MappingConfiguration;
using MyNet.Components.Mapper;
using MyNet.Components.Misc;
using MyNet.Components.WPF.Models;
using MyNet.CustomQuery.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Client.Models
{
    public class FieldViewModel : CheckableModel, ICopytToable
    {
        public string id { get; set; }
        public string tbid { get; set; }
        private string _fieldname;
        [Required(ErrorMessage = "字段名称不能为空")]
        [StringLength(20, ErrorMessage = "字段名称最大为20个字符")]
        public string fieldname
        {
            get { return _fieldname; }
            set
            {
                if (_fieldname == value)
                {
                    return;
                }
                _fieldname = value;
                base.RaisePropertyChanged("fieldname");
            }
        }

        private string _displayname;
        [MaxLength(40, ErrorMessage = "显示名称最大为40个字符")]
        public string displayname
        {
            get { return _displayname; }
            set
            {
                if (_displayname == value)
                {
                    return;
                }
                _displayname = value;
                base.RaisePropertyChanged("displayname");
            }
        }

        private string _fieldtype;
        [Required(ErrorMessage = "字段类型不能为空")]
        [MaxLength(20, ErrorMessage = "字段类型最大为20个字符")]
        public string fieldtype
        {
            get { return _fieldtype; }
            set
            {
                if (_fieldtype == value)
                {
                    return;
                }
                _fieldtype = value;
                base.RaisePropertyChanged("fieldtype");
            }
        }

        private string _remark;
        [MaxLength(255, ErrorMessageResourceName = "Remark_Length", ErrorMessageResourceType = typeof(MyNet.Components.Resource.ViewModelResource))]
        public string remark
        {
            get { return _remark; }
            set
            {
                if (_remark == value)
                {
                    return;
                }
                _remark = value;
                base.RaisePropertyChanged("remark");
            }
        }

        private string _tbname;
        public string tbname
        {
            get { return _tbname; }
            set
            {
                if (_tbname != value)
                {
                    _tbname = value;
                    base.RaisePropertyChanged("tbname");
                    if (string.IsNullOrEmpty(_tbname))
                    {
                        tbid = "";
                    }
                }
            }
        }

        private string _fieldtype_name;
        public string fieldtype_name
        {
            get { return _fieldtype_name; }
            set
            {
                if (_fieldtype_name != value)
                {
                    _fieldtype_name = value;
                    base.RaisePropertyChanged("fieldtype_name");
                    if (string.IsNullOrEmpty(_fieldtype_name))
                    {
                        tbid = "";
                    }
                }
            }
        }

        [JsonIgnore]
        public string fieldfullname { get { return string.Format("{0}.{1}[{2}]", tbname, displayname, fieldname); } }

        public void CopyTo(IBaseModel targetModel)
        {
            if (targetModel == null)
            {
                return;
            }
            var vmField = (FieldDetailViewModel)targetModel;
            vmField.id = this.id;
            vmField.tbid = this.tbid;
            vmField.tbname = this.tbname;
            vmField.displayname = this.displayname;
            vmField.fieldname = this.fieldname;
            vmField.fieldtype = this.fieldtype;
            vmField.fieldtype_name = this.fieldtype_name;
            vmField.remark = this.remark;
        }

        public static FieldViewModel Parse(DbField dbField)
        {
            if (dbField == null)
            {
                return null;
            }
            FieldViewModel fvm = OOMapper.Map<DbField, FieldViewModel>(dbField,
                new DefaultMapConfig()
                .ConvertUsing<DbField, FieldViewModel>(f => new FieldViewModel
                {
                    fieldname = f.column_name,
                    displayname = f.column_comment,
                    fieldtype = f.field_type,
                    tbname = f.table_name
                }));
            return fvm;
        }

        public override string ToString()
        {
            return fieldfullname;
        }
    }
}
