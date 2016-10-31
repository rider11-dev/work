using MyNet.Components.Misc;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MyNet.Components.Extensions;
using MyNet.Components;
using MyNet.Client.Public;
using MyNet.Components.Result;
using MyNet.Components.WPF.Windows;

namespace Biz.PartyBuilding.Client.Models.Base
{
    public class PartyOrgDetailViewModel : PartyOrgViewModel, ICopytToable, IClearable
    {
        [JsonIgnore]
        public CmbModel CmbModelOrgType { get; set; }
        [JsonIgnore]
        public CmbModel CmbModelChgRemind { get; set; }

        CmbItem _selectedPoType;
        [JsonIgnore]
        public CmbItem SelectedPoType
        {
            get { return _selectedPoType; }
            set
            {
                if (_selectedPoType != value)
                {
                    _selectedPoType = value;
                    if (_selectedPoType != null && this.po_type != _selectedPoType.Id)
                    {
                        this.po_type = _selectedPoType.Id;
                    }
                    base.RaisePropertyChanged("SelectedPoType");
                }
            }
        }

        CmbItem _cmb_chg_remind;
        [JsonIgnore]
        public CmbItem cmb_chg_remind
        {
            get { return _cmb_chg_remind; }
            set
            {
                if (_cmb_chg_remind != value)
                {
                    _cmb_chg_remind = value;
                    if (_cmb_chg_remind != null && _cmb_chg_remind.Id != base.po_chg_remind.ToString())
                    {
                        bool val = false;
                        Boolean.TryParse(_cmb_chg_remind.Id, out val);
                        po_chg_remind = val;
                    }
                    base.RaisePropertyChanged("cmb_chg_remind");
                }
            }
        }

        ICommand _getCmd;
        [JsonIgnore]
        public ICommand GetCmd
        {
            get
            {
                if (_getCmd == null)
                {
                    _getCmd = new DelegateCommand(GetAction);
                }
                return _getCmd;
            }
        }
        private void GetAction(object parameter)
        {
            this.Clear();
            if (parameter.IsEmpty())
            {
                return;
            }

            var rst = HttpHelper.GetResultByPost(ApiHelper.GetApiUrl(PartyApiKeys.PartyOrgGetById, PartyApiKeys.Key_ApiProvider_Party),
                    new
                    {
                        pk = parameter.ToString()
                    }, Context.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Search, rst.msg);
                return;
            }
            PartyOrgDetailViewModel vmOrg = null;
            if (rst.data != null)
            {
                vmOrg = JsonConvert.DeserializeObject<PartyOrgDetailViewModel>(rst.data.ToString());
            }
            if (vmOrg != null)
            {
                vmOrg.CopyTo(this);
            }
        }

        ICommand _saveCmd;
        [JsonIgnore]
        public ICommand SaveCmd
        {
            get
            {
                if (_saveCmd == null)
                {
                    _saveCmd = new DelegateCommand(SaveAction);
                }
                return _saveCmd;
            }
        }

        private void SaveAction(object parameter)
        {
            if (!this.IsValid)
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Validate, this.Error);
                return;
            }
            var url = ApiHelper.GetApiUrl(PartyApiKeys.SaveOrg, PartyApiKeys.Key_ApiProvider_Party);
            var rst = HttpHelper.GetResultByPost(url, (PartyOrgViewModel)this, Context.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.Save, rst.msg);
                return;
            }
            MessageWindow.ShowMsg(MessageType.Info, OperationDesc.Save, MsgConst.Msg_Succeed);

        }

        public void CopyTo(IBaseModel targetModel)
        {
            if (targetModel == null)
            {
                return;
            }
            var vmOrg = (PartyOrgDetailViewModel)targetModel;
            vmOrg.po_id = this.po_id;
            vmOrg.po_gp_id = this.po_gp_id;
            vmOrg.po_type = this.po_type;
            if (vmOrg.CmbModelOrgType != null)
            {
                vmOrg.CmbModelOrgType.Select(vmOrg.po_type);
            }
            vmOrg.po_chg_num = base.po_chg_num;
            vmOrg.po_chg_date = base.po_chg_date;
            vmOrg.po_expire_date = base.po_expire_date;
            vmOrg.po_chg_remind = base.po_chg_remind;
            if (vmOrg.CmbModelChgRemind != null)
            {
                vmOrg.CmbModelChgRemind.Select(vmOrg.po_chg_remind.ToString());
            }
            vmOrg.po_mem_normal = base.po_mem_normal;
            vmOrg.po_mem_potential = base.po_mem_potential;
            vmOrg.po_mem_activists = base.po_mem_activists;
            vmOrg.po_remark = base.po_remark;
        }

        public void Clear()
        {
            po_id = "";
            po_gp_id = "";
            base.po_type = "";
            if (CmbModelOrgType != null)
            {
                CmbModelOrgType.Select("");
            }
            base.po_chg_num = "";
            base.po_chg_date = DateTime.Now;
            base.po_expire_date = DateTime.Now;
            base.po_chg_remind = false;
            if (CmbModelChgRemind != null)
            {
                CmbModelChgRemind.Select(base.po_chg_remind.ToString());
            }
            base.po_mem_normal = 0;
            base.po_mem_potential = 0;
            base.po_mem_activists = 0;
            base.po_remark = "";
        }

    }
}
