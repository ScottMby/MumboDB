using MumboDB.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MumboDB.Commands
{
    public class Search : ICommandWithParams<string>
    {
        public List<string> commandParams { get; set; } = new();

        public string Execute(BTreeNode bTreeNode)
        {
            return ExecuteCommand(bTreeNode);
        }

        object ICommand.Execute(BTreeNode bTreeNode)
        {
            return Execute(bTreeNode);
        }

        public string ExecuteCommand(BTreeNode rootNode)
        {
            string[] paramStrs = commandParams.ToArray();
            if (paramStrs.Length != 1)
            {
                throw new Exception("Invalid number of params for Search Command");
            }
            if (int.TryParse(paramStrs[0], out int key))
            {
                string value = FindLocation(rootNode, key).keys.Where(k => k.Key == key).First().Value;
            }

            return "this needs changing";

        }

        private BTreeNode FindLocation(BTreeNode currentNode, int keyValue)
        {
            if (currentNode.isLeaf)
            {
                return currentNode;
            }
            else
            {
                for (int i = 0; i < currentNode.keys.Count(); i++)
                {
                    if (keyValue < currentNode.keys.Where(k => k.Key == i).First().Key)
                    {
                        return FindLocation(currentNode.childNodes[i], keyValue);
                    }
                    if (keyValue > currentNode.keys.Where(k => k.Key == i).First().Key)
                    {
                        return FindLocation(currentNode.childNodes[i + 1], keyValue);
                    }
                }
            }
            //Failed to find location
            return null;
        }
    }
}
