using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MumboDB
{
    public class BTreeNode
    {
        public int maxKeys = 3;
        public List<KeyValuePair<int, string>> keys = new();
        public List<BTreeNode> childNodes = new List<BTreeNode>();
        public bool isLeaf;
    }
}
