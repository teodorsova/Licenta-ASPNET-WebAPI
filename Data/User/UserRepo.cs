using auth.Models;

namespace auth.Data
{
    public class UserRepo : IUserRepo
    {
        private readonly DatabaseContext _context;

        public UserRepo(DatabaseContext context)
        {
            _context = context;
        }
        public UserModel CreateUser(UserModel user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public UserModel GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public UserModel GetById(int Id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == Id);
        }

        public UserModel GetByCompanyName(string companyName) {
            return _context.Users.FirstOrDefault(u => u.CompanyName.Equals(companyName));
        }

        public void UpdateUser()
        {
            _context.SaveChanges();
        }
    }
}