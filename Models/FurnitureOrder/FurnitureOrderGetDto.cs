using System.ComponentModel.DataAnnotations;

namespace auth.Models {
    public class FurnitureOrderModelGetDto {
        [Required]
        public int OrderModelId { get; set; }
    }
}