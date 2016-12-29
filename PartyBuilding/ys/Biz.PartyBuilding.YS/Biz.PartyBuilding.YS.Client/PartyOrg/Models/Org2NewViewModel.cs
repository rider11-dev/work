using MyNet.Components.Misc;
using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.YS.Client.PartyOrg.Models
{
    public class Org2NewViewModel : BaseModel
    {
        string _comp_name;
        public string comp_name
        {
            get
            {
                return _comp_name;
            }
            set
            {
                if (_comp_name != value)
                {
                    _comp_name = value;
                    base.RaisePropertyChanged("comp_name");
                }
            }
        }

        int _mem_count_dy;
        public int mem_count_dy
        {
            get
            {
                return _mem_count_dy;
            }
            set
            {
                if (_mem_count_dy != value)
                {
                    _mem_count_dy = value;
                    base.RaisePropertyChanged("mem_count_dy");
                }
            }
        }
        int _emp_count;
        public int emp_count
        {
            get
            {
                return _emp_count;
            }
            set
            {
                if (_emp_count != value)
                {
                    _emp_count = value;
                    base.RaisePropertyChanged("emp_count");
                }
            }
        }

        string _is_dzz_establish;
        public string is_dzz_establish
        {
            get
            {
                return _is_dzz_establish;
            }
            set
            {
                if (_is_dzz_establish != value)
                {
                    _is_dzz_establish = value;
                    base.RaisePropertyChanged("is_dzz_establish");
                }
            }
        }

        string _dzz_establish_type;
        public string dzz_establish_type
        {
            get
            {
                return _dzz_establish_type;
            }
            set
            {
                if (_dzz_establish_type != value)
                {
                    _dzz_establish_type = value;
                    base.RaisePropertyChanged("dzz_establish_type");
                }
            }
        }

        string _zbsj_name;
        public string zbsj_name
        {
            get
            {
                return _zbsj_name;
            }
            set
            {
                if (_zbsj_name != value)
                {
                    _zbsj_name = value;
                    base.RaisePropertyChanged("zbsj_name");
                }
            }
        }

        string _zbsj_sex;
        public string zbsj_sex
        {
            get
            {
                return _zbsj_sex;
            }
            set
            {
                if (_zbsj_sex != value)
                {
                    _zbsj_sex = value;
                    base.RaisePropertyChanged("zbsj_sex");
                }
            }
        }

        int _zbsj_age;
        public int zbsj_age
        {
            get
            {
                return _zbsj_age;
            }
            set
            {
                if (_zbsj_age != value)
                {
                    _zbsj_age = value;
                    base.RaisePropertyChanged("zbsj_age");
                }
            }
        }

        string _zbsj_xl;
        public string zbsj_xl
        {
            get
            {
                return _zbsj_xl;
            }
            set
            {
                if (_zbsj_xl != value)
                {
                    _zbsj_xl = value;
                    base.RaisePropertyChanged("zbsj_xl");
                }
            }
        }


        string _zbsj_joinin_time;
        public string zbsj_joinin_time
        {
            get
            {
                return _zbsj_joinin_time;
            }
            set
            {
                if (_zbsj_joinin_time != value)
                {
                    _zbsj_joinin_time = value;
                    base.RaisePropertyChanged("zbsj_joinin_time");
                }
            }
        }

        string _has_atc_place;
        public string has_atc_place
        {
            get
            {
                return _has_atc_place;
            }
            set
            {
                if (_has_atc_place != value)
                {
                    _has_atc_place = value;
                    base.RaisePropertyChanged("has_atc_place");
                }
            }
        }

        string _atc_place_area;
        public string atc_place_area
        {
            get
            {
                return _atc_place_area;
            }
            set
            {
                if (_atc_place_area != value)
                {
                    _atc_place_area = value;
                    base.RaisePropertyChanged("atc_place_area");
                }
            }
        }

        public void CopyTo(Org2NewViewModel vm)
        {
            if (vm == null)
            {
                return;
            }
            vm.comp_name = this.comp_name;
            vm.mem_count_dy = this.mem_count_dy;
            vm.emp_count = this.emp_count;
            vm.is_dzz_establish = this.is_dzz_establish;
            vm.dzz_establish_type = this.dzz_establish_type;
            vm.zbsj_name = this.zbsj_name;
            vm.zbsj_sex = this.zbsj_sex;
            vm.zbsj_age = this.zbsj_age;
            vm.zbsj_xl = this.zbsj_xl;
            vm.zbsj_joinin_time = this.zbsj_joinin_time;
            vm.has_atc_place = this.has_atc_place;
            vm.atc_place_area = this.atc_place_area;
        }
    }
}
