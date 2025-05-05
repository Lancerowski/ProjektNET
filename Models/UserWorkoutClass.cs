using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektNET.Models
{
    public class UserWorkoutClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int WorkoutClassId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("WorkoutClassId")]
        public WorkoutClass WorkoutClass { get; set; }
    }
}
