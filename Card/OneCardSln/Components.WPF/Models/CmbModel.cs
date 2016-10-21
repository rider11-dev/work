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

        private IList<CmbItem> _dataSrc = null;

        public IList<CmbItem> DataSource
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

        public void Bind(IList<CmbItem> dataSrc, string selectedId = "", bool setSelect = true, bool needBlankItem = false)
        {
            if (dataSrc == null || dataSrc.Count < 1)
            {
                return;
            }

            var copyArr = new CmbItem[dataSrc.Count];
            if (dataSrc != null)
            {
                dataSrc.CopyTo(copyArr, 0);
            }
            var copyList = copyArr.ToList();
            if (needBlankItem && copyList != null && copyList.Count(m => string.IsNullOrEmpty(m.Id)) < 1)
            {
                copyList.Insert(0, new CmbItem { Id = string.Empty });
            }
            this.DataSource = copyList;

            if (setSelect)
            {
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
