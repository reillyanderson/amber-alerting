using Microsoft.EntityFrameworkCore;
using AmberAlerting.Models;

namespace AmberAlerting.Data
{
    public class AlertContext : DbContext
    {
        public AlertContext(DbContextOptions<AlertContext> options) : base(options)
        {
        }

        public DbSet<Alert> Alerts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alert>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Message).IsRequired();
                entity.Property(e => e.OriginalCountdown).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
                
                // Ignore calculated properties
                entity.Ignore(e => e.Countdown);
            });
        }
    }
}