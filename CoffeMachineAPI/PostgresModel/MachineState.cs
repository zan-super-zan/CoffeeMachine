using System.ComponentModel.DataAnnotations;

namespace CoffeMachineAPI.PostgresModel
{
    public class MachineState
    {
        [Key]
        public int Id { get; set; }
        public uint CoffeeAmountGrams { get; set; }
        public uint WaterAmountMl { get; set; }
        public bool IsPoweredOn { get; set; }
    }
}
