using System.ComponentModel.DataAnnotations;

namespace auth.Models {
    public class OrderCreateModel {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public string AddressLine1 {get; set;}
        public string AddressLine2 {get; set;}

    }
}