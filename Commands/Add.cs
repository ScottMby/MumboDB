using MumboDB.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MumboDB.Commands
{
    public class Add : ICommandWithParams<BTreeNode>
    {
        public List<string> commandParams { get; set; } = new();

        public BTreeNode Execute(BTreeNode bTreeNode)
        {
            return ExecuteCommand(bTreeNode);
        }

        object ICommand.Execute(BTreeNode bTreeNode)
        {
            return Execute(bTreeNode);
        }

        public BTreeNode ExecuteCommand(BTreeNode bTreeNode)
        {
            string[] paramStrs = commandParams.ToArray();
            if(paramStrs.Length != 2)
            {
                throw new Exception("Invalid number of params for Add Command");
            }
            if(int.TryParse(paramStrs[0], out int key))
            {
                if(bTreeNode.keys.Count() < bTreeNode.maxKeys)
                {
                    bTreeNode.keys.Add(new KeyValuePair<int, string>(key, paramStrs[1]));
                    return bTreeNode;
                }
                else
                {
                    bTreeNode.keys.Add(new KeyValuePair<int, string>(key, paramStrs[1]));
                    bTreeNode = SplitNode(bTreeNode);
                    return bTreeNode;
                }
            }
            else
            {
                throw new Exception("Invalid key for Add Command: " + paramStrs);
            }

        }

        private BTreeNode SplitNode(BTreeNode bTreeNode)
        {
            BTreeNode newNode = new();
            List<KeyValuePair<int, string>> keys = bTreeNode.keys;
            int medianIndex = (keys.Count - 1) / 2;
            newNode.keys.Add(keys[medianIndex]);

            BTreeNode leftNode = new();
            for (int i = 0; i < medianIndex; i++)
            {
                leftNode.keys.Add(keys[i]);
            }

            BTreeNode rightNode = new();
            for (int i = medianIndex + 1; i < keys.Count; i++)
            {
                rightNode.keys.Add(keys[i]);
            }

            newNode.childNodes.Add(leftNode);
            newNode.childNodes.Add(rightNode);

            return newNode;
        }
    }
}
