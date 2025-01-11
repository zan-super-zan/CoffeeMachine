using CoffeMachineAPI.PostgresModel;
using Microsoft.EntityFrameworkCore;

namespace CoffeMachineAPI.Data
{
    public class CoffeeMachineContext : DbContext
    {
        public CoffeeMachineContext(DbContextOptions<CoffeeMachineContext> options)
            : base(options) { }
        public DbSet<MachineEvent> Events { get; set; }
        public DbSet<MachineState> MachineStates { get; set; }
    }
}
