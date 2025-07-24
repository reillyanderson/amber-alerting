using System.ComponentModel.DataAnnotations;

namespace AmberAlerting.Models
{
    public class Alert
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        public string Message { get; set; } = string.Empty;
        
        /// <summary>
        /// Current countdown time in seconds - calculated based on elapsed time
        /// </summary>
        public int Countdown 
        { 
            get
            {
                var elapsed = (DateTime.UtcNow - CreatedAt).TotalSeconds;
                var remaining = Math.Max(0, OriginalCountdown - (int)elapsed);
                return remaining;
            }
            // No setter - this is calculated only
        }
        
        [Required]
        public int OriginalCountdown { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class NewAlert
    {
        [Required]
        public string Message { get; set; } = string.Empty;
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Countdown must be greater than 0")]
        public int Countdown { get; set; }
    }
}