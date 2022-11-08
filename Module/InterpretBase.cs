using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenVMSys_Console.Module
{
    internal class InterpretBase
    {
        public void Welcome(Version version)
        {
            System.Console.WriteLine(" _______  _______  _______  __    _  __   __  __   __  _______ ");
            System.Console.WriteLine("|       ||       ||       ||  |  | ||  | |  ||  |_|  ||       |");
            System.Console.WriteLine("|   _   ||    _  ||    ___||   |_| ||  |_|  ||       ||  _____|");
            System.Console.WriteLine("|  | |  ||   |_| ||   |___ |       ||       ||       || |_____ ");
            System.Console.WriteLine("|  |_|  ||    ___||    ___||  _    ||       ||       ||_____  |");
            System.Console.WriteLine("|       ||   |    |   |___ | | |   | |     | | ||_|| | _____| |");
            System.Console.WriteLine("|_______||___|    |_______||_|  |__|  |___|  |_|   |_||_______|\n");
            System.Console.WriteLine("Author:Guo Tingjin\tMail:dev@peercat.cn\tGithub:https://github.com/Ericple\tVersion:{0}", version);
            System.Console.WriteLine("Welcome to OpenVMS Console, application is not started yet, type \"service start\" to launch the service");
        }
        public void Get(string[] appStartArgs)
        {
            System.Console.Write("\nOpenVMS ~> ");
            var command = System.Console.ReadLine();
            if (command != null)
            {
                var mainArg = command.Split(" ")[0];
                var args = command.Split(" ")[new Range(1, command.Split(" ").Length)];
                Decoder.Decode(mainArg, args, appStartArgs);
            }
        }
    }
}
