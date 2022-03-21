using WebAPI.Models;

namespace WebAPI.Servers
{
    public interface IUserServer
    {
        User Create(User user);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User GetByEmail (string email);
    }
}
