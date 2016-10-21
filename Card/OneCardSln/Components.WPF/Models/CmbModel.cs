using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.WPF.Models
{
    /// <summary>
    /// 下拉框模型
    /// </summary>
    public class CmbModel : BaseModel
    {
        CmbItem _selected;
        public CmbItem Selected
        {
            get { return _selected; }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    base.RaisePropertyChanged("Selected");
                }
            }
        }

        private ObservableCollection<CmbItem> _dataSrc = null;

        public ObservableCollection<CmbItem> DataSource
        {
            get
            {
                return this._dataSrc;
            }
            private set
            {
                if (this._dataSrc != value)
                {
                    this._dataSrc = value;
                    base.RaisePropertyChanged("DataSource");
                }
            }
        }

        public void Bind(ObservableCollection<CmbItem> dataSrc, string selectedId = "")
        {
            this.DataSource = dataSrc;
            if (this.DataSource == null)
            {
                return;
            }

            //设置选中项
            if (string.IsNullOrEmpty(selectedId))
            {
                Select(this.DataSource
                    .Where(m => m.IsDefault == true)
                    .FirstOrDefault()
                    .Id);
            }
            else
            {
                Select(selectedId);
            }
        }

        public void Select(string id)
        {
            if (this.DataSource == null)
            {
                return;
            }
            this.Selected = this.DataSource.Where(m => m.Id == id).FirstOrDefault();
        }
    }
}
