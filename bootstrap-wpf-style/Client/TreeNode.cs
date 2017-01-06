using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class TreeNode
    {
        public string Label { get; set; }
        public ObservableCollection<TreeNode> ChildNodes { get; set; }
    }
}
