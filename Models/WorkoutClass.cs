using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjektNET.Models
{
    public class WorkoutClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Schedule { get; set; }

        [Required]
        public int Duration { get; set; }

        // Initialize collections to avoid null reference exceptions
        public ICollection<TrainerWorkoutClass> TrainerWorkoutClasses { get; set; } = new List<TrainerWorkoutClass>();

        public ICollection<Trainer> Trainers { get; set; } = new List<Trainer>();

        public ICollection<UserWorkoutClass> UserWorkoutClasses { get; set; } = new List<UserWorkoutClass>();

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
