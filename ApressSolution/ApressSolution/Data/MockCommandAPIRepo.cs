using ApressSolution.Models;

namespace ApressSolution.Data
{
    public class MockCommandAPIRepo : ICommandAPIRepo
    {
        List<Command> commands = new List<Command>
            {
                new Command
                {
                    Id = 0,
                    HowTo = "How to generate a migration",
                    CommandLine = "dotnet ef migrations add <Name of Migration>",
                    Platform = ".Net Core EF"
                },
                new Command
                {
                    Id = 1,
                    HowTo = "Run Migrations",
                    CommandLine = "dotnet ef database update",
                    Platform = ".Net Core EF"
                },
                new Command
                {
                    Id = 2,
                    HowTo = "List active migrations",
                    CommandLine = "dotnet ef migrations list",
                    Platform = ".NET Core EF"
                },
                new Command
                {
                    Id = 3,
                    HowTo = "Create PostgreSQL Docker",
                    CommandLine = "docker run --name some-postgres -e POSTGRES_PASSWORD=mysecretpassword -p 5432:5432 -d postgres",
                    Platform = "Docker"
                },
                new Command
                {
                    Id = 4,
                    HowTo = "Install EF Core",
                    CommandLine = "dotnet tool install --global dotnet-ef",
                    Platform = ".NET Core EF"
                }
            };
        public void CreateCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
           
            return commands;
        }

        public Command GetCommandById(int id)
        {
            return commands[id];
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command command)
        {
            throw new NotImplementedException();
        }
    }
}
