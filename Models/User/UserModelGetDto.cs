namespace auth.Models
{
    public class UserModelGetDto
    {
        public int Id {get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string CompanyName { get; set; }
        
    }
}