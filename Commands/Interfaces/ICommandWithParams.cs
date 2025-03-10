using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MumboDB.Commands.Interfaces
{
    public interface ICommandWithParams : ICommand
    {
        //Commands with params contain this string array
        public List<string> commandParams { get; set; }
    }
}
