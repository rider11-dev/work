﻿using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Windows;
using MyNet.ClientFrame.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.ClientFrame.Help
{
    /// <summary>
    /// 树形帮助辅助类
    /// </summary>
    public class TreeHelpHelper
    {
        public static void OpenAllFuncsHelp(bool multiSel = false,
            Action<TreeViewData.TreeNode> singleSelAction = null,
            Action<IList<TreeViewData.TreeNode>> multiSelAction = null)
        {
            TreeHelpWindow.ShowHelp("功能菜单帮助", () =>
            {
                List<TreeViewData.NodeViewModel> datas = new List<TreeViewData.NodeViewModel>();
                foreach (var kvp in DataCacheHelper.AllFuncs)
                {
                    var func = kvp.Value;
                    datas.Add(new TreeViewData.NodeViewModel { Id = func.per_code, Label = func.per_name, Parent = func.per_parent, Order = func.per_sort, Data = func, DataId = func.per_id });
                }
                return datas;
            }, multiSel, singleSelAction, multiSelAction);
        }

        public static void OpenAllPermsHelp(Action<IList<TreeViewData.TreeNode>> multiSelAction = null)
        {
            TreeHelpWindow.ShowHelp("所有权限", () =>
            {
                List<TreeViewData.NodeViewModel> datas = new List<TreeViewData.NodeViewModel>();
                foreach (var kvp in DataCacheHelper.AllFuncs)
                {
                    var func = kvp.Value;
                    datas.Add(new TreeViewData.NodeViewModel { Id = func.per_code, Label = func.per_name, Parent = func.per_parent, Order = func.per_sort, Data = func, DataId = func.per_id });
                }

                foreach (var kvp in DataCacheHelper.AllOpts)
                {
                    var opt = kvp.Value;
                    datas.Add(new TreeViewData.NodeViewModel { Id = opt.per_code, Label = opt.per_name, Parent = opt.per_parent, Order = opt.per_sort, Data = opt, DataId = opt.per_id });
                }
                return datas;
            }, true, null, multiSelAction);
        }

        public static void OpenAllGroupsHelp(bool multiSel = false,
            Action<TreeViewData.TreeNode> singleSelAction = null,
            Action<IList<TreeViewData.TreeNode>> multiSelAction = null)
        {
            TreeHelpWindow.ShowHelp("组织帮助", () =>
            {
                List<TreeViewData.NodeViewModel> datas = new List<TreeViewData.NodeViewModel>();
                foreach (var kvp in DataCacheHelper.AllGroups)
                {
                    var gp = kvp.Value;
                    datas.Add(new TreeViewData.NodeViewModel { Id = gp.gp_code, Label = gp.gp_name, Parent = gp.gp_parent, Order = gp.gp_sort, Data = gp, DataId = gp.gp_id });
                }
                return datas;
            }, multiSel, singleSelAction, multiSelAction);
        }
    }
}
