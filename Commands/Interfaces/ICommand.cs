using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MumboDB.Commands.Interfaces
{
    public interface ICommand
    {
        //All Commands Must Have an Execute Method
        public void Execute();
    }
}
