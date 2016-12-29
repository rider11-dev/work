using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.WebHostService.ApiDemo
{
    public interface IContact
    {
        string usrname { get; set; }
        string phone { get; set; }
        string email { get; set; }
        Address address { get; set; }
    }
}
