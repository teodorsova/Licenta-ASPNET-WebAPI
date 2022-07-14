namespace auth.Models {
    public class FurnitureModel {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserModel Company { get; set; }
        public string CompanyName { get; set; }
        public int OrderingUserId { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string Status {get; set;}
    }
}