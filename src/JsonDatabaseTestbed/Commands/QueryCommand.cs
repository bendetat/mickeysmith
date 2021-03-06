﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MickeySmith;

namespace MickeySmithTestbed.Commands
{
    public class QueryCommand : ILeafCliCommand
    {
        private readonly Func<Session> _sessionFactory;
        public string Description => "\tquery\t\tQuery";
        public Type ParentCommandType => null;
        public bool CanHandle(string command) => command.IsRoughly("query") || command.RoughlyStartsWith("query ");

        public QueryCommand(Func<Session> sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public Task Execute(string command)
        {
            var session = _sessionFactory();
            var query = GetQuery(command);

            IDictionary<string, dynamic> results;

            using (new Benchmark())
            {
                results = session.Query(query).End();
            }

            if (!results.Any())
            {
                Cmd.WriteWarningLine("No results");
            }
            else
            {
                Cmd.WriteSubheader($"{results.Count} results");
                foreach (var result in results)
                {
                    Cmd.WriteLine($"Key: {result.Key}");
                    Cmd.WriteLine($"Type: {result.Value.GetType()}");
                    Cmd.WriteLine(result.Value.ToString());
                }
            }

            return Task.CompletedTask;
        }

        static string GetQuery(string command)
        {
            var bits = command.Split(' ');
            if (bits.Length == 2)
            {
                return bits[1];
            }

            Cmd.WriteLine("Query:");

            return Cmd.Prompt();
        }
    }
}
