using Microsoft.EntityFrameworkCore;
using ProjektNET.Models;

namespace ProjektNET.Data
{
    public class ProjektNETDbContext : DbContext
    {
        public ProjektNETDbContext(DbContextOptions<ProjektNETDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<WorkoutClass> WorkoutClasses { get; set; }
        public DbSet<UserWorkoutClass> UserWorkoutClasses { get; set; }
        public DbSet<TrainerWorkoutClass> TrainerWorkoutClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // UserWorkoutClass: relation setup
            modelBuilder.Entity<UserWorkoutClass>()
                .HasOne(uwc => uwc.User)
                .WithMany(u => u.UserWorkoutClasses)
                .HasForeignKey(uwc => uwc.UserId);

            modelBuilder.Entity<UserWorkoutClass>()
                .HasOne(uwc => uwc.WorkoutClass)
                .WithMany(wc => wc.UserWorkoutClasses)
                .HasForeignKey(uwc => uwc.WorkoutClassId);

            // TrainerWorkoutClass: relation setup
            modelBuilder.Entity<TrainerWorkoutClass>()
                .HasOne(twc => twc.Trainer)
                .WithMany(t => t.TrainerWorkoutClasses)
                .HasForeignKey(twc => twc.TrainerId);

            modelBuilder.Entity<TrainerWorkoutClass>()
                .HasOne(twc => twc.WorkoutClass)
                .WithMany(wc => wc.TrainerWorkoutClasses)
                .HasForeignKey(twc => twc.WorkoutClassId);
        }
    }
}
