using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.WebHostService.ApiDemo
{
    public class Contact
    {
        public virtual string usrname { get; set; }
        public virtual string phone { get; set; }
        public virtual string email { get; set; }
        //public virtual Address address { get; set; }
    }

    public class Address
    {
        public string province { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string street { get; set; }
    }

    public class Contact_1 : Contact
    {
        public new string usrname { get; set; }
        public new string phone { get; set; }
        public new string email { get; set; }
    }
}
