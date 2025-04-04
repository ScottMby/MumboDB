using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MumboDB.Commands.Interfaces
{
    public interface ICommand
    {
        object Execute(BTreeNode rootNode);
    }
    public interface ICommand<T> : ICommand
    {
        //All Commands Must Have an Execute Method
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="rootNode">The root node of the BTree</param>
        new T Execute(BTreeNode rootNode);
    }
}
