using System;
using System.Threading.Tasks;
using JsonDatabase;

namespace JsonDatabaseTestbed.Commands.Set
{
    public class SetIntegerCommand : ILeafCliCommand
    {
        private readonly Func<Session> _sessionFactory;
        public string Description => "\tint\t\tSet integer";
        public Type ParentCommandType => typeof(SetCommand);
        public bool CanHandle(string command) => command.IsRoughly("integer");

        public SetIntegerCommand(Func<Session> sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public async Task Execute()
        {
            Cmd.WriteLine("Key:");
            var key = Cmd.Prompt();

            Cmd.WriteLine("Value:");
            var value = Int32.Parse(Cmd.Prompt());

            using (new Benchmark())
            {
                var session = _sessionFactory();

                await session.SetAsync(key, value);
            }
        }
    }
}