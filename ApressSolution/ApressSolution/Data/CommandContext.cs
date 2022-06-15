using ApressSolution.Models;
using Microsoft.EntityFrameworkCore;

namespace ApressSolution.Data
{
    public class CommandContext : DbContext
    {
        public CommandContext(DbContextOptions<CommandContext> options) : base(options)
        {

        }

        public DbSet<Command> Commands { get; set; }
    }
}
