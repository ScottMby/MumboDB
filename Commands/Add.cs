using MumboDB.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MumboDB.Commands
{
    public class Add : ICommandWithParams
    {
        public List<string> commandParams { get; set; } = new();

        public void Execute()
        {
            Console.WriteLine("Add executed with params: ");
            commandParams.ForEach(i => Console.WriteLine(i));
        }
    }
}
    