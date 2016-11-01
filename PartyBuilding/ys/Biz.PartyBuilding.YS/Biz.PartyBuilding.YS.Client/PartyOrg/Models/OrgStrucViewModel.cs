using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.YS.Client.PartyOrg.Models
{
    public class OrgStrucViewModel : BaseModel
    {
        string _org_name;
        public string org_name
        {
            get
            {
                return _org_name;
            }
            set
            {
                if (_org_name != value)
                {
                    _org_name = value;
                    base.RaisePropertyChanged("org_name");
                }
            }
        }

        string _org_parent_name;
        public string org_parent_name
        {
            get
            {
                return _org_parent_name;
            }
            set
            {
                if (_org_parent_name != value)
                {
                    _org_parent_name = value;
                    base.RaisePropertyChanged("org_parent_name");
                }
            }
        }

        string _org_code;
        public string org_code
        {
            get
            {
                return _org_code;
            }
            set
            {
                if (_org_code != value)
                {
                    _org_code = value;
                    base.RaisePropertyChanged("org_code");
                }
            }
        }

        string _org_unit;
        public string org_unit
        {
            get
            {
                return _org_unit;
            }
            set
            {
                if (_org_unit != value)
                {
                    _org_unit = value;
                    base.RaisePropertyChanged("org_unit");
                }
            }
        }

        string _org_unit_type1;
        public string org_unit_type1
        {
            get
            {
                return _org_unit_type1;
            }
            set
            {
                if (_org_unit_type1 != value)
                {
                    _org_unit_type1 = value;
                    base.RaisePropertyChanged("org_unit_type1");
                }
            }
        }
        string _org_unit_type2;
        public string org_unit_type2
        {
            get
            {
                return _org_unit_type2;
            }
            set
            {
                if (_org_unit_type2 != value)
                {
                    _org_unit_type2 = value;
                    base.RaisePropertyChanged("org_unit_type2");
                }
            }
        }
        string _org_contacts;
        public string org_contacts
        {
            get
            {
                return _org_contacts;
            }
            set
            {
                if (_org_contacts != value)
                {
                    _org_contacts = value;
                    base.RaisePropertyChanged("org_contacts");
                }
            }
        }

        string _org_phone;
        public string org_phone
        {
            get
            {
                return _org_phone;
            }
            set
            {
                if (_org_phone != value)
                {
                    _org_phone = value;
                    base.RaisePropertyChanged("org_phone");
                }
            }
        }

        string _org_addr;
        public string org_addr
        {
            get
            {
                return _org_addr;
            }
            set
            {
                if (_org_addr != value)
                {
                    _org_addr = value;
                    base.RaisePropertyChanged("org_addr");
                }
            }
        }

        string _org_remark;
        public string org_remark
        {
            get
            {
                return _org_remark;
            }
            set
            {
                if (_org_remark != value)
                {
                    _org_remark = value;
                    base.RaisePropertyChanged("org_remark");
                }
            }
        }

        public void CopyTo(OrgStrucViewModel target)
        {
            target.org_code = this.org_code;
            target.org_name = this.org_name;
            target.org_parent_name = this.org_parent_name;
            target.org_contacts = this.org_contacts;
            target.org_phone = this.org_phone;
            target.org_addr = this.org_addr;
        }

    }
}
