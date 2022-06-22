namespace auth.Models
{
    public class UserModelRegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Role { get; set; } 
        public string CompanyName { get; set; }
    }
}