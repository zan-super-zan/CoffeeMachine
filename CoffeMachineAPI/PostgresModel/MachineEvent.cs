using System.ComponentModel.DataAnnotations;

namespace CoffeMachineAPI.PostgresModel
{
    public class MachineEvent
    {
        [Key]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        [Required]
        public string EventDescription { get; set; }
    }
}
