using MyNet.Components.Extensions;
using MyNet.Components.WPF.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace MyNet.CustomQuery.Client.Models.ExecQuery
{
    public class SelFieldsSelector
    {
        private ExecQueryModel QModel;
        //查询结果字段视图数据源
        public CollectionViewSource ViewSrcSelFields { get; set; }
        //已选字段视图
        public CollectionViewSource ViewSelectedFields { get; set; }

        public SelFieldsSelector(ExecQueryModel qModel)
        {
            QModel = qModel;
        }

        private ICommand _addSelFieldsCmd;
        public ICommand AddSelFieldsCmd
        {
            get
            {
                if (_addSelFieldsCmd == null)
                {
                    _addSelFieldsCmd = new DelegateCommand(AddSelFieldsAction);
                }
                return _addSelFieldsCmd;
            }
        }

        private void AddSelFieldsAction(object obj)
        {
            var fields = obj as IEnumerable<object>;
            if (fields.IsEmpty())
            {
                return;
            }
            var maxOrder = QModel.SelectedFields.GetMax(f => f.order) + 1;
            var selFields = fields.Select(o => o as FieldViewModel).ToList();//这里不能直接设置Order，因为会导致不连续；需要转换为List，IEnumerable延迟查询
            selFields.ForEach(f => f.order = maxOrder++);
            QModel.SelectedFields.AddRange(selFields);
            //过滤字段源
            FilterSelFieldsSrc();
        }

        private ICommand _delSelFieldsCmd;
        public ICommand DelSelFieldsCmd
        {
            get
            {
                if (_delSelFieldsCmd == null)
                {
                    _delSelFieldsCmd = new DelegateCommand(DelSelFieldsAction);
                }
                return _delSelFieldsCmd;
            }
        }

        private void DelSelFieldsAction(object obj)
        {
            var fields = obj as IEnumerable<object>;
            if (fields.IsEmpty())
            {
                return;
            }
            QModel.SelectedFields.DeleteBatch(() =>
            {
                var lst = fields.Select(o => (FieldViewModel)o);
                lst.ToList().ForEach(f => f.ResetOrder());//重置序号
                return lst;
            });
            FilterSelFieldsSrc();
        }

        private ICommand _addAllSelFieldsCmd;
        public ICommand AddAllSelFieldsCmd
        {
            get
            {
                if (_addAllSelFieldsCmd == null)
                {
                    _addAllSelFieldsCmd = new DelegateCommand(AddAllSelFieldsAction);
                }
                return _addAllSelFieldsCmd;
            }
        }

        private void AddAllSelFieldsAction(object obj)
        {
            var fields = QModel.GetFieldsBySelectedTable();
            if (fields.IsEmpty())
            {
                return;
            }
            //先删除，为了重新排order
            QModel.SelectedFields.DeleteBatch(QModel.SelectedFields.Where(f => true));
            QModel.SelectedFields.AddRange(() =>
            {
                var orderMax = 0;
                var lst = fields.Cast<FieldViewModel>().Select(o => o as FieldViewModel);
                lst.ToList().ForEach(f => f.order = orderMax++);//设置order
                return lst;
            });
            FilterSelFieldsSrc();
        }

        private ICommand _delAllSelFieldsCmd;
        public ICommand DelAllSelFieldsCmd
        {
            get
            {
                if (_delAllSelFieldsCmd == null)
                {
                    _delAllSelFieldsCmd = new DelegateCommand(DelAllSelFieldsAction);
                }
                return _delAllSelFieldsCmd;
            }
        }

        private void DelAllSelFieldsAction(object obj)
        {
            QModel.SelectedFields.ToList().ForEach(f => f.ResetOrder());
            QModel.SelectedFields.Clear();
            FilterSelFieldsSrc();
        }

        private ICommand _moveUpSelFieldCmd;
        public ICommand MoveUpSelFieldCmd
        {
            get
            {
                if (_moveUpSelFieldCmd == null)
                {
                    _moveUpSelFieldCmd = new DelegateCommand(MoveUpSelFieldAction);
                }
                return _moveUpSelFieldCmd;
            }
        }

        private void MoveUpSelFieldAction(object obj)
        {
            var cur = obj as FieldViewModel;
            if (QModel.SelectedFields.IsEmpty() || cur == null || cur.order == QModel.SelectedFields.GetMin(f => f.order))
            {
                return;
            }
            //上移：将当前order与前一个替换
            var pre = QModel.SelectedFields.Where(f => f.order == cur.order - 1).First();
            var temp = pre.order;
            pre.order = cur.order;
            cur.order = temp;

            SortSelectedFields();
        }

        private ICommand _moveDownSelFieldCmd;
        public ICommand MoveDownSelFieldCmd
        {
            get
            {
                if (_moveDownSelFieldCmd == null)
                {
                    _moveDownSelFieldCmd = new DelegateCommand(MoveDownSelFieldAction);
                }
                return _moveDownSelFieldCmd;
            }
        }

        private void MoveDownSelFieldAction(object obj)
        {
            var cur = obj as FieldViewModel;
            if (QModel.SelectedFields.IsEmpty() || cur == null || cur.order == QModel.SelectedFields.GetMax(f => f.order))
            {
                return;
            }
            //下移
            var next = QModel.SelectedFields.Where(f => f.order == cur.order + 1).First();
            var temp = next.order;
            next.order = cur.order;
            cur.order = temp;

            SortSelectedFields();
        }

        private void SortSelectedFields()
        {
            var view = ViewSelectedFields.View;
            if (view == null)
            {
                return;
            }
            if (view.SortDescriptions.IsEmpty())
            {
                view.SortDescriptions.Add(new SortDescription("order", ListSortDirection.Ascending));
            }
            else
            {
                view.Refresh();
            }
        }

        /// <summary>
        /// 过滤查询结果字段选择源，同理还有过滤条件、排序选择源
        /// 因为有左右选择，所以需要添加额外过滤条件
        /// </summary>
        public void FilterSelFieldsSrc(IEnumerable<FieldViewModel> baseFields = null)
        {
            ICollectionView view = ViewSrcSelFields.View;
            if (view == null)
            {
                return;
            }

            if (baseFields == null)
            {
                baseFields = QModel.GetFieldsBySelectedTable();
            }
            if (baseFields.IsEmpty())
            {
                view.Filter = model => { return 1 == 0; };
                return;
            }
            var leftFields = baseFields.Except(QModel.SelectedFields);//差集
            view.Filter = model => { return leftFields.Contains((FieldViewModel)model); };
            //删除已选但不存在的查询字段
            QModel.SelectedFields.DeleteBatch(QModel.SelectedFields.Where(sel => !baseFields.Contains(sel)));//删除SelectedFields中存在而fields中不存在的，先清空order
        }

    }
}
