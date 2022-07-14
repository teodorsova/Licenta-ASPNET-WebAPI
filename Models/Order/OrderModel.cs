using System.ComponentModel.DataAnnotations;

namespace auth.Models {
    public class OrderModel {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public UserModel User { get; set; }
        public string AddressLine1 {get;set;}
        public string AddressLine2{get;set;}
        public DateTime Date {get;set;}
    }
}