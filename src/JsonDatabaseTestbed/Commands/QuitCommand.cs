﻿using System;
using System.Threading.Tasks;

namespace MickeySmithTestbed.Commands
{
    public class QuitCommand : ILeafCliCommand
    {
        public bool CanHandle(string command)
        {
            return command.IsRoughly("q", "quit", "exit");
        }

        public Task Execute(string command)
        {
            Cmd.WriteLine("Closing");
            Environment.Exit(0);

            return Task.FromResult(0);
        }

        public string Description => "\tq, quit, exit\tQuit";
        public Type ParentCommandType => null;
    }
}