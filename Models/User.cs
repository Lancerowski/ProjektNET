using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjektNET.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public int Age { get; set; }

        // Make the navigation property nullable
        public ICollection<UserWorkoutClass>? UserWorkoutClasses { get; set; }
    }
}
