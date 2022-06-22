using auth.Models;

namespace auth.Data
{
    public interface IUserRepo
    {
        UserModel CreateUser (UserModel user);
        UserModel GetByEmail(string email);
        UserModel GetById(int Id);
        UserModel GetByCompanyName(string companyName);
        void UpdateUser();
    }
}