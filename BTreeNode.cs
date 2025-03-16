using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MumboDB
{
    public class BTreeNode
    {
        public int[] key = new int[3];
        public List<BTreeNode> childNodes = new List<BTreeNode>();
        public bool isLeaf;
    }
}
