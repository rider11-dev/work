using MyNet.Components.Misc;
using MyNet.Components.WPF.Models;
using MyNet.CustomQuery.Model;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyNet.CustomQuery.Client.Models
{
    public class TableViewModel : CheckableModel, ICopytToable
    {
        public string id { get; set; }
        private string _tbname;
        [Required(ErrorMessage = "表名称不能为空")]
        [StringLength(100, ErrorMessage = "表名称最大为100个字符")]
        public string tbname
        {
            get { return _tbname; }
            set
            {
                if (_tbname == value)
                {
                    return;
                }
                _tbname = value;
                base.RaisePropertyChanged("tbname");
            }
        }

        private string _alias;
        [Required(ErrorMessage = "表别名不能为空")]
        [MaxLength(20, ErrorMessage = "表别名最大为20个字符")]
        public string alias
        {
            get { return _alias; }
            set
            {
                if (_alias == value)
                {
                    return;
                }
                _alias = value;
                base.RaisePropertyChanged("alias");
            }
        }

        private string _comment;
        [Required(ErrorMessage = "表注释不能为空")]
        [MaxLength(20, ErrorMessage = "表注释最大为20个字符")]
        public string comment
        {
            get { return _comment; }
            set
            {
                if (_comment == value)
                {
                    return;
                }
                _comment = value;
                base.RaisePropertyChanged("comment");
            }
        }

        public string tbnamealias
        {
            get
            {
                return string.Format("{0} {1}", tbname, alias);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}[{1}]", comment, tbnamealias);
        }

        public void CopyTo(IBaseModel targetModel)
        {
            if (targetModel == null)
            {
                return;
            }
            var vmTable = (TableViewModel)targetModel;
            vmTable.id = this.id;
            vmTable.tbname = this.tbname;
            vmTable.alias = this.alias;
            vmTable.comment = this.comment;
        }

    }
}
