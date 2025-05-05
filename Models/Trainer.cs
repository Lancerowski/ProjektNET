using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjektNET.Models
{
    public class Trainer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Phone { get; set; }

        public ICollection<TrainerWorkoutClass>? TrainerWorkoutClasses { get; set; }

        
    }
}
