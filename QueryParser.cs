using MumboDB.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MumboDB
{
    public static class QueryParser
    {
        public static List<ICommand> Parse(string commandString)
        {
            List<ICommand> queryList = new();

            //Get all types within program that implement ICommand
            var type = typeof(ICommand);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));


            string[] commandParts = commandString.Split(' ');

            ICommand? lastCommand = null;
            foreach (string commandPart in commandParts)
            {
                IEnumerable<Type> commandType = types.Where(i => i.Name.ToLower() == commandPart.ToLower());
                //If command corresponds to type that implements ICommand then it is a command.
                if (commandType.Any())
                {
                    //If one command matching the query exists, create an instance of it
                    Type command = commandType.Single();
                    lastCommand = (ICommand?)Activator.CreateInstance(command);
                    if (lastCommand != null)
                    {
                        queryList.Add(lastCommand);
                    }
                    else
                    {
                        throw new Exception("Error creating instance of " + command.Name);
                    }
                }
                else
                {
                    //Is not a command, check if is parameter
                    var paramType = typeof(ICommandWithParams);
                    if(lastCommand != null)
                    {
                        if(paramType.IsAssignableFrom(lastCommand.GetType()))
                        {
                            if (lastCommand is ICommandWithParams lastCommandWithParams)
                            {
                                lastCommandWithParams.commandParams.Add(commandPart);
                            }
                            else
                            {
                                // Command cannot take parameters
                                throw new Exception("Command: " + commandPart + " does not take any parameters.");
                            }
                        }
                        else
                        {
                            //Command cannot take parameters
                            throw new Exception("Command: " + commandPart + " does not take any parameters.");
                        }
                    }
                    else
                    {
                        //Invalid command
                        throw new Exception("Command: " + commandPart + " is not a valid command. Please enter a valid command");
                    }
                }
            }

            return queryList;
        }
    }
}
