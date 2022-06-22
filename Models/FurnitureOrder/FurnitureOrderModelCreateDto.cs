using System.ComponentModel.DataAnnotations;

namespace auth.Models {
    public class FurnitureOrderModelCreateDto {
        public int Id {get; set;}
        [Required]
        public int OrderModelId { get; set; }
        [Required]
        public int FurnitureModelId { get; set; }
    }
}