using MyNet.Components.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllInOne.Client
{
    public class ElderInfoVM : BaseModel, ICopyable
    {
        string _name;
        public string name//姓名
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    base.RaisePropertyChanged("name");
                }
            }
        }
        string _sex;
        public string sex
        {
            get { return _sex; }
            set
            {
                if (_sex != value)
                {
                    _sex = value;
                    base.RaisePropertyChanged("sex");
                }
            }
        }//
        string _age;
        public string age
        {
            get { return _age; }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    base.RaisePropertyChanged("age");
                }
            }
        }
        string _birthday;
        public string birthday
        {
            get { return _birthday; }
            set
            {
                if (_birthday != value)
                {
                    _birthday = value;
                    base.RaisePropertyChanged("birthday");
                }
            }
        }
        string _idcard;
        public string idcard
        {
            get { return _idcard; }
            set
            {
                if (_idcard != value)
                {
                    _idcard = value;
                    base.RaisePropertyChanged("idcard");
                }
            }
        }
        string _address;
        public string address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    base.RaisePropertyChanged("address");
                }
            }
        }//地址
        string _huadres;
        public string huadres
        {
            get { return _huadres; }
            set
            {
                if (_huadres != value)
                {
                    _huadres = value;
                    base.RaisePropertyChanged("huadres");
                }
            }
        }//户籍地
        string _phone;
        public string phone
        {
            get { return _phone; }
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    base.RaisePropertyChanged("phone");
                }
            }
        }
        string _tel;
        public string tel
        {
            get { return _tel; }
            set
            {
                if (_tel != value)
                {
                    _tel = value;
                    base.RaisePropertyChanged("tel");
                }
            }
        }
        string _education;
        public string education
        {
            get { return _education; }
            set
            {
                if (_education != value)
                {
                    _education = value;
                    base.RaisePropertyChanged("education");
                }
            }
        }//文化程度
        string _health;
        public string health
        {
            get { return _health; }
            set
            {
                if (_health != value)
                {
                    _health = value;
                    base.RaisePropertyChanged("health");
                }
            }
        }//健康状况
        string _companyWork;
        public string companyWork
        {
            get { return _companyWork; }
            set
            {
                if (_companyWork != value)
                {
                    _companyWork = value;
                    base.RaisePropertyChanged("companyWork");
                }
            }
        }//工作单位
        string _job;
        public string job
        {
            get { return _job; }
            set
            {
                if (_job != value)
                {
                    _job = value;
                    base.RaisePropertyChanged("job");
                }
            }
        }//职务
        string _political;
        public string political
        {
            get { return _political; }
            set
            {
                if (_political != value)
                {
                    _political = value;
                    base.RaisePropertyChanged("political");
                }
            }
        }//政治面貌
        string _religious;
        public string religious
        {
            get { return _religious; }
            set
            {
                if (_religious != value)
                {
                    _religious = value;
                    base.RaisePropertyChanged("religious");
                }
            }
        }//宗教信仰
        string _maritaltid;
        public string maritaltid
        {
            get { return _maritaltid; }
            set
            {
                if (_maritaltid != value)
                {
                    _maritaltid = value;
                    base.RaisePropertyChanged("maritaltid");
                }
            }
        }//婚姻状况
        string _marryDate;
        public string marryDate
        {
            get { return _marryDate; }
            set
            {
                if (_marryDate != value)
                {
                    _marryDate = value;
                    base.RaisePropertyChanged("marryDate");
                }
            }
        }//结婚日期
        string _nation;
        public string nation
        {
            get { return _nation; }
            set
            {
                if (_nation != value)
                {
                    _nation = value;
                    base.RaisePropertyChanged("nation");
                }
            }
        }//民族
        string _nature;
        public string nature
        {
            get { return _nature; }
            set
            {
                if (_nature != value)
                {
                    _nature = value;
                    base.RaisePropertyChanged("nature");
                }
            }
        }//户口性质
        string _status;
        public string status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    base.RaisePropertyChanged("status");
                }
            }
        }//管理类型：常住人口

        public void CopyTo(object target)
        {
            if (target == null)
            {
                return;
            }
            var info = target as ElderInfoVM;
            info.name = name;
            info.sex = sex;
            info.age = age;
            info.birthday = birthday;
            info.idcard = idcard;
            info.address = address;
            info.huadres = huadres;
            info.phone = phone;
            info.tel = tel;
            info.education = education;
            info.health = health;
            info.companyWork = companyWork;
            info.job = job;
            info.political = political;
            info.religious = religious;
            info.maritaltid = maritaltid;
            info.marryDate = marryDate;
            info.nation = nation;
            info.nature = nature;
            info.status = status;
        }

        public void Reset()
        {
            name = "";
            sex = "";
            age = "";
            birthday = "";
            idcard = "";
            address = "";
            huadres = "";
            phone = "";
            tel = "";
            education = "";
            health = "";
            companyWork = "";
            job = "";
            political = "";
            religious = "";
            maritaltid = "";
            marryDate = "";
            nation = "";
            nature = "";
            status = "";
        }
    }
}
