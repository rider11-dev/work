using MyNet.Components.Misc;
using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.Client.Models.Base
{
    public class PartyOrgViewModel : BaseModel, ICopytToable
    {

        public string po_id { get; set; }

        string _po_type;
        public string po_type
        {
            get { return _po_type; }
            set
            {
                if (_po_type != value)
                {
                    _po_type = value;
                    base.RaisePropertyChanged("po_type");
                }
            }
        }

        string _po_chg_num;
        public string po_chg_num
        {
            get { return _po_chg_num; }
            set
            {
                if (_po_chg_num != value)
                {
                    _po_chg_num = value;
                    base.RaisePropertyChanged("po_chg_num");
                }
            }
        }

        DateTime _po_chg_date;
        public DateTime po_chg_date
        {
            get { return _po_chg_date; }
            set
            {
                if (_po_chg_date != value)
                {
                    _po_chg_date = value;
                    base.RaisePropertyChanged("po_chg_date");
                }
            }
        }

        DateTime _po_expire_date;
        public DateTime po_expire_date
        {
            get { return _po_expire_date; }
            set
            {
                if (_po_expire_date != value)
                {
                    _po_expire_date = value;
                    base.RaisePropertyChanged("po_expire_date");
                }
            }
        }

        bool _po_chg_remind;
        public bool po_chg_remind
        {
            get { return _po_chg_remind; }
            set
            {
                if (_po_chg_remind != value)
                {
                    _po_chg_remind = value;
                    base.RaisePropertyChanged("po_chg_remind");
                }
            }
        }

        int _po_mem_normal;
        public int po_mem_normal
        {
            get { return _po_mem_normal; }
            set
            {
                if (_po_mem_normal != value)
                {
                    _po_mem_normal = value;
                    base.RaisePropertyChanged("po_mem_normal");
                }
            }
        }

        int _po_mem_potential;
        public int po_mem_potential
        {
            get { return _po_mem_potential; }
            set
            {
                if (_po_mem_potential != value)
                {
                    _po_mem_potential = value;
                    base.RaisePropertyChanged("po_mem_potential");
                }
            }
        }

        int _po_mem_activists;
        public int po_mem_activists
        {
            get { return _po_mem_activists; }
            set
            {
                if (_po_mem_activists != value)
                {
                    _po_mem_activists = value;
                    base.RaisePropertyChanged("po_mem_activists");
                }
            }
        }

        string _po_remark;
        public string po_remark
        {
            get { return _po_remark; }
            set
            {
                if (_po_remark != value)
                {
                    _po_remark = value;
                    base.RaisePropertyChanged("po_remark");
                }
            }
        }

        public void CopyTo(IBaseModel targetModel)
        {
            if (targetModel == null)
            {
                return;
            }
            var vmOrg = (PartyOrgViewModel)targetModel;
            vmOrg.po_type = this.po_type;
            vmOrg.po_chg_num = this.po_chg_num;
            vmOrg.po_chg_date = this.po_chg_date;
            vmOrg.po_expire_date = this.po_expire_date;
            vmOrg.po_chg_remind = this.po_chg_remind;
            vmOrg.po_mem_normal = this.po_mem_normal;
            vmOrg.po_mem_potential = this.po_mem_potential;
            vmOrg.po_mem_activists = this.po_mem_activists;
            vmOrg.po_remark = this.po_remark;
        }
    }
}
