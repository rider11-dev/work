using MyNet.Components.Mapper;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyNet.Components.WPF.Controls
{
    public class TreeViewData
    {
        private static TreeViewData _data = null;
        public static TreeViewData TestData
        {
            get
            {
                if (_data == null)
                {
                    _data = new TreeViewData();

                    var rn1 = new TreeNode() { Label = "Root A", Level = 1 };
                    rn1.SubNodes.Add(new TreeNode() { Label = "Root A - Child 1", Level = 2 });
                    rn1.SubNodes.Add(new TreeNode() { Label = "Root A - Child 2", Level = 2 });
                    rn1.SubNodes.Add(new TreeNode() { Label = "Root A - Child 3", Level = 2 });
                    rn1.SubNodes.Add(new TreeNode() { Label = "Root A - Child 4", Level = 2 });
                    rn1.SubNodes.Add(new TreeNode() { Label = "Root A - Child 5", Level = 2 });

                    var rn2 = new TreeNode() { Label = "Root B", Level = 1 };
                    rn2.SubNodes.Add(new TreeNode() { Label = "Root B - Child 1", Level = 2 });

                    var rn21 = new TreeNode() { Label = "Root B - Child 2", Level = 2 };
                    rn21.SubNodes.Add(new TreeNode() { Label = "Root B - Child 2 - Child 1", Level = 3 });
                    rn21.SubNodes.Add(new TreeNode() { Label = "Root B - Child 2 - Child 2", Level = 3 });
                    rn2.SubNodes.Add(rn21);
                    rn2.SubNodes.Add(new TreeNode() { Label = "Root B - Child 3", Level = 2 });
                    rn2.SubNodes.Add(new TreeNode() { Label = "Root B - Child 4", Level = 2 });
                    rn2.SubNodes.Add(new TreeNode() { Label = "Root B - Child 5", Level = 2 });



                    var rn3 = new TreeNode() { Label = "Root C", Level = 1 };
                    rn3.SubNodes.Add(new TreeNode() { Label = "Root C - Child 1", Level = 2 });
                    rn3.SubNodes.Add(new TreeNode() { Label = "Root C - Child 2", Level = 2 });
                    rn3.SubNodes.Add(new TreeNode() { Label = "Root C - Child 3", Level = 2 });
                    rn3.SubNodes.Add(new TreeNode() { Label = "Root C - Child 4", Level = 2 });
                    rn3.SubNodes.Add(new TreeNode() { Label = "Root C - Child 5", Level = 2 });

                    _data.RootNodes.Add(rn1);
                    _data.RootNodes.Add(rn2);
                    _data.RootNodes.Add(rn3);
                }

                return _data;
            }
        }

        public IList<TreeNode> AllNodes
        {
            get
            {
                var nodes = new List<TreeNode>();
                if (RootNodes == null || RootNodes.Count < 1)
                {
                    return nodes;
                }
                foreach (var node in RootNodes)
                {
                    CollectNodes(nodes, node);
                }
                return nodes;
            }
        }

        private void CollectNodes(IList<TreeNode> container, TreeNode node)
        {
            container.Add(node);
            if (node.SubNodes == null || node.SubNodes.Count < 1)
            {
                return;
            }
            foreach (var subNode in node.SubNodes)
            {
                CollectNodes(container, subNode);
            }
        }

        private ObservableCollection<TreeNode> _rootNodes = null;
        public IList<TreeNode> RootNodes
        {
            get
            {
                return _rootNodes ?? (_rootNodes = new ObservableCollection<TreeNode>());
            }
        }

        public TreeViewData()
        {
            //BindTest();
        }

        public void Bind(IList<NodeViewModel> data)
        {
            RootNodes.Clear();
            if (data == null || data.Count < 1)
            {
                return;
            }
            //构造节点树
            var rootsData = data.Where(p => string.IsNullOrEmpty(p.Parent))
                                .OrderBy(p => p.Order);
            TreeNode rootNode = null;
            foreach (var rData in rootsData)
            {
                rootNode = OOMapper.Map<NodeViewModel, TreeNode>(rData);//Label、Id、Parent、Data
                rootNode.Level = 1;
                //递归构造子节点
                if (data.Where(p => p.Parent == rootNode.Id).Count() > 0)
                {
                    BindSubNodes(rootNode, data);
                }

                RootNodes.Add(rootNode);
            }
        }

        private void BindSubNodes(TreeNode pNode, IList<NodeViewModel> data)
        {
            var subDatas = data.Where(p => p.Parent == pNode.Id)
                                .OrderBy(p => p.Order);
            if (subDatas == null || subDatas.Count() < 1)
            {
                return;
            }
            TreeNode subNode = null;
            foreach (var sub in subDatas)
            {
                subNode = OOMapper.Map<NodeViewModel, TreeNode>(sub);
                subNode.Level = pNode.Level + 1;
                subNode.ParentNode = pNode;
                if (data.Where(p => p.Parent == subNode.Id).Count() > 0)
                {
                    BindSubNodes(subNode, data);
                }

                pNode.SubNodes.Add(subNode);
            }
        }

        private void BindTest()
        {
            List<NodeViewModel> nodesData = new List<NodeViewModel>();
            //A
            nodesData.Add(new NodeViewModel { Id = "rootA", Label = "Root A", Order = "1", Data = new { Id = "rootA", Code = "RootA" } });
            nodesData.Add(new NodeViewModel { Id = "level2A1", Label = "Root A - Child 1", Parent = "rootA", Order = "1" });
            nodesData.Add(new NodeViewModel { Id = "level2A2", Label = "Root A - Child 2", Parent = "rootA", Order = "2" });
            nodesData.Add(new NodeViewModel { Id = "level2A3", Label = "Root A - Child 3", Parent = "rootA", Order = "3" });

            //B
            nodesData.Add(new NodeViewModel { Id = "rootB", Label = "Root B", Order = "2" });
            //B1
            nodesData.Add(new NodeViewModel { Id = "level2B1", Label = "Root B - Child 1", Parent = "rootB", Order = "1" });
            //B2
            nodesData.Add(new NodeViewModel { Id = "level2B2", Label = "Root B - Child 2", Parent = "rootB", Order = "2" });
            nodesData.Add(new NodeViewModel { Id = "level3B2C1", Label = "Root B - Child 2 - Child 1", Parent = "level2B2", Order = "1" });
            nodesData.Add(new NodeViewModel { Id = "level3B2C2", Label = "Root B - Child 2 - Child 2", Parent = "level2B2", Order = "2" });
            nodesData.Add(new NodeViewModel { Id = "level3B2C3", Label = "Root B - Child 2 - Child 3", Parent = "level2B2", Order = "3" });
            //B3
            nodesData.Add(new NodeViewModel { Id = "level2B3", Label = "Root B - Child 3", Parent = "rootB", Order = "3" });

            //C
            nodesData.Add(new NodeViewModel { Id = "rootC", Label = "Root C", Order = "3" });
            nodesData.Add(new NodeViewModel { Id = "level2C1", Label = "Root C - Child 1", Parent = "rootC", Order = "1" });
            nodesData.Add(new NodeViewModel { Id = "level2C2", Label = "Root C - Child 2", Parent = "rootC", Order = "2" });

            Bind(nodesData);
        }

        public void Check(List<string> ids)
        {
            if (ids == null || ids.Count < 1)
            {
                return;
            }
            if (RootNodes == null || RootNodes.Count < 1)
            {
                return;
            }
            var allNodes = AllNodes;
            if (allNodes == null || allNodes.Count < 1)
            {
                return;
            }
            foreach (var node in allNodes.Where(n => ids.Contains(n.Id)))
            {
                node.IsChecked = true;
            }
        }

        public void CheckAll()
        {
            SetAllChecked(true);
        }

        public void UncheckAll()
        {
            SetAllChecked(false);
        }

        private void SetAllChecked(bool check)
        {
            if (RootNodes == null || RootNodes.Count < 1)
            {
                return;
            }

            foreach (var node in RootNodes)
            {
                node.CheckCmd.Execute(check);
            }
        }

        public class TreeNode : BaseModel
        {
            public string Label { get; set; }
            public int Level { get; set; }
            public string Id { get; set; }
            /// <summary>
            /// 数据id
            /// </summary>
            public string DataId { get; set; }
            /// <summary>
            /// 指向上级节点的Id字段
            /// </summary>
            public string Parent { get; set; }
            public TreeNode ParentNode { get; set; }
            public dynamic Data { get; set; }
            public string IconUri { get; set; }
            public string HAlign { get; set; }

            private ObservableCollection<TreeNode> _subNodes = null;
            public ObservableCollection<TreeNode> SubNodes
            {
                get
                {
                    return _subNodes ?? (_subNodes = new ObservableCollection<TreeNode>());
                }
            }

            /// <summary>
            /// 得到当前节点的路径，如：功能1 - 功能2 - 功能3
            /// </summary>
            /// <param name="split">分隔符，默认'-'</param>
            /// <returns></returns>
            public string GetNodePath(char split = '-')
            {
                StringBuilder sb = new StringBuilder();
                GetNodePathCore(this, sb, split);
                string path = sb.ToString();
                return path.TrimEnd(' ').TrimEnd(split).TrimEnd(' ');
            }

            private void GetNodePathCore(TreeNode node, StringBuilder sb, char split = '-')
            {
                if (node.ParentNode != null)
                {
                    GetNodePathCore(node.ParentNode, sb, split);
                }
                sb.AppendFormat("{0} {1} ", node.Label, split);
            }

            #region checkbox选中相关
            bool _ischecked = false;
            public bool IsChecked
            {
                get { return _ischecked; }
                set
                {
                    if (_ischecked != value)
                    {
                        _ischecked = value;
                        base.RaisePropertyChanged("IsChecked");
                    }
                }
            }

            private ICommand _checkCmd;
            public ICommand CheckCmd
            {
                get
                {
                    if (_checkCmd == null)
                    {
                        _checkCmd = new DelegateCommand(Check);
                    }
                    return _checkCmd;
                }
            }

            private void Check(object parameter)
            {
                var ck = (bool)parameter;
                IsChecked = ck;
                CheckSub(this);
                CheckParent(this);
            }

            private void CheckSub(TreeNode parent)
            {
                if (parent.SubNodes == null || parent.SubNodes.Count < 1)
                {
                    return;
                }
                foreach (var node in parent.SubNodes)
                {
                    node.IsChecked = parent.IsChecked;
                    CheckSub(node);
                }
            }

            private void CheckParent(TreeNode child)
            {
                var parent = child.ParentNode;
                if (parent == null)
                {
                    return;
                }
                if (child.IsChecked == true)
                {
                    parent.IsChecked = true;
                }
                else if (parent.SubNodes != null && parent.SubNodes.Where(n => n.IsChecked).Count() < 1)
                {
                    parent.IsChecked = false;
                }
                CheckParent(parent);
            }
            #endregion
        }

        public class NodeViewModel
        {
            public string Label { get; set; }
            public string Id { get; set; }
            /// <summary>
            ///  数据id
            /// </summary>
            public string DataId { get; set; }
            public string Parent { get; set; }
            public string Order { get; set; }
            public dynamic Data { get; set; }
            public string IconUri { get; set; }
            public string HAlign { get; set; }
        }
    }
}
