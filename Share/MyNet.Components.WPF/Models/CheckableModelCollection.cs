using MyNet.Components.Misc;
using MyNet.Components.WPF.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.WPF.Models
{
    public class CheckableModelCollection : BaseModel, ICheckable
    {
        bool _checked;
        public bool IsChecked
        {
            get
            {
                return _checked;
            }
            set
            {
                if (_checked != value)
                {
                    _checked = value;
                    base.RaisePropertyChanged("IsChecked");
                }
            }
        }

        public int PageStart { get; set; }

        IList<CheckableModel> _models;
        /// <summary>
        /// Models必须是IList<T>集合，而不能是IEnumerable<T>，否则WPF中datagrid删除行后，再编辑将报错
        /// </summary>
        public IList<CheckableModel> Models
        {
            get { return _models; }
            set
            {
                if (_models != value)
                {
                    _models = value;
                    base.RaisePropertyChanged("Models");
                    if (_models != null)
                    {
                        int i = 0;
                        foreach (var model in _models)
                        {
                            model.BelongTo = this;
                            (model as IRowNumber).RowNumber = PageStart + (i++) + 1;
                        }
                    }
                }
            }
        }

        DelegateCommand _checkAllCmd;
        public DelegateCommand CheckAllCmd
        {
            get
            {
                if (_checkAllCmd == null)
                {
                    _checkAllCmd = new DelegateCommand(CheckAllAction, e => { return true; });
                }
                return _checkAllCmd;
            }
        }

        private void CheckAllAction(object parameter)
        {
            if (_models == null || _models.Count() < 1)
            {
                return;
            }

            bool ck = false;
            if (parameter != null)
            {
                Boolean.TryParse(parameter.ToString(), out ck);
            }

            foreach (var item in _models)
            {
                item.IsChecked = ck;
            }
        }

        protected IList<CheckableModel> GetSelectedModels()
        {
            return Models == null ? null : Models.Where(m => m.IsChecked == true).ToList();
        }

    }
}
