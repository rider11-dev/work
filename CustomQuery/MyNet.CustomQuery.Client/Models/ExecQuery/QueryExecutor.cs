using MyNet.Client.Public;
using MyNet.Components;
using MyNet.Components.Extensions;
using MyNet.Components.Result;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Extension;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using MyNet.CustomQuery.Model;
using MyNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace MyNet.CustomQuery.Client.Models.ExecQuery
{
    public class QueryExecutor
    {
        private ExecQueryModel ExecQModel;
        public QueryExecutor(ExecQueryModel execQModel)
        {
            ExecQModel = execQModel;
            Results = new ObservableCollection<dynamic>();
        }

        public DataGrid DgResults { get; set; }
        private ControlPagination _ctlPage;
        public ObservableCollection<dynamic> Results { get; set; }
        /// <summary>
        /// 分页查询控件
        /// </summary>
        public ControlPagination CtlPage
        {
            get
            {
                return _ctlPage;
            }
            set
            {
                _ctlPage = value;
                _ctlPage.QueryHandler = (e) =>
                {
                    ExecQAction(e);
                };
            }
        }
        private ICommand _execQCmd;
        public ICommand ExecQCmd
        {
            get
            {
                if (_execQCmd == null)
                {
                    _execQCmd = new DelegateCommand(o => { CtlPage.Bind(); });
                }
                return _execQCmd;
            }
        }

        private void ExecQAction(PagingArgs page)
        {
            ExecQModel.FilterBaseFieldsSrc();//这里是为了执行查询前，删除由于主表、关联表改变导致的垃圾数据
            Results.Clear();

            //1、转换QueryModel
            QueryModel qModel = null;
            bool success = ParseQueryModel(out qModel);
            if (success == false)
            {
                return;
            }

            //2、查询条件展示
            //3、过滤条件展示
            //4、查询结果DataGrid动态列
            BuildDataGridCols();
            //5、分页
            qModel.Page = new PageQuery { pageIndex = page.PageIndex, pageSize = page.PageSize };
            //6、执行查询，返回结果，绑定Results
            var url = ApiHelper.GetApiUrl(CustomQueryApiKeys.ExecQuery, CustomQueryApiKeys.Key_ApiProvider_CustomQuery);
            var rst = HttpHelper.GetResultByPost(url, qModel, ClientContext.Token);
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, CustomQueryOptDesc.ExecQuery, rst.msg);
                return;
            }
            if (rst.data != null && rst.data.total != null)
            {
                page.RecordsCount = (int)rst.data.total;
                if (page.RecordsCount == 0)
                {
                    page.PageCount = 0;
                    page.PageIndex = 1;
                    return;
                }
                page.PageCount = Convert.ToInt32(Math.Ceiling(page.RecordsCount * 1.0 / page.PageSize));

                var rows = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(((JArray)rst.data.rows).ToString());
                if (rows.IsNotEmpty())
                {
                    Results.AddRange(rows);
                }
                //设置行号
                DgResults.LoadingRow -= SetDgRowNumber;
                DgResults.LoadingRow += SetDgRowNumber;
            }
        }

        private void SetDgRowNumber(object o, DataGridRowEventArgs e)
        {
            var item = e.Row.Item;
            e.Row.Header = CtlPage.PageStart + e.Row.GetIndex() + 1;
        }

        private bool ParseQueryModel(out QueryModel model)
        {
            model = new QueryModel();
            //1、查询表
            bool rst = ParseTables(model);
            if (rst == false)
            {
                return rst;
            }
            //2、查询字段
            rst = ParseFields(model);
            if (rst == false)
            {
                return rst;
            }
            //3、过滤
            rst = ParseFilters(model);
            if (rst == false)
            {
                return rst;
            }
            //4、排序
            rst = ParseSorts(model);
            if (rst == false)
            {
                return rst;
            }
            return rst;
        }

        private bool ParseTables(QueryModel model)
        {
            if (ExecQModel.TableMain == null)
            {
                MessageWindow.ShowMsg(MessageType.Info, CustomQueryOptDesc.ExecQuery, "请选择查询主表！");
                return false;
            }
            model.TableRelation.PrimeTable = ExecQModel.TableMain.tbnamealias;
            if (ExecQModel.JoinTables.IsNotEmpty())
            {
                var cnt = ExecQModel.JoinTables.Count(t => t.Table.IsEmpty());
                if (cnt > 0)
                {
                    MessageWindow.ShowMsg(MessageType.Info, CustomQueryOptDesc.ExecQuery, "关联表信息不完整，请选择关联表或删除空数据！");
                    return false;
                }
                if (ExecQModel.RelFields.IsEmpty())
                {
                    MessageWindow.ShowMsg(MessageType.Info, CustomQueryOptDesc.ExecQuery, "请选择关联字段！");
                    return false;
                }
                cnt = ExecQModel.RelFields.Count(f => f.Field1.IsEmpty() || f.Field2.IsEmpty());
                if (cnt > 0)
                {
                    MessageWindow.ShowMsg(MessageType.Info, CustomQueryOptDesc.ExecQuery, "关联字段信息不完整，请选择关联字段或删除空数据！");
                    return false;
                }
                //获得副本，不能修改源JoinTables
                IList<JoinTable> joinTables = new List<JoinTable>();
                ExecQModel.JoinTables.ToList().ForEach(t => joinTables.Add(new JoinTable { Table = t.Table, JoinType = t.JoinType }));
                foreach (var table in joinTables)
                {
                    var fields = ExecQModel.RelFields.Where(f => f.Table2 == table.Table);
                    if (fields.IsEmpty())
                    {
                        MessageWindow.ShowMsg(MessageType.Info, CustomQueryOptDesc.ExecQuery, "存在未指定关联字段的关联表！");
                        return false;
                    }
                    table.RelFields = fields.ToList();
                    //最后重设Table，因为fields以他为过滤条件（延迟查询）
                    table.Table = ExecQModel.Tables.Where(t => t.tbname == table.Table).First().tbnamealias;//重新设置关联表名称
                }
                model.TableRelation.JoinTables.AddRange(joinTables);
            }
            return true;
        }

        private bool ParseFields(QueryModel model)
        {
            if (ExecQModel.SelectedFields.IsEmpty())
            {
                MessageWindow.ShowMsg(MessageType.Info, CustomQueryOptDesc.ExecQuery, "请选择查询字段！");
                return false;
            }
            model.Fields = ExecQModel.SelectedFields.Select(f => f.fieldname).ToList();
            return true;
        }

        private bool ParseFilters(QueryModel model)
        {
            return true;
        }

        private bool ParseSorts(QueryModel model)
        {
            if (ExecQModel.SelectedSorts.IsEmpty())
            {
                return true;
            }
            model.Sorts = ExecQModel.SelectedSorts.Select(s => s.Sort).ToList();
            return true;
        }

        private void BuildDataGridCols()
        {
            if (DgResults == null)
            {
                return;
            }

            DgResults.Columns.Clear();

            if (ExecQModel.SelectedFields.IsEmpty())
            {
                return;
            }
            DgResults.AddColumns(ExecQModel.SelectedFields
                .Where(f => Convert.ToBoolean(f.visible) == true)
                .OrderBy(f => f.order)
                .Select(f => new DataGridColModel(field: f.fieldname.Replace('.', '_'), header: f.mydisplayname)));
        }
    }
}
