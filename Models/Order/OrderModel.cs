using System.ComponentModel.DataAnnotations;

namespace auth.Models {
    public class OrderModel {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public UserModel User { get; set; }
        public string Status { get; set; }
    }
}