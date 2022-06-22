using System.ComponentModel.DataAnnotations;

namespace auth.Models{
    public class FurnitureOrderModel {
        
        
        public int Id { get; set; }
        [Required]
        public int OrderModelId { get; set; }
        public OrderModel Order {get; set;}
        [Required]
        public int FurnitureModelId { get; set; }
        public FurnitureModel Furniture { get; set; }
    }
}