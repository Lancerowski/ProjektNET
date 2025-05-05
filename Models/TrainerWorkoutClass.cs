using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektNET.Models
{
    public class TrainerWorkoutClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TrainerId { get; set; }

        [Required]
        public int WorkoutClassId { get; set; }

        [ForeignKey("TrainerId")]
        public Trainer? Trainer { get; set; }

        [ForeignKey("WorkoutClassId")]
        public WorkoutClass? WorkoutClass { get; set; }
    }
}
