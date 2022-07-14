namespace auth.Models {
    public class FurnitureCreateDto {
        public string CompanyName { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public int OrderingUserId { get; set; }
    }
}