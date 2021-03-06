using System;
using System.Threading.Tasks;
using MickeySmith;

namespace MickeySmithTestbed.Commands.Set
{
    public class SetIntegerCommand : ILeafCliCommand
    {
        private readonly Func<Session> _sessionFactory;
        public string Description => "\tint\t\tSet integer";
        public Type ParentCommandType => typeof(SetTestsCommand);
        public bool CanHandle(string command) => command.IsRoughly("integer");

        public SetIntegerCommand(Func<Session> sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public Task Execute(string command)
        {
            Cmd.WriteLine("Key:");
            var key = Cmd.Prompt();

            Cmd.WriteLine("Value:");
            var value = Int32.Parse(Cmd.Prompt());

            using (new Benchmark())
            {
                var session = _sessionFactory();

                 session.Set(key, value);
            }

            return Task.CompletedTask;
        }
    }
}