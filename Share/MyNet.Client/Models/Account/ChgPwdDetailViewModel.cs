using MyNet.Components.Emit;
using MyNet.Components.Extensions;
using MyNet.Components.Misc;
using MyNet.Components.WPF.Models;
using MyNet.ViewModel.Auth.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.Client.Models.Account
{
    public class ChgPwdDetailViewModel : BaseModel
    {
        public IChgPwdVM chgpwddata { get; private set; }

        public ChgPwdDetailViewModel(bool needValidate = true) : base(needValidate)
        {
            chgpwddata = DynamicModelBuilder.GetInstance<IChgPwdVM>(parent: typeof(BaseModel), ctorArgs: needValidate);
            chgpwddata.ValidateMetadataType = typeof(ChgPwdVM);
        }

        public bool HandlyCheck(out string error)
        {
            error = string.Empty;
            if (!chgpwddata.newpwd.Equals(chgpwddata.newpwd2))
            {
                error = "两次输入新密码不一致";
                return false;
            }
            if (chgpwddata.oldpwd.Equals(chgpwddata.newpwd))
            {
                error = "新旧密码不能相同";
                return false;
            }

            return true;
        }

        public void Reset()
        {
            chgpwddata.oldpwd = chgpwddata.newpwd = chgpwddata.newpwd2 = "";
        }
    }
}