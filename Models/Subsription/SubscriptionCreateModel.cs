using System.ComponentModel.DataAnnotations;

namespace auth.Models
{

    public class SubscriptionCreateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int Price {get; set;}
        [Required]
        public int FurnitureCap { get; set; }
        [Required]
        public int OccupiedCap { get; set; }
    }
}