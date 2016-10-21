using MyNet.Model.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Service.Auth.Models
{
    public class UserDto : User
    {
        //按理，这里应该单独定义数据传输对象需要的字段，这里偷懒了（直接继承User类）
    }
}
